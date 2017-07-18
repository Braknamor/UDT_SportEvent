using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour {
    /*********************
    * scripts references *
    *********************/
    public Filters _filters;

    /*******************
    * public variables *
    ********************/
    public Camera cameraLeft;
    public GameObject middleScreen;
    public float waitingTimePaticleSystem;
    public GameObject InfoPanel;
    public Text infoText;
    public string info;
    [HideInInspector]
    public List<GameObject> goList;    

   /********************
   * private variables *
   ********************/
    private RaycastHit hitInfo;
    private Collider hitTemp;
    private Coroutine coroutine;
	
	// Update is called once per frame
	void Update () {
        // create a ray that will detect what he is touching
        Ray ray = cameraLeft.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // if he it the demo shop
            if (hit.transform.gameObject.name.Equals("colliderShop"))
            {
                // check if the light is not already turned on
                if (!hit.transform.gameObject.GetComponentInChildren<Light>().isActiveAndEnabled)
                {
                    hit.transform.gameObject.GetComponentInChildren<Light>().enabled = true;
                    InfoPanel.SetActive(true);
                    infoText.text = info;
                }                    
                // making sure that we can reuse the coroutines
                if (coroutine != null)
                    StopCoroutine(coroutine);

                coroutine = StartCoroutine(stopParticleSystem(waitingTimePaticleSystem, hit.transform.gameObject));
            }

            foreach (GameObject g in goList)
            {
                if (_filters.filterChoice.Equals("NO FILTER") || _filters.filterChoice == "")
                    return;

                if (hit.transform.gameObject.name.Equals(g.name))
                    {
                        if (hit.transform.gameObject.GetComponent<Renderer>().enabled)
                        {
                            if (!hit.transform.gameObject.GetComponentInChildren<ParticleSystem>().isEmitting)
                            {
                                hit.transform.gameObject.GetComponentInChildren<ParticleSystem>().Play();                                
                            }

                            if (coroutine != null)
                                StopCoroutine(coroutine);

                            coroutine = StartCoroutine(stopParticleSystem(waitingTimePaticleSystem, hit.transform.gameObject));
                        }                 
                    }       
             }         
         }
    }

    private IEnumerator stopParticleSystem(float waitTimeToEndParticleSytem, GameObject go)
    {
        yield return new WaitForSeconds(waitTimeToEndParticleSytem);

        if (go.GetComponentInChildren<ParticleSystem>())
            go.GetComponentInChildren<ParticleSystem>().Stop();

        if (go.GetComponentInChildren<Light>())
            go.GetComponentInChildren<Light>().enabled = false;

        if (InfoPanel.activeInHierarchy)
            InfoPanel.SetActive(false); 
    }
}
