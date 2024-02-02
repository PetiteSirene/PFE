using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    [SerializeField] float minX, maxX, minZ, maxZ;

    [SerializeField] float speed;
    [SerializeField] float duration;



    private IEnumerator MoveCoroutine(float duration, float xAmount, float zAmount)
    {
        float timer = 0f;
        while (timer < duration)
        {   
            float t = timer / duration;
            float t2 = t * (1 - t);
            TryTranslateX(xAmount * Time.deltaTime * t2);
            TryTranslateZ(zAmount * Time.deltaTime * t2);            
            yield return null;
        }
    }

    private void TryTranslateX(float amount)
    {
        if(amount > 0)
        {
            if(transform.position.x + amount > maxX)
            {
                SetX(maxX);
            }
            else
            {
                transform.Translate(Vector3.right * amount);
            }
        }
        else
        {
            if(transform.position.x + amount < minX)
            {
                SetX(minX);
            }
            else
            {
                transform.Translate(Vector3.right * amount);
            }
        }
    }

    private void TryTranslateZ(float amount)
    {
        if(amount > 0)
        {
            if(transform.position.z + amount > maxZ)
            {
                SetZ(maxZ);
            }
            else
            {
                transform.Translate(Vector3.forward * amount);
            }
        }
        else
        {
            if(transform.position.z + amount < minZ)
            {
                SetZ(minZ);
            }
            else
            {
                transform.Translate(Vector3.forward * amount);
            }
        }
    }

    private void SetX(float x)
    {
        Vector3 newPos = transform.position;
        newPos.x = x;
        transform.position = newPos;    
    }

    private void SetZ(float z)
    {
        Vector3 newPos = transform.position;
        newPos.z = z;
        transform.position = newPos;
    }

    public void Move(Vector3 vec)
    {
        float x = vec.z;
        float z = vec.x;

        //StartCoroutine(MoveCoroutine(duration, speed * vec.x, speed * vec.z));
        if (x < 180f)
        {
            TryTranslateX(-speed * x*x * Time.deltaTime );
        }
        else
        {
            TryTranslateX(speed * (x - 360f)* (x - 360f) * Time.deltaTime );
        }
         if (z < 180f)
        {
            TryTranslateZ(-speed *3 * z * z * Time.deltaTime );
        }
        else
        {
            TryTranslateZ(speed *3 * (z - 360f) * (z - 360f) * Time.deltaTime );
        }
        
    }


}