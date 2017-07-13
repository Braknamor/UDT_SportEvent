using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour {

    /******************
    * public variable *
    *******************/
    public Canvas uiCanvas;
    public Camera camera;
    public Canvas userHelperCanvas;
    

	public void DisableUI_Element()
    {
        camera.enabled = true;
        uiCanvas.enabled = false;
    }

    public void EnableUI_Element()
    {
        uiCanvas.enabled = true;
    }

    public void gotIt_UserHelper()
    {
        userHelperCanvas.enabled = false;
    }
}
