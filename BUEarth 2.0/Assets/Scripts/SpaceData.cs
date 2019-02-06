using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpaceData
{
    public string instruction;
    public string tip;
    public string image;

    public double p_suit;
    public string t_battery;
    public string t_oxygen;
    public string t_water;
    public double p_sub;
    public double t_sub;
    public double v_fan;
    //public string extraVehicularActivityTime;
    public int p_o2;
    public double rate_o2;
    public int cap_battery;
    public double p_h2o_g;
    public double p_h2o_l;
    public double p_sop;
    public double rate_sop;
    public int heart_bpm;
}

[System.Serializable]
public class SwitchData
{
    public bool sop_on;
    public bool sspe;
    public bool fan_error;
    public bool vent_error;
    public bool vehicle_power;
    public bool h2o_off;
    public bool o2_off;
}

[System.Serializable]
public class HelpData
{
    public string name;
    public string path;
}