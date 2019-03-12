using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class VitalsSlotData
{
    public string title;
    public string subTitle;
    public string value;
    public float fillamount;
    public float priority;
    public bool isPie;
}

public class VitalsText : MonoBehaviour {
    private const int NUM_VITALS = 8;
    public VitalsSlot[] vitalsSlots = new VitalsSlot[NUM_VITALS];
    public List<VitalsSlotData> priorityList = new List<VitalsSlotData>();

    public string tempText;
    public string BatteryLife;
    public string BatteryCapacity;
    public string OxygenLife;
    public string EVA;
    public string H2OLife;
    public string O2Pressure;
    public string O2Rate;
    public string H2OGas;
    public string H2OLiquid;
    public string SOPPressure;
    public string SOPSuitRate;
    public string InternalSuitPressure;
    public string FanTachometer;
    public string SUBPressure;
    public string SUBTemp;
    public string BPM;

    public void UpdateText()
    {
        priorityList.Clear();
        tempText = "" + DataController.data.data[0].t_sub;
        //EVA.text = "" + DataController.data.data[0].extraVehicularActivityTime;
        EVA = "";

        //Example Pie Meter
        string[] timeArray = DataController.data.data[0].t_battery.Split(':');
        print(float.Parse(timeArray[0]));
        float seconds = float.Parse(timeArray[0]) * 3600 + float.Parse(timeArray[1]) * 60 + float.Parse(timeArray[2]);
        float tempFillAmount = seconds / 36000;
        if (tempFillAmount > 1) tempFillAmount = 1;
        else if(tempFillAmount < 0) tempFillAmount = 0;
        float tempPriority = tempFillAmount;
        AddToList("Battery", "Time Remaining", DataController.data.data[0].t_battery, tempFillAmount, tempPriority, true);

        //Example Gauge Meter
        float o2p = DataController.data.data[0].p_o2;
        o2p -= 850;
        o2p /= 100;
        tempPriority = 1 - Mathf.Abs(o2p);
        tempFillAmount = o2p / 2 + 0.5f;
        AddToList("Oxygen", "Current Pressure", DataController.data.data[0].p_o2.ToString(), tempFillAmount, tempPriority, false);

        string[] timeArray2 = DataController.data.data[0].t_water.Split(':');
        seconds = float.Parse(timeArray2[0]) * 3600f + float.Parse(timeArray2[1]) * 60f + float.Parse(timeArray2 [2]);
        tempFillAmount = seconds / 36000f;
        if (tempFillAmount > 1) tempFillAmount = 1;
        else if (tempFillAmount < 0) tempFillAmount = 0;
        tempPriority = tempFillAmount;
        AddToList("H2O", "Time Remaining", DataController.data.data[0].t_water, tempFillAmount, tempPriority, true);

        float fanSpeed = DataController.data.data[0].v_fan;
        fanSpeed -=25000;
        fanSpeed /= 15000;
        tempPriority = 1 - Mathf.Abs(fanSpeed);
        tempFillAmount = fanSpeed / 2 + 0.5f;
        AddToList("Fan", "Current RPM", DataController.data.data[0].v_fan.ToString(), tempFillAmount, tempPriority, false);

        float subp = DataController.data.data[0].p_sub;
        subp -= 3;
        tempPriority = 1 - Mathf.Abs(subp);
        tempFillAmount = subp / 2 + 0.5f;
        AddToList("Sub", "SubPressure", DataController.data.data[0].p_sub.ToString(), tempFillAmount, tempPriority, false);

        float o2r = DataController.data.data[0].rate_o2;
        o2r -= 0.75f;
        o2r /= 0.25f;
        tempPriority = 1 - Mathf.Abs(o2r);
        tempFillAmount = o2r / 2 + 0.5f;
        AddToList("Oxygen", "Current Rate", DataController.data.data[0].rate_o2.ToString(), tempFillAmount, tempPriority, false);

        float batcap = DataController.data.data[0].cap_battery;
        tempFillAmount = DataController.data.data[0].cap_battery / 30f;
        if (tempFillAmount > 1) tempFillAmount = 1;
        else if (tempFillAmount < 0) tempFillAmount = 0;
        tempPriority = tempFillAmount;
        AddToList("Battery", "Capacity", DataController.data.data[0].cap_battery.ToString(), tempFillAmount, tempPriority, true);

        float h20pg = DataController.data.data[0].p_h2o_g;
        h20pg -= 15;
        h20pg /= 1;
        tempPriority = 1 - Mathf.Abs(h20pg);
        tempFillAmount = h20pg / 2 + 0.5f;
        AddToList("H2O", "Gas Pressure", DataController.data.data[0].p_h2o_g.ToString(), tempFillAmount, tempPriority, false);

        float h20pl = DataController.data.data[0].p_h2o_g;
        h20pl -= 15;
        h20pl /= 1;
        tempPriority = 1 - Mathf.Abs(h20pl);
        tempFillAmount = h20pl / 2 + 0.5f;
        AddToList("H2O", "Liquid Pressure", DataController.data.data[0].p_h2o_g.ToString(), tempFillAmount, tempPriority, false);

        float sopp = DataController.data.data[0].p_sop;
        sopp -= 850;
        sopp /= 100;
        tempPriority = 1 - Mathf.Abs(sopp);
        tempFillAmount = sopp / 2 + 0.5f;
        AddToList("SOP", "Current Pressure", DataController.data.data[0].p_sop.ToString(), tempFillAmount, tempPriority, false);

        float sopr = DataController.data.data[0].rate_sop;
        sopr -= .75f;
        sopr /= .25f;
        tempPriority = 1 - Mathf.Abs(sopr);
        tempFillAmount = sopr / 2 + 0.5f;
        AddToList("SOP", "Rate", DataController.data.data[0].rate_sop.ToString(), tempFillAmount, tempPriority, false);

        float suitp = DataController.data.data[0].p_suit;
        suitp -= 3;
        suitp /= 1;
        tempPriority = 1 - Mathf.Abs(suitp);
        tempFillAmount = suitp / 2 + 0.5f;
        AddToList("Suit", "Current Pressure", DataController.data.data[0].p_suit.ToString(), tempFillAmount, tempPriority, false);

        string[] timeArray3 = DataController.data.data[0].t_oxygen.Split(':');
        seconds = float.Parse(timeArray3[0]) * 3600 + float.Parse(timeArray3[1]) * 60 + float.Parse(timeArray3[2]);
        tempFillAmount = seconds /*float.Parse(DataController.data.data[0].t_water)*/ / 36000f;
        if (tempFillAmount > 1) tempFillAmount = 1;
        else if (tempFillAmount < 0) tempFillAmount = 0;
        tempPriority = tempFillAmount;
        AddToList("Oxygen", "Time Remaining", DataController.data.data[0].t_oxygen, tempFillAmount, tempPriority, true);

        /*BatteryCapacity = "" + DataController.data.data[0].cap_battery + " Ah";
        OxygenLife = "" + DataController.data.data[0].t_oxygen;
        
        H2OLife = "" + DataController.data.data[0].t_water;
       
        O2Rate = "" + DataController.data.data[0].rate_o2 + " psi/min";
        H2OGas = "" + DataController.data.data[0].p_h2o_g + " psia";
        H2OLiquid = "" + DataController.data.data[0].p_h2o_l + " psia";
        SOPPressure = "" + DataController.data.data[0].p_sop + " psia";
        SOPSuitRate = "" + DataController.data.data[0].rate_sop + " psi/min";
        InternalSuitPressure = "" + DataController.data.data[0].p_suit + " psid";
        FanTachometer = "" + DataController.data.data[0].v_fan + " RPM";
        SUBPressure = "" + DataController.data.data[0].p_sub + " psia";
        SUBTemp = "" + DataController.data.data[0].t_sub + "°F";
        BPM = "" + DataController.data.data[0].heart_bpm + " BPM";*/

        UpdateList();
    }

