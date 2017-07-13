using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateObjectOnMap : MonoBehaviour {
    /*********************
    * scripts references *
    *********************/
    public ConvertSphericalToCartesian _convert;
    public NormalizedData _normalizer;
    public UIActions _uiActions;
    public Filters _filters;
    public ScoreBoard _scoreBoard;

    /*******************
    * public variables *
    ********************/
    public GameObject cyclistPrefab;
    public GameObject point0;

    public List<Cartesian> listCartesian_to_use;    

    /*******************
    * private variables *
    ********************/
    private List<Cartesian> listCartesian;
    private List<Cartesian> tempList;


    bool pass = false;
    
    // method called in InstantiateJsonObject script
    // used for instantiate athlete position on the map and the dropdown list for filters
    public void seeCartesian()
    {
        // create a temporary list for later checks (used for the dropdown list)
        if (listCartesian_to_use != null)
        {
            tempList = new List<Cartesian>(listCartesian_to_use);
        }

        listCartesian = new List<Cartesian>();
        listCartesian_to_use = new List<Cartesian>();
        
        // creation of the first list that have the normalized data
        foreach (Groups g in InstantiateJsonObject.timestampObject.groups)
        {
            foreach (Athletes a in g.athletes)
            {
                listCartesian.Add(_normalizer.Normalized_Point(a));
            }
        }

        // creation of the list that we actually use with the points converted to our world
        foreach (Cartesian c in listCartesian)
        {
            listCartesian_to_use.Add(_normalizer.convertPoints(c));
        }

        _uiActions.DisableUI_Element();

        //InstantiateObjectOnMap();

        if (!pass)
        {
            pass = true;
            InstantiateObjectOnMap_Color();
        }        

        // create the dropdown list only if we have a modification in our group of athlete
        // e.g. new athlete, deleted athlete
        if(!CompareList(listCartesian_to_use, tempList))
        {
            _filters.Populate_DropdownList(listCartesian_to_use);
        }
        
        InvokeRepeating("MoveAthletes", 1, 1);

    }

    public void InstantiateObjectOnMap()
    {
        // first we destroy the ancient points of interest, in this case our athlete visualization on the map.
        // point0 means the origin point (transform) for the new objects
        if (point0.transform.childCount > 0)
        {
            foreach (Transform t in point0.transform)
            {
                Destroy(t.gameObject);
            }
        }

        // create for each athletes is representation on the map
        foreach (Cartesian c in listCartesian_to_use)
        {
            GameObject go = Instantiate(cyclistPrefab, new Vector3((float)c.x, 0, (float)c.y), Quaternion.identity, point0.transform) as GameObject;
            go.name = c.athlete.gname;
        }
    }

    public void InstantiateObjectOnMap_Color()
    {
        // create for each athletes is representation on the map
        foreach (Cartesian c in listCartesian_to_use)
        {
            GameObject go = Instantiate(cyclistPrefab, new Vector3((float)c.x, 0, (float)c.y), Quaternion.identity, point0.transform) as GameObject;
            go.name = c.athlete.bib + " " + c.athlete.gname;
            go.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    private void MoveAthletes()
    {
       foreach (Cartesian c in listCartesian_to_use)
        {
            GameObject g = GameObject.Find(c.athlete.bib + " " + c.athlete.gname);
            g.transform.position = new Vector3((float)c.x, 0, (float)c.y);
        }

        ApplyFilters();
        _scoreBoard.setScorBoard(listCartesian_to_use);
    }

    private void ApplyFilters()
    {
        if (point0.transform.childCount > 0)
        {
            if (_filters.filterChoice.Equals("NO FILTER") || _filters.filterChoice == "")
            {
                setAllFilterVisible();
                return;
            }

            foreach (Transform t in point0.transform)
            {
                t.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                if (t.gameObject.name != _filters.filterChoice)
                {
                    t.gameObject.GetComponentInChildren<Renderer>().enabled = false;
                }
            }
        }
    }

    private void setAllFilterVisible()
    {
        foreach (Transform t in point0.transform)
        {
            t.gameObject.GetComponentInChildren<Renderer>().enabled = true;                               
        }
    }


    private bool CompareList(List<Cartesian> list1, List<Cartesian> list2)
    {
        if (list2 == null)
        {
            Debug.Log("Dropdown list : - temporary control list is empty, return false.");
            return false;
        }
            
        // check if the list of athletes is the same (independent of the position in the control list)
        for (int i = 0; i < list1.Count; i++)
        {
            for (int j = 0; j < list2.Count; j++)
            {
                if (list1[i].athlete.gname.Equals(list2[j].athlete.gname))
                {
                    return true;
                }
            }                      
        }
        Debug.Log("Dropdown liste : - Different entries, return false.");
        return false;
    }
}
