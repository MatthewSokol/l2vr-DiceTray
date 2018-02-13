using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTouch : TouchScript
{
   
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        OVRInput.Update();

        if (aPressed())
        {
            DiceTrayManager.createTray();
        }

        bool thumbstick = OVRInput.Get(OVRInput.Button.SecondaryThumbstick);
        if (thumbstick)//NOT WORKING
        {
            print("Right thumbstick pressed");
        }
        float triggerPressure = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        float gripPressure = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
        if (triggerPressure > 0)
        {
        }
        prevTrigger = triggerPressure;

        if (gripPressure > 0 && prevGrip == 0)
        {
            if (this.touching != null && this.touching.CompareTag("selector"))
            {
                DiceTrayManager.addDie(this, touching);
            }
        }
        else if (gripPressure == 0 && prevGrip > 0)
        {
            DiceTrayManager.throwDie(this);
        }
        prevGrip = gripPressure;

        // get input data from keyboard or controller
        Vector2 thumb = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector3 position = transform.position;
        position.x += thumb.x  * Time.deltaTime;
        position.z += thumb.y  * Time.deltaTime;

        prevTrigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        prevGrip = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
        setPrevValues();
    }
    


}
