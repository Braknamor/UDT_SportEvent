using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Security.Cryptography;
using System;

public class OffscreenIndicator : MonoBehaviour
{
    /*******************
    * public variables *
    ********************/
    public Canvas arrowCanvas;
    public GameObject arrowsPrefab;
    public Transform parentCanvas;
    [HideInInspector]
    public List<GameObject> objects;

   /********************
   * private variables *
   ********************/
    private List<GameObject> listArrows = new List<GameObject>();
    private bool pass = false;
    private int i;
    private int j = 0;

    // Update is called once per frame
    void LateUpdate()
    {
        paint();
    }
    // methods that calcul the position of the offscreen arrows
    void paint()
    {
        i = 0;
        // we create for each athlete one arrow
        while (j < objects.Count)
        {
            GameObject arrow = Instantiate(arrowsPrefab, parentCanvas.transform) as GameObject;
            arrow.name = "arrow" + j;
            listArrows.Add(arrow);
            j++;
        }

        foreach (GameObject obj in objects)
		{
            // we will affect one arrow for one athlete. If we don't specify that,
            // every arrow will be pointing at the last athlete of our list
            GameObject arrows = listArrows[i];

            // take the actual position of the object we want to see
            Vector3 screenpos = Camera.main.WorldToScreenPoint(obj.transform.position);

            // those first line into the if condition will determine if our object
            // is inside the focus of our screen, if it's the case we do not want to
            // display the arrow
            if (screenpos.z > 0 &&
                screenpos.x > 0 && screenpos.x < Screen.width &&
                screenpos.y > 0 && screenpos.y < Screen.height)
            {
                foreach (GameObject g in listArrows)
                {
                    g.GetComponent<Image>().enabled = false;
                }                                         
            }
            // here we are going to calculate the position for our arrow in border of our screen
            // and place it
            else
            {
                // display the arrow
                arrows.GetComponent<Image>().enabled = true;                

                if (screenpos.z < 0)
                {
                    screenpos *= -1;
                }

                Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

                screenpos -= screenCenter;

                float angle = Mathf.Atan2(screenpos.y, screenpos.x);
                angle -= 90 * Mathf.Deg2Rad;

                float cos = Mathf.Cos(angle);
                float sin = Mathf.Sin(angle);

                screenpos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

                float m = cos / sin;

                Vector3 screenBounds = screenCenter * 0.9f;

                if (cos > 0)
                {
                    screenpos = new Vector3(-screenBounds.y / m, screenBounds.y, 0);
                }
                else
                {
                    screenpos = new Vector3(screenBounds.y / m, -screenBounds.y, 0);
                }

                if (screenpos.x > screenBounds.x)
                {
                    screenpos = new Vector3(screenBounds.x, -screenBounds.x * m, 0);
                }
                else if (screenpos.x < -screenBounds.x)
                {
                    screenpos = new Vector3(-screenBounds.x, screenBounds.x * m, 0);
                }

                screenpos += screenCenter;

                // set the position and rotation of the arrow
                arrows.transform.position = screenpos;
                arrows.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
                i++;
            }
        }
        
    }

}
