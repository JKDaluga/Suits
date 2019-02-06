using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalsText : MonoBehaviour {

    public Text tempText;
    public Text BatteryLife;
    public Text BatteryCapacity;
    public Text OxygenLife;
    public Text EVA;
    public Text H2OLife;
    public Text O2Pressure;
    public Text O2Rate;
    public Text H2OGas;
    public Text H2OLiquid;
    public Text SOPPressure;
    public Text SOPSuitRate;
    public Text InternalSuitPressure;
    public Text FanTachometer;
    public Text SUBPressure;
    public Text SUBTemp;
    public Text BPM;

    public void UpdateText()
    {
        tempText.text = "" + DataController.data.data[0].t_sub;
        BatteryLife.text = "" + DataController.data.data[0].t_battery;
        BatteryCapacity.text = "" + DataController.data.data[0].cap_battery + " Ah";
        OxygenLife.text = "" + DataController.data.data[0].t_oxygen;
        //EVA.text = "" + DataController.data.data[0].extraVehicularActivityTime;
        EVA.text = "";
        H2OLife.text = "" + DataController.data.data[0].t_water;
        O2Pressure.text = "" + DataController.data.data[0].p_o2 + " psia";
        O2Rate.text = "" + DataController.data.data[0].rate_o2 + " psi/min";
        H2OGas.text = "" + DataController.data.data[0].p_h2o_g + " psia";
        H2OLiquid.text = "" + DataController.data.data[0].p_h2o_l + " psia";
        SOPPressure.text = "" + DataController.data.data[0].p_sop + " psia";
        SOPSuitRate.text = "" + DataController.data.data[0].rate_sop + " psi/min";
        InternalSuitPressure.text = "" + DataController.data.data[0].p_suit + " psid";
        FanTachometer.text = "" + DataController.data.data[0].v_fan + " RPM";
        SUBPressure.text = "" + DataController.data.data[0].p_sub + " psia";
        SUBTemp.text = "" + DataController.data.data[0].t_sub + "°F";
        BPM.text = "" + DataController.data.data[0].heart_bpm + " BPM";
    }
}
