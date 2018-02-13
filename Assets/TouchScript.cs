using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    internal float prevGrip;
    internal float prevTrigger;
    internal bool prevX;
    internal bool prevY;
    internal bool prevA;
    internal bool prevB;
    internal GameObject touching;
    public Vector3 prevPosition = Vector3.zero;
    public float speed = 0;

    internal void move(Vector2 joystick)
    {

        Vector3 position = transform.position;
        position.x += joystick.x * Time.deltaTime;
        position.z += joystick.y * Time.deltaTime;
        transform.position = position;
    }


    internal void OnCollisionEnter(Collision collision)
    {
        this.touching = collision.gameObject;
    }

    internal void OnCollisionExit(Collision collision)
    {
        this.touching = null;
    }


    //NOTE: touch controllers seem to be getting crossed
    internal bool aPressed()
    {
        bool a = OVRInput.Get(OVRInput.Button.Three);
        return a != prevA && a;
    }

    internal bool aReleased()
    {
        bool a = OVRInput.Get(OVRInput.Button.Three);
        return a != prevA && !a;
    }

    internal bool bPressed()
    {
        bool b = OVRInput.Get(OVRInput.Button.Four);
        return b != prevB && b;
    }

    internal bool bReleased()
    {
        bool b = OVRInput.Get(OVRInput.Button.Four);
        return b != prevB && !b;
    }

    internal bool xPressed()
    {
        bool x = OVRInput.Get(OVRInput.Button.One);
        return x != prevX && x;
    }
    internal bool xReleased()
    {
        bool x = OVRInput.Get(OVRInput.Button.One);
        return x != prevX && !x;
    }

    internal bool yPressed()
    {
        bool y = OVRInput.Get(OVRInput.Button.Two);
        return y != prevY && y;
    }
    internal bool yReleased()
    {
        bool y = OVRInput.Get(OVRInput.Button.Two);
        return y != prevY && !y;
    }
    internal void setPrevValues()
    {
        prevA = OVRInput.Get(OVRInput.Button.Three);
        prevB = OVRInput.Get(OVRInput.Button.Four);
        prevX = OVRInput.Get(OVRInput.Button.One);
        prevY = OVRInput.Get(OVRInput.Button.Two);
    }

    public void FixedUpdate()
    {

        speed = (transform.position - prevPosition).magnitude;
        prevPosition = transform.position;
    }

}