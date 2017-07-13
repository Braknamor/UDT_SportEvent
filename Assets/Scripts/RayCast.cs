using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {

    /******** IMPORTANT ****************
    *                                  *
    * this script is not used anymore! *
    *                                  *
    ***********************************/

    public int fingerTap;
	
	// Update is called once per frame
	void Update () {

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].tapCount == fingerTap)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast (ray, out hit))
                {
                    Debug.Log("Barycentric coordinate : " + hit.barycentricCoordinate);
                    Debug.Log("distance : " + hit.distance);
                    Debug.Log("transform : " + hit.transform);
                }
            }
        }
		
	}
}
