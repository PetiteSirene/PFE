using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using UnityEngine;

public class SerialHandler : MonoBehaviour
{
    
    private SerialPort serial;
    private bool arduinoNotConnected;
    private Quaternion receivedQuaternion;
    
    public bool ArduinoNotConnected { get; private set; }
    public Quaternion ReceivedQuaternion { get; private set; }

    // Common default serial device on a Windows machine
    [SerializeField] private string serialPort = "COM1";
    [SerializeField] private int baudrate = 115200;
    
    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort(serialPort,baudrate);
        // Guarantee that the newline is common across environments.
        serial.NewLine = "\n";
        // Once configured, the serial communication must be opened just like a file : the OS handles the communication.
        try
        {
            serial.Open();
            ArduinoNotConnected = false;
        }
        catch (IOException)
        {
            ArduinoNotConnected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ArduinoNotConnected)
        {
            try
            {
                serial.Open();
                ArduinoNotConnected = false;
            }
            catch (IOException)
            {
                ArduinoNotConnected = true;
            }
        }
        else
        {
            try
            {
                // Prevent blocking if no message is available as we are not doing anything else
                // Alternative solutions : set a timeout, read messages in another thread, coroutines, futures...
                if (serial.BytesToRead <= 0) return;

                // Trim leading and trailing whitespaces, makes it easier to handle different line endings.
                // Arduino uses \r\n by default with `.println()`.
                var message = serial.ReadLine().Trim().TrimStart('r', '/');

                string[] quaternionCoefficientText = message.Split('/');
                if (quaternionCoefficientText.Length == 4)
                {
                    float[] qCoeffs = new float[4];
                    for (int i = 0; i < 4; i++)
                    {
                        qCoeffs[i] = float.Parse(quaternionCoefficientText[i]);
                    }

                    Quaternion objectRotation = new Quaternion(qCoeffs[0], qCoeffs[1], qCoeffs[2], qCoeffs[3]);
                    ReceivedQuaternion = objectRotation;
                }
            }
            catch (IOException)
            {
                ArduinoNotConnected = true;
                serial.Close();
            }
        }
    }

    public void SendAngularDifference(float angle)
    {
        if (angle < 10)
        {
            serial.WriteLine("0.0");
        }
        serial.WriteLine(angle.ToString(new CultureInfo("en-US")));
    }
    
    private void OnDestroy()
    {
        if (!ArduinoNotConnected)
        {
            serial.Close();   
        }
    }
}
