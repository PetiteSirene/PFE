#include "Uduino.h"  // Include Uduino library at the top of the sketch
Uduino uduino("IMU");

#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#include "Wire.h"

int LED = 7;

MPU6050 mpu;

// MPU control/status vars
bool dmpReady = false;  // set true if DMP init was successful
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// orientation/motion vars
Quaternion q;           // [w, x, y, z]         quaternion container
VectorInt16 aa;         // [x, y, z]            accel sensor measurements
VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity;    // [x, y, z]            gravity vector
float euler[3];         // [psi, theta, phi]    Euler angle container
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector

int oldTime;
int timer;
int ms_since_last_beep;

void setup() {
  Wire.begin();
  Wire.setClock(400000); // 400kHz I2C clock. Comment this line if having compilation difficulties

  Serial.begin(115200);

  while (!Serial); // wait for Leonardo enumeration, others continue immediately

  mpu.initialize();
  devStatus = mpu.dmpInitialize();
  //mpu 6050
  /*
  mpu.setXGyroOffset(147); //++ 
  mpu.setYGyroOffset(-618); //--
  mpu.setZGyroOffset(-770);
  mpu.setXAccelOffset(-525);
  mpu.setYAccelOffset(1343);
  mpu.setZAccelOffset(2233);
  */
  //nv mpu6050
  mpu.setXGyroOffset(-5); //++ 
  mpu.setYGyroOffset(-50); //--
  mpu.setZGyroOffset(29);
  mpu.setXAccelOffset(-2219);
  mpu.setYAccelOffset(1287);
  mpu.setZAccelOffset(1289);

  //gyro module IoT
  /*
  mpu.setXGyroOffset(-504); //++ 
  mpu.setYGyroOffset(-173); //--
  mpu.setZGyroOffset(-27);
  mpu.setXAccelOffset(-142);
  mpu.setYAccelOffset(117);
  mpu.setZAccelOffset(1665);
  */


  if (devStatus == 0) {
    mpu.setDMPEnabled(true);
    // set our DMP Ready flag so the main loop() function knows it's okay to use it
    dmpReady = true;
    // get expected DMP packet size for later comparison
    packetSize = mpu.dmpGetFIFOPacketSize();
  } else {
    // Error
    Serial.println("Error!");
  }
}



void loop() {
  uduino.update();

  if (!uduino.isInit()) {
    if (!dmpReady) {
      Serial.println("IMU not connected.");
      delay(10);
      return;
    }

    int  mpuIntStatus = mpu.getIntStatus();
    fifoCount = mpu.getFIFOCount();

    if ((mpuIntStatus & 0x10) || fifoCount == 1024) { // check if overflow
      mpu.resetFIFO();
    } else if (mpuIntStatus & 0x02) {
      while (fifoCount < packetSize) fifoCount = mpu.getFIFOCount();

      mpu.getFIFOBytes(fifoBuffer, packetSize);
      fifoCount -= packetSize;

      SendQuaternion();
      //SendEuler();
      //SendYawPitchRoll();
      //SendRealAccel();
      //SendWorldAccel();
    }
  }
  oldTime = timer;
  timer = millis();
  ms_since_last_beep += timer - oldTime;
}



void SendQuaternion() {
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  Serial.print("r/");
  Serial.print(q.w, 4); Serial.print("/");
  Serial.print(q.x, 4); Serial.print("/");
  Serial.print(q.y, 4); Serial.print("/");
  Serial.println(q.z, 4);
}

void SendEuler() {
  // display Euler angles in degrees
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetEuler(euler, &q);
  Serial.print(euler[0] * 180 / M_PI); Serial.print("/");
  Serial.print(euler[1] * 180 / M_PI); Serial.print("/");
  Serial.println(euler[2] * 180 / M_PI);
}

void SendYawPitchRoll() {
  // display Euler angles in degrees
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetGravity(&gravity, &q);
  mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
  Serial.print(ypr[0] * 180 / M_PI); Serial.print("/");
  Serial.print(ypr[1] * 180 / M_PI); Serial.print("/");
  Serial.println(ypr[2] * 180 / M_PI);
}

void SendRealAccel() {
  // display real acceleration, adjusted to remove gravity
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetAccel(&aa, fifoBuffer);
  mpu.dmpGetGravity(&gravity, &q);
  mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
  Serial.print("a/");
  Serial.print(aaReal.x); Serial.print("/");
  Serial.print(aaReal.y); Serial.print("/");
  Serial.println(aaReal.z);
}

void SendWorldAccel() {
  // display initial world-frame acceleration, adjusted to remove gravity
  // and rotated based on known orientation from quaternion
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetAccel(&aa, fifoBuffer);
  mpu.dmpGetGravity(&gravity, &q);
  mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
  mpu.dmpGetLinearAccelInWorld(&aaWorld, &aaReal, &q);
  Serial.print("a/");
  Serial.print(aaWorld.x); Serial.print("/");
  Serial.print(aaWorld.y); Serial.print("/");
  Serial.println(aaWorld.z);
}

// Handles incoming messages
// Called by Arduino if any serial data has been received
#include <math.h>

int buzzerPin = 8;

float note_A4 = 440.0;

float max_delay = 1000.0;

void serialEvent()
{
  String message = Serial.readStringUntil('\n');
  /*if (message == "Ball OUT") {
    digitalWrite(LED,HIGH);
  }*/
  

  /* TODO: Reactivate code below (for angles) for later work */
  
  float angle = message.toFloat();
  float angle_converted = abs(cos(angle));
  float max_period = 5000; //en ms
  float period =  max_period * (1 - angle_converted);
  int note_to_play = int(angle_converted*100.0 + note_A4 - 100.0);
  //delay(max_delay * (1 - angle_converted));
  if (ms_since_last_beep > period) {
    tone(buzzerPin, note_to_play, 100);
    ms_since_last_beep = 0;
  }
  
}