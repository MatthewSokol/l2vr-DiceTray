using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTouch : TouchScript {
    
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        OVRInput.Update();

        if (xPressed()) {
            DiceTrayManager.createTray();

        }

        bool thumbstick = OVRInput.Get(OVRInput.Button.PrimaryThumbstick);
        float triggerPressure = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        float gripPressure = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
        if (triggerPressure > 0)
        {
            // print("Left Trigger pressed in " + triggerPressure * 100 + "%");

        }
        prevTrigger = triggerPressure;

        if (gripPressure > 0 && prevGrip == 0)
        {
            if (this.touching != null && this.touching.CompareTag("selector"))
            {
                DiceTrayManager.addDie(this, touching);
            }
        }
        else if(gripPressure == 0 && prevGrip > 0)
        {
            DiceTrayManager.throwDie(this);
        }
        prevGrip = gripPressure;

        // get input data from keyboard or controller
        Vector2 thumb = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        move(thumb);

        prevTrigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        prevGrip = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
        setPrevValues();
    }

    
}
