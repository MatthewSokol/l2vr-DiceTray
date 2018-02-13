using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTrayManager : MonoBehaviour
{
    //private static GameObject tray = new GameObject("DiceTray");
    //private static GameObject tower = new GameObject("DiceTower");
    //private static GameObject diceSelector = new GameObject("DiceGallery");
    private static GameObject tray;
    private static GameObject tower;
    private static GameObject diceSelector;
    public GameObject cloningTray;
    public GameObject cloningDiceSelector;
    private static GameObject actualDiceTray;
    private static GameObject actualDiceSelector;
    //TODO ...singleton?
    private void Start()
    {
        tray = cloningTray;
        diceSelector = cloningDiceSelector;
    }

    internal static void createTray()
    {
        if (actualDiceTray == null)
        {
            OvrAvatarLocalDriver avatar = FindObjectOfType<OvrAvatarLocalDriver>();


            Vector3 trayPosition = new Vector3();
            trayPosition.x = avatar.transform.position.x - 1f; //left/right
            trayPosition.y = avatar.transform.position.y - 0.75f;
            trayPosition.z = avatar.transform.position.z + 1.5f; //forward/back?
            Quaternion trayRotation = new Quaternion();
            trayRotation.x = 0;
            trayRotation.y = 0;
            trayRotation.z = 0;
            actualDiceTray = Instantiate(tray, trayPosition, trayRotation, avatar.transform);
			

            Vector3 dicePosition = new Vector3();
            dicePosition.x = avatar.transform.position.x - 0.8f;// TODO base on where player is facing, 
            dicePosition.y = avatar.transform.position.y - 0.75f;
            dicePosition.z = avatar.transform.position.z + 1.5f;
            Quaternion diceRotation = new Quaternion();
            diceRotation.x = 0;
            diceRotation.y = 0;
            diceRotation.z = 0;
			actualDiceSelector = Instantiate(diceSelector, dicePosition, diceRotation, avatar.transform);

        }
        else
        {
            Destroy(actualDiceTray);
            Destroy(actualDiceSelector);
            actualDiceTray = null;
        }
    }


    internal static void throwDie(TouchScript controllerScript)
    {
        GameObject die = null;
        var children = controllerScript.gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.gameObject.CompareTag("rolling"))
            {
                die = child.gameObject;
                break;
            }
        }

        if (die != null)
        {
            die.transform.parent = actualDiceTray.transform;
            Rigidbody rb = die.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.MovePosition(rb.position + Time.deltaTime * (controllerScript.speed * Input.GetAxisRaw("Vertical") * controllerScript.transform.TransformDirection(Vector3.forward)));
        }

    }

    internal static void addDie(TouchScript controllerScript, GameObject touching)
    {
        
        GameObject d = Instantiate(touching, controllerScript.gameObject.transform);
        Vector3 dp = d.transform.position;
        dp.x = controllerScript.gameObject.transform.position.x; //left/right
        dp.y = controllerScript.gameObject.transform.position.y;
        dp.z = controllerScript.gameObject.transform.position.z; //forward/back?
        d.transform.position = dp;
        d.tag = "rolling";
        Vector3 scale = d.transform.localScale;
        scale.x = 0.02f;
        scale.y = 0.02f;
        scale.z = 0.02f;
        d.transform.localScale = scale;
        Collider c = d.GetComponent<Collider>();

    }
}
