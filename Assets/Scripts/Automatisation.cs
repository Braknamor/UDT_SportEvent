using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatisation : MonoBehaviour {

    /******************
    * public variable *
    *******************/
    public InstantiateJsonObject _instantiateJsonObject;
    public float time;
    public float repeatTime;

    public static bool launchAppBool = false;

    // provide the abilitie to call a method at a certain time
    void Update () {

        if (launchAppBool)
        {
            launchAppBool = false;
            _instantiateJsonObject.InvokeRepeating("loadFileFromLocalComputer", time, repeatTime);
        }
        
	}
}
