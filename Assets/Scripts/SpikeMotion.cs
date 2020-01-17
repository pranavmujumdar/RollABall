using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMotion : MonoBehaviour
{
    //adjust this to change speed
    public float speed = 1f;
    //adjust this to change how high it goes
    float height = 0.3f;

    // Update is called once per frame
    void Update()
    {
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z) ;
    }
}
