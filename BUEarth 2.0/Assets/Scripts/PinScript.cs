using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{

    public void pinProcedure()
    {        
        // Do a raycast into the world that will only hit the Spatial Mapping mesh.
        var gazeDirection = Camera.main.transform.forward;

       

        AudioLibrary.AlertSound();
        // Move this object's parent object to
        // where the raycast hit the Spatial Mapping mesh.
        this.transform.position = gazeDirection.normalized * 11;

        // Rotate this object's parent object to face the user.
        Quaternion toQuat = Quaternion.LookRotation(Camera.main.transform.forward);
        // toQuat.x = 0;
        //toQuat.z = 0;
        this.transform.parent.rotation = toQuat;
    }
}
