using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualVitalView : MonoBehaviour
{
    public GameObject VitalsBlock; //The vitals block prefab that will be spawned
    public GameObject CurrentBlock = null; //The currently-showing vitals block
    public List<GameObject> PinnedBlocks; //Used for holding pinned blocks
    public float PinTime = 10f; //How long in seconds the blocks should show before dissapearing
    private bool isPie = false;
    private float tempFillAmount = 0;
    private string[] timeArray;


    public void Update()
    {
        tickCoundown(); //Runs the timer down
    }

    public void SpawnBlock(string type)
    {
        Debug.Log("Type: " + type);
        isPie = false;
        AudioLibrary.OpenMenuSFX();
        Destroy(CurrentBlock); //Removes the current block, if any
        PinTime = 10f; //Resets the pin time
        CurrentBlock = Instantiate(VitalsBlock, new Vector3(-2.5f, 1.5f, 10), new Quaternion(0, 0, 0, 0), GameObject.Find("VitalsCanvas").transform); //Spawns a new vitals block in the correct location
        CurrentBlock.transform.localScale *= 2;
        CurrentBlock.tag = "IndividualPanel"; //Tags the block so it can be removed
        VitalsSlot CurrentSlot = CurrentBlock.GetComponent<VitalsSlot>();
        //Sets the data of the block to be accurate

        switch (type)
        {

            
            case "cap_battery":
                tempFillAmount = DataController.data.data[0].cap_battery / 30f;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].cap_battery.ToString();
                CurrentSlot.title.text = "Battery";
                CurrentSlot.subTitle.text = "Capacity";
                isPie = true;
                break;

            case "p_suit":
                float suitp = DataController.data.data[0].p_suit;
                suitp -= 3;
                suitp /= 1 / 0.7f;
                tempFillAmount = suitp / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].p_suit.ToString();
                CurrentSlot.title.text = "Battery";
                CurrentSlot.subTitle.text = "Time Remaining";
                break;

            case "t_battery":
                timeArray = DataController.data.data[0].t_battery.Split(':');
                float seconds = float.Parse(timeArray[0]) * 3600 + float.Parse(timeArray[1]) * 60 + float.Parse(timeArray[2]);
                CurrentSlot.fillamount = seconds / 36000;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                CurrentSlot.value.text = DataController.data.data[0].t_battery.ToString();
                CurrentSlot.title.text = "Battery";
                CurrentSlot.subTitle.text = "Time Remaining";
                isPie = true;
                break;

            case "t_oxygen":
                timeArray = DataController.data.data[0].t_oxygen.Split(':');
                seconds = float.Parse(timeArray[0]) * 3600 + float.Parse(timeArray[1]) * 60 + float.Parse(timeArray[2]);
                tempFillAmount = seconds /*float.Parse(DataController.data.data[0].t_water)*/ / 36000f;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].t_oxygen.ToString();
                CurrentSlot.title.text = "Battery";
                CurrentSlot.subTitle.text = "Time Remaining";
                isPie = true;
                break;

            case "t_water":
                timeArray = DataController.data.data[0].t_water.Split(':');
                seconds = float.Parse(timeArray[0]) * 3600f + float.Parse(timeArray[1]) * 60f + float.Parse(timeArray[2]);
                tempFillAmount = seconds / 36000f;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].t_water.ToString();
                CurrentSlot.title.text = "Water";
                CurrentSlot.subTitle.text = "Time Remaining";
                isPie = true;
                break;

            case "p_sub":
                float subp = DataController.data.data[0].p_sub;
                subp -= 2.7f;
                tempFillAmount = subp / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].p_sub.ToString();
                CurrentSlot.title.text = "Sub";
                CurrentSlot.subTitle.text = "SubPressure";
                break;

            case "t_sub":
                CurrentSlot.fillamount = DataController.data.data[0].cap_battery / 30f;
                CurrentSlot.value.text = DataController.data.data[0].cap_battery.ToString();
                CurrentSlot.title.text = "Battery";
                CurrentSlot.subTitle.text = "Time Remaining";
                isPie = true;
                break;

            case "v_fan":
                float fanSpeed = DataController.data.data[0].v_fan;
                fanSpeed -= 25000;
                fanSpeed /= 15000 / 0.7f;
                tempFillAmount = fanSpeed / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].v_fan.ToString();
                CurrentSlot.title.text = "Fan";
                CurrentSlot.subTitle.text = "Current RPM";
                break;

            case "p_o2":
                tempFillAmount = DataController.data.data[0].p_o2 / 2 + 0.5f;
                if (tempFillAmount > .9) tempFillAmount = .9f;
                else if (tempFillAmount < .1f) tempFillAmount = .1f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].p_o2.ToString();
                CurrentSlot.title.text = "Oxygen";
                CurrentSlot.subTitle.text = "Current Pressure";
                isPie = true;
                break;

            case "rate_o2":
                float o2r = DataController.data.data[0].rate_o2;
                o2r -= 0.75f;
                o2r /= 0.25f / 0.7f;
                tempFillAmount = o2r / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].rate_o2.ToString();
                CurrentSlot.title.text = "Oxygen";
                CurrentSlot.subTitle.text = "Rate";
                break;

            case "p_h2o_g":
                float h20pg = DataController.data.data[0].p_h2o_g;
                h20pg -= 15;
                h20pg /= 1 / 0.7f;
                tempFillAmount = h20pg / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].p_h2o_g.ToString();
                CurrentSlot.title.text = "H2O";
                CurrentSlot.subTitle.text = "Gas Pressure";
                break;

            case "p_h2o_l":
                float h20pl = DataController.data.data[0].p_h2o_g;
                h20pl -= 15;
                h20pl /= 1 / 0.7f;
                tempFillAmount = h20pl / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].cap_battery.ToString();
                CurrentSlot.title.text = "H2O";
                CurrentSlot.subTitle.text = "Liquid Pressure";
                break;

            case "p_sop":
                float sopp = DataController.data.data[0].p_sop;
                sopp -= 850;
                sopp /= 100 / 0.7f;
                tempFillAmount = sopp / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].p_sop.ToString();
                CurrentSlot.title.text = "SOP";
                CurrentSlot.subTitle.text = "Current Pressure";
                break;

            case "rate_sop":
                float sopr = DataController.data.data[0].rate_sop;
                sopr -= .75f;
                sopr /= .25f / 0.7f;
                tempFillAmount = sopr / 2 + 0.5f;
                CurrentSlot.fillamount = tempFillAmount;
                CurrentSlot.value.text = DataController.data.data[0].rate_sop.ToString();
                CurrentSlot.title.text = "SOP";
                CurrentSlot.subTitle.text = "Rate";
                break;

            case "heart_bpm":
                CurrentSlot.fillamount = DataController.data.data[0].cap_battery / 30f;
                CurrentSlot.value.text = DataController.data.data[0].cap_battery.ToString();
                CurrentSlot.title.text = "Battery";
                CurrentSlot.subTitle.text = "Time Remaining";
                isPie = true;
                break;

        }

        if (isPie)
        {
            //Turns on the correct visual icon for the block
            CurrentBlock.transform.GetChild(0).gameObject.SetActive(true);
            CurrentBlock.transform.GetChild(0).GetComponent<PieMeterController>().SetProgressPercentage(tempFillAmount);
            CurrentBlock.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            CurrentBlock.transform.GetChild(1).gameObject.SetActive(true);
            CurrentBlock.transform.GetChild(1).GetComponent<GaugeMeterController>().SetProgressPercentage(tempFillAmount);
            CurrentBlock.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    //Runs the timer down
    private void tickCoundown()
    {
        if (CurrentBlock != null) //Makes sure a block has been spawned
        {
            PinTime = PinCountdown(PinTime);

            if (PinTime <= 0) //If the time is 0, destory the current block
            {
                Destroy(CurrentBlock);
            }
        }
    }

    //Stick the current panel in place
    private void PinIndividualPanel()
    {
        if (CurrentBlock != null) //Makes sure there is a panel spawned to pin
        {
            PinnedBlocks.Add(CurrentBlock); //Adds the panel to the pinned list
            CurrentBlock.transform.SetParent(GameObject.Find("Menu").transform);
            CurrentBlock = null; //Resets the current panel
        }
    }

    //Actually modifies the timer's number
    private float PinCountdown(float timerSeconds)
    {
        timerSeconds -= Time.deltaTime;

        return timerSeconds;
    }


    //Removes the pinned panel being looked at
    private void RemoveFocusedPanel()
    {
        RaycastHit hit; //Sets up a RaycastHit to be used in the subsequent raycast
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 30.0f,
            Physics.DefaultRaycastLayers))
        {
            //Makes sure the object hit is an individual panel, and if so, destroys it
            if (hit.collider.gameObject.CompareTag("IndividualPanel"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    //Removes the first panel pinned, reguardless of gaze direction.
    private void RemoveFirstPanel()
    {
        if (PinnedBlocks.Count > 0)
        {
            GameObject[] toRemoveArray = PinnedBlocks.ToArray();
            PinnedBlocks.RemoveAt(0);
            Destroy(toRemoveArray[0]);
        }
    }
}
