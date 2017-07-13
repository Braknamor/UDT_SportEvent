using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizedData : MonoBehaviour {

    /******************
    * public variable *
    *******************/
    public double max_Lat;
    public double min_Lat;
    public double max_Lon;
    public double min_Lon;

    public double MAX_X;
    public double MAX_Y;
    public double MIN_X;
    public double MIN_Y;

    // method to normalize our GPS data
    public Cartesian Normalized_Point(Athletes athlete)
    {
        double x = (athlete.lat - min_Lat) / (max_Lat - min_Lat);
        double y = (athlete.lon - min_Lon) / (max_Lon - min_Lon);

        x = x * (MAX_X - MIN_X) + MIN_X;
        y = y * (MAX_Y - MIN_Y) + MIN_Y;

        return new Cartesian(athlete, x,y);
    }

    // perform a "rotation" of our points for having the right display
    public Cartesian convertPoints(Cartesian cartesian)
    {
        double x1 = -(cartesian.y / (MAX_Y - MIN_Y)) * (MAX_X - MIN_X) + MAX_X;
        double y1 = -(cartesian.x / (MAX_X - MIN_X)) * (MAX_Y - MIN_Y) + MAX_Y;

        return new Cartesian(cartesian.athlete, x1,y1);
    }
}
