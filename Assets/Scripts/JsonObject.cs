using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;

public class JsonObject : MonoBehaviour {
    // creation of the objects that will be used by the program
}

[Serializable]
public class Course
{
    /******** IMPORTANT ****************
    *                                  *
    * this class is not used           *
    * keep it just in case             *
    *                                  *
    ***********************************/
    public string courseName { get; set; }
    public int distance { get; set; }
    public List<Participant> participant { get; set; }
    public int mode { get; set; }
    public int id { get; set; }
    public float length { get; set; }
    public int sequence { get; set; }

    public Course(string courseName, int distance, List<Participant> participant, int mode, int id, float length, int sequence)
    {
        this.courseName = courseName;
        this.distance = distance;
        this.participant = participant;
        this.mode = mode;
        this.id = id;
        this.length = length;
        this.sequence = sequence;
    }

    public Course() { }

}

[Serializable]
public class Participant
{
    /******** IMPORTANT ****************
    *                                  *
    * this class is not used           *
    * keep it just in case             *
    *                                  *
    ***********************************/
    public decimal lattitude { get; set; }
    public decimal longitude { get; set; }
    public int pSequence { get; set; }
    public int lap { get; set; }

    public Participant(decimal t, decimal n, int q, int l)
    {
        this.lattitude = t;
        this.longitude = n;
        this.pSequence = q;
        this.lap = l;
    }

    public Participant() { }
}

    /******** IMPORTANT ****************
    *                                  *
    * all the class after are used     *
    *                                  *
    ***********************************/

[Serializable]
public class TimeStamp
{
    public int timeStamp { get; set; }
    public List<Groups> groups { get; set; }

    public TimeStamp(int timeStamp, List<Groups> groups)
    {
        this.timeStamp = timeStamp;
        this.groups = groups;
    }

    public TimeStamp() { }

}


public class Groups
{
    public int distance { get; set; }
    public string gap { get; set; }
    public List<Athletes> athletes { get; set; }
    public int no { get; set; }

    public Groups(int distance, string gap, List<Athletes> athletes, int no)
    {
        this.distance = distance;
        this.gap = gap;
        this.athletes = athletes;
        this.no = no;
    }

    public Groups() { }
}

[Serializable]
public class Athletes
{
    public string hr { get; set; }
    public string pow { get; set; }
    public int bib { get; set; }
    public int rank { get; set; }
    public string IRM { get; set; }
    public string gname { get; set; }
    public double lat { get; set; }
    public string nationality { get; set; }
    public string spd { get; set; }
    public double lon { get; set; }
    public int valid { get; set; }
    public string fname { get; set; }
    public string team { get; set; }
    public string cad { get; set; }

    public Athletes(string hr, string pow, int bib, int rank, string IRM, string gname, double lat, string nationality, string spd, double lon, int valid, string fname, string team, string cad)
    {
        this.hr = hr;
        this.pow = pow;
        this.bib = bib;
        this.rank = rank;
        this.IRM = IRM;
        this.gname = gname;
        this.lat = lat;
        this.nationality = nationality;
        this.spd = spd;
        this.lon = lon;
        this.valid = valid;
        this.fname = fname;
        this.team = team;
        this.cad = cad;
    }

    // this constructor have no rank (due to wrong data but I prefer to keep it instead of rewriting it maybe later), normally it should not be used anymore
    public Athletes(string hr, string pow, int bib, string IRM, string gname, double lat, string nationality, string spd, double lon, int valid, string fname, string team, string cad)
    {
        this.hr = hr;
        this.pow = pow;
        this.bib = bib;
        this.IRM = IRM;
        this.gname = gname;
        this.lat = lat;
        this.nationality = nationality;
        this.spd = spd;
        this.lon = lon;
        this.valid = valid;
        this.fname = fname;
        this.team = team;
        this.cad = cad;
    }

    public Athletes() { }
}

public class Cartesian
{
    public Athletes athlete { get; set; }
    public double x { get; set; }
    public double y { get; set; }

    public Cartesian(Athletes athlete, double x, double y)
    {
        this.athlete = athlete;
        this.x = x;
        this.y = y;
    }
}