    void AddToList(string title, string subTitle, string value, float fillAmount, float priority, bool isPie)
    {
        //create the vitalslot
        VitalsSlotData tempVitalsSlot = new VitalsSlotData();
        tempVitalsSlot.fillamount = fillAmount;
        tempVitalsSlot.priority = priority;
        tempVitalsSlot.title = title;
        tempVitalsSlot.subTitle = subTitle;
        tempVitalsSlot.value = value;
        tempVitalsSlot.isPie = isPie;
        for (int i = 0; i < priorityList.Count; i++)
        {
            if(priority < priorityList[i].priority)
            {
                print(i);
                priorityList.Insert(i, tempVitalsSlot);
                return;
            }
        }
        priorityList.Add(tempVitalsSlot);
    }

    void UpdateList()
    {
        for(int i = 0; i < NUM_VITALS; i++)
        {
            vitalsSlots[i].title.text = priorityList[i].title;
            vitalsSlots[i].subTitle.text = priorityList[i].subTitle;
            vitalsSlots[i].value.text = priorityList[i].value;
            vitalsSlots[i].fillamount = priorityList[i].fillamount;
            //Set the graph to pie or gauge
            if(priorityList[i].isPie)
            {
                vitalsSlots[i].pie.gameObject.SetActive(true);
                vitalsSlots[i].pie.GetComponent<PieMeterController>().SetProgressPercentage(priorityList[i].fillamount);
                vitalsSlots[i].gauge.gameObject.SetActive(false);
            }
            else
            {
                vitalsSlots[i].gauge.gameObject.SetActive(true);
                vitalsSlots[i].gauge.GetComponent<GaugeMeterController>().SetProgressPercentage(priorityList[i].fillamount);
                vitalsSlots[i].pie.gameObject.SetActive(false);
            }
            print(priorityList[i].fillamount);
        }
    }
}
