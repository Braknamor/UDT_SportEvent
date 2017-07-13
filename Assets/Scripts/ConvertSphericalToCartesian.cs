using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertSphericalToCartesian : MonoBehaviour {

    /******** IMPORTANT ****************
    *                                  *
    * this script is not used anymore! *
    *                                  *
    ***********************************/

    public float earthRadius;

    public Cartesian convertSphericalToCartesian(Athletes athlete)
    {
        double lat = DegToRad(athlete.lat);
        double lon = DegToRad(athlete.lon);

        var x = earthRadius * Mathf.Cos((float)lat) * Mathf.Cos((float)lon);
        var y = earthRadius * Mathf.Cos((float)lat) * Mathf.Sin((float)lon); //altitude
        var z = earthRadius * Mathf.Sin((float)lat);        

        return new Cartesian(athlete, x, y);
    }

    private double DegToRad(double x)
    {
        return x * Mathf.PI / 180;
    }

    public void CartesianMaped(Cartesian cartesianValue)
    {
        //normalize_Y(cartesianValue.y);
        //normalize_X(cartesianValue.x);
    }

    private void normalize_Y(float y_axis)
    {
        Debug.Log(y_axis);
        float y = (y_axis - 4610.288f) / (4612.416f - 46103.288f);
        Debug.Log("normalized data y : " + y);
    }

    private void normalize_X(float x_axis)
    {
        Debug.Log(x_axis);
        float x = (x_axis - 4371.713f) / (4368.943f - 4371.713f);
        Debug.Log("normalized data x : " + x);
    }
}


