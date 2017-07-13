using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;

public class InstantiateJsonObject : MonoBehaviour {
    /*********************
    * scripts references *
    *********************/
    public UIActions _uiActions;
    public GenerateObjectOnMap _seeCartesian;

    /******************
    * public variable *
    *******************/
    public string fileLocation;
    public string fileExtension;
    public int nJsonFile;
    public string url;
    
    public GameObject canvasEnd;
    public GameObject cameraCanvas;
    public GameObject ARCamera;

    /*******************
    * private variable *
    ********************/
    private string jsonString;
    private string path;
    private int JsonFileTemp = 0;
    private int cptNoFile = 0;
    private bool getNameOnce = false;

    private JsonData itemData;

    private List<Athletes> listAthletesWithinRanks;
    private List<Groups> listGroups;   
   
    public static TimeStamp timestampObject; 

    // method that load our objects from the json files provided
    public void loadFileFromLocalComputer()
    {
        // check that we do not take the same file as previously
        if (nJsonFile == JsonFileTemp )
        {
            nJsonFile += 1;
        }

        try
        {

#if UNITY_EDITOR
            Debug.Log("Unity editor");
            path = fileLocation;
            if (!getNameOnce)
            {
                getNameOnce = true;
                GetFileNameUnityEditor(path);
            }

#elif UNITY_ANDROID
            Debug.Log("Android editor");
            path = Application.streamingAssetsPath + "/jsonData/";
            //GetFileNameAndroidEditor(path);
#endif

            WWW wwwObject = new WWW(path + nJsonFile + fileExtension);
            
            // wait until the end of the download
            while (!wwwObject.isDone)
            {
                Debug.Log(wwwObject.bytesDownloaded);
            }

            jsonString = wwwObject.text;
            itemData = JsonMapper.ToObject(jsonString);
            
            listAthletesWithinRanks = new List<Athletes>();
            listGroups = new List<Groups>();
            
            instantiateListsAthletes(itemData);
            
            instantiateListsGroups(itemData);
            
            instantiateListsTimeStamp(itemData);
            
            JsonFileTemp = nJsonFile;

            _seeCartesian.seeCartesian();
        }
        catch (Exception)
        {
            // the exception catch the fact that we have no file
            // after 50 not existing files, the program stop.
            Debug.Log("File doesn't exist");
            cptNoFile++;

            if (cptNoFile < 50)
            {
                nJsonFile += 1;
                loadFileFromLocalComputer();
            } else
            {
                ARCamera.SetActive(false);
                cameraCanvas.SetActive(true);
                canvasEnd.SetActive(true);
                Time.timeScale = 0;
            }            
        } 
    }

    private void instantiateListsAthletes(JsonData itemData)
    {
        // parsing the athletes objects from the Json
        for (int j = 0; j < itemData["groups"].Count; j++)
        {
            for (int i = 0; i < itemData["groups"][j]["athletes"].Count; i++)
            {
                listAthletesWithinRanks.Add(new Athletes(itemData["groups"][j]["athletes"][i]["hr"].ToString(),
                                                          itemData["groups"][j]["athletes"][i]["pow"].ToString(),
                                                          (int)itemData["groups"][j]["athletes"][i]["bib"],
                                                          (int)itemData["groups"][j]["athletes"][i]["rank"],
                                                          itemData["groups"][j]["athletes"][i]["IRM"].ToString(),
                                                          itemData["groups"][j]["athletes"][i]["gname"].ToString(),
                                                          (double)itemData["groups"][j]["athletes"][i]["lat"],
                                                          itemData["groups"][j]["athletes"][i]["nationality"].ToString(),
                                                          itemData["groups"][j]["athletes"][i]["spd"].ToString(),
                                                          (double)itemData["groups"][j]["athletes"][i]["lon"],
                                                          (int)itemData["groups"][j]["athletes"][i]["valid"],
                                                          itemData["groups"][j]["athletes"][i]["fname"].ToString(),
                                                          itemData["groups"][j]["athletes"][i]["team"].ToString(),
                                                          itemData["groups"][j]["athletes"][i]["cad"].ToString()));
            }
        }
    }

    private void instantiateListsGroups(JsonData itemData)
    {
        // parsing the groups from the Json 
        for (int i = 0; i < itemData["groups"].Count; i++)
        {
            listGroups.Add(new Groups((int)itemData["groups"][i]["distance"],
                                  itemData["groups"][i]["gap"].ToString(),
                                  listAthletesWithinRanks,
                                  (int)itemData["groups"][i]["no"]));
        }     
    }

    private void instantiateListsTimeStamp(JsonData itemData)
    {
        // parsing the timestamp from the Json
        timestampObject = new TimeStamp((int)itemData["timestamp"], listGroups);
    }

    private void GetFileNameUnityEditor(string path)
    {
        string rightPath = path.Substring(path.IndexOf('/')+2);

        DirectoryInfo dir = new DirectoryInfo(rightPath); //"F:/Hes-so/Semestre 8/Travail de Bachelor/données/Données corrigées/json/"
        FileInfo[] info = dir.GetFiles("*.*");

        string temp = info[0].Name;
        string output = temp.Remove(temp.IndexOf('.'));
        nJsonFile = int.Parse(output);        
    }

    private void GetFileNameAndroidEditor(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.*");

        string temp = info[0].Name;
        string output = temp.Remove(temp.IndexOf('.'));
        nJsonFile = int.Parse(output);
        Debug.Log("Nom du fichier : " + nJsonFile);
    }
}
