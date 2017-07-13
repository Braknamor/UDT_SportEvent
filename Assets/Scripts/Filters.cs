using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filters : MonoBehaviour
{
    /******************
    * public variable *
    *******************/
    public Dropdown dropdown;

    [HideInInspector]
    public string filterChoice;

    /******************
    * private variable *
    *******************/
    private List<string> cyclistNames;

    // dynamic method that save the choice selected in the dropdown list
    public void Dropdown_OnChanged(int index)
    {
        filterChoice = cyclistNames[index];
    }

    public void Populate_DropdownList(List<Cartesian> athletes)
    {
        cyclistNames = new List<string>();
        Debug.Log("Dropdown list : - Populate the dropdown list");
        cyclistNames.Add("NO FILTER");
        foreach (Cartesian c in athletes)
        {
            cyclistNames.Add(c.athlete.bib + " " + c.athlete.gname);
        }

        dropdown.options.Clear();

        dropdown.AddOptions(cyclistNames);
    }
}
