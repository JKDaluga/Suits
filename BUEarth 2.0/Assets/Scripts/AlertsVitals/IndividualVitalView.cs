using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualVitalView : MonoBehaviour
{
    private const float PIN_TIME = 10f; //How long in seconds the blocks should show before dissapearing
    public GameObject Oxygen; //The list of vitals block prefab that will be spawned
    public GameObject H2O;
    public GameObject Battery;
    public GameObject Suit;
    public GameObject Sub;
    public GameObject SOP;
    public GameObject Fan;
    public GameObject CurrentBlock = null; //The currently-showing vitals block
    public List<GameObject> PinnedBlocks; //Used for holding pinned blocks
    public float PinTime = PIN_TIME; //Countdown timer for pinning the vital block
    public GameObject helpText;


    public void Update()
    {
        tickCoundown(); //Runs the timer down
    }

    public void SpawnBlock(string type)
    {
        bool isPie = false;
        Debug.Log("Type: " + type);
        float seconds = 0;
        float tempFillAmount = 0;
        AudioLibrary.OpenMenuSFX();
        Destroy(CurrentBlock); //Removes the current block, if any
        PinTime = PIN_TIME; //Resets the pin time
        helpText.SetActive(true);
        string[] timeArray;
        //Abbreviation for the current block. Easier to work with than the full name
        string currentBlock = "";
        //Amount of sub-panels in current block
        int subPanelAmount = 0;
        //Spawns the panel of the given type
        CurrentBlock = SpawnPanel(type);

        switch (type)
        {

            
            case "Battery":
                //Sets the data of the block to be accurate

                timeArray = DataController.data.data[0].t_battery.Split(':');
                /*
                seconds = float.Parse(timeArray[0]) * 3600 + float.Parse(timeArray[1]) * 60 + float.Parse(timeArray[2]);
                tempFillAmount = seconds / 36000;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                */
                tempFillAmount = CalculateTempFillFromTime(timeArray);
                currentBlock = "bat";
                subPanelAmount = 2;
                //int tempOrder = 11;
                CurrentBlock.transform.Find("VitalsDataBlock (11)").GetComponent<VitalsSlot>().isPie = true;
                CurrentBlock.transform.Find("VitalsDataBlock (11)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (11)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (11)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].t_battery;

                float batcap = DataController.data.data[0].cap_battery;
                tempFillAmount = DataController.data.data[0].cap_battery / 30f;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                //tempOrder = 12;
                CurrentBlock.transform.Find("VitalsDataBlock (12)").GetComponent<VitalsSlot>().isPie = true;
                CurrentBlock.transform.Find("VitalsDataBlock (12)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (12)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (12)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].cap_battery.ToString();

                break;

            case "H2O":
                //Sets the data of the block to be accurate

                timeArray = DataController.data.data[0].t_water.Split(':');
                /*
                seconds = float.Parse(timeArray[0]) * 3600f + float.Parse(timeArray[1]) * 60f + float.Parse(timeArray[2]);
                tempFillAmount = seconds / 36000f;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                */
                tempFillAmount = CalculateTempFillFromTime(timeArray);
                //tempOrder = 3;
                currentBlock = "h2o";
                subPanelAmount = 3;
                CurrentBlock.transform.Find("VitalsDataBlock (3)").GetComponent<VitalsSlot>().isPie = true;
                CurrentBlock.transform.Find("VitalsDataBlock (3)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (3)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (3)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].t_water;

                float h20pg = DataController.data.data[0].p_h2o_g;
                h20pg -= 15;
                h20pg /= 1 / 0.7f;
                //tempOrder = 5;
                tempFillAmount = h20pg / 2 + 0.5f;

                CurrentBlock.transform.Find("VitalsDataBlock (4)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (4)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (4)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (4)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].p_h2o_g.ToString();

                float h20pl = DataController.data.data[0].p_h2o_l;
                h20pl -= 15;
                h20pl /= 1 / 0.7f;
                //tempOrder = 4;
                tempFillAmount = h20pl / 2 + 0.5f;

                CurrentBlock.transform.Find("VitalsDataBlock (5)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (5)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (5)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (5)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].p_h2o_l.ToString();


                break;

            case "Oxygen":
                //Sets the data of the block to be accurate
                currentBlock = "oxygen";
                subPanelAmount = 3;
                float o2p = DataController.data.data[0].p_o2;
                o2p -= 850;
                o2p /= (100 / 0.7f);
                //tempOrder = 2;
                tempFillAmount = o2p / 2 + 0.5f;
                if (tempFillAmount > .9) tempFillAmount = .9f;
                else if (tempFillAmount < .1f) tempFillAmount = .1f;
                CurrentBlock.transform.Find("VitalsDataBlock (1)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (1)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (1)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (1)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].p_o2.ToString();

                float o2r = DataController.data.data[0].rate_o2;
                o2r -= 0.75f;
                o2r /= 0.25f / 0.7f;
                //tempOrder = 1;
                tempFillAmount = o2r / 2 + 0.5f;
                CurrentBlock.transform.Find("VitalsDataBlock (2)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (2)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (2)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (2)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].rate_o2.ToString();

                timeArray = DataController.data.data[0].t_oxygen.Split(':');
                /*
                seconds = float.Parse(timeArray3[0]) * 3600 + float.Parse(timeArray3[1]) * 60 + float.Parse(timeArray3[2]);
                tempFillAmount = seconds /*float.Parse(DataController.data.data[0].t_water) / 36000f;
                if (tempFillAmount > 1) tempFillAmount = 1;
                else if (tempFillAmount < 0) tempFillAmount = 0;
                */
                tempFillAmount = CalculateTempFillFromTime(timeArray);
                //tempOrder = 0;
                CurrentBlock.transform.Find("VitalsDataBlock (0)").GetComponent<VitalsSlot>().isPie = true;
                CurrentBlock.transform.Find("VitalsDataBlock (0)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (0)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (0)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].t_oxygen;

                break;

            case "Suit":
                //Sets the data of the block to be accurate
                currentBlock = "suit";
                subPanelAmount = 1;
                float suitp = DataController.data.data[0].p_suit;
                suitp -= 3;
                suitp /= 1 / 0.7f;
                //tempOrder = 7;
                tempFillAmount = suitp / 2 + 0.5f;

                CurrentBlock.transform.Find("VitalsDataBlock (7)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (7)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (7)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (7)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].p_suit.ToString();

                break;

            case "SOP":
                //Sets the data of the block to be accurate
                currentBlock = "sop";
                subPanelAmount = 2;
                float sopp = DataController.data.data[0].p_sop;
                sopp -= 850;
                sopp /= 100 / 0.7f;
                //tempOrder = 9;
                tempFillAmount = sopp / 2 + 0.5f;

                CurrentBlock.transform.Find("VitalsDataBlock (8)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (8)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (8)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (8)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].p_sop.ToString();


                float sopr = DataController.data.data[0].rate_sop;
                sopr -= .75f;
                sopr /= .25f / 0.7f;
                //tempOrder = 8;
                tempFillAmount = sopr / 2 + 0.5f;

                CurrentBlock.transform.Find("VitalsDataBlock (9)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (9)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (9)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (9)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].rate_sop.ToString();

                break;

            case "Sub":
                //Sets the data of the block to be accurate
                currentBlock = "sub";
                subPanelAmount = 1;
                float subp = DataController.data.data[0].p_sub;
                subp -= 3;
                subp /= 1 / 0.7f;
                //tempOrder = 6;
                tempFillAmount = subp / 2 + 2f;

                CurrentBlock.transform.Find("VitalsDataBlock (6)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (6)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (6)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (6)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].p_sub.ToString();

                break;

            case "Fan":
                //Sets the data of the block to be accurate
                currentBlock = "fan";
                subPanelAmount = 1;
                float fanSpeed = DataController.data.data[0].v_fan;
                fanSpeed -= 25000;
                fanSpeed /= 15000 / 0.7f;
                //tempOrder = 10;
                tempFillAmount = fanSpeed / 10000;

                CurrentBlock.transform.Find("VitalsDataBlock (10)").GetComponent<VitalsSlot>().isPie = false;
                CurrentBlock.transform.Find("VitalsDataBlock (10)").GetComponent<VitalsSlot>().fillamount = tempFillAmount;
                CurrentBlock.transform.Find("VitalsDataBlock (10)").GetComponent<VitalsSlot>().subTitle.text = "Time Remaining";
                CurrentBlock.transform.Find("VitalsDataBlock (10)").GetComponent<VitalsSlot>().value.text = DataController.data.data[0].v_fan.ToString();

                break;

        }

        /*
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
        */
    }

    /// <summary>
    /// Takes a time in seconds and converts to to a 0-1 value for filling a graph
    /// </summary>
    /// <param name="seconds">What amount of seconds to convert</param>
    /// <returns>The amount to fill the graph</returns>
    private float CalculateTempFillFromTime(string[] timeArray)
    {
        float seconds = float.Parse(timeArray[0]) * 3600 + float.Parse(timeArray[1]) * 60 + float.Parse(timeArray[2]);
        float tempFillAmount = seconds / 36000;
        if (tempFillAmount > 1) tempFillAmount = 1;
        else if (tempFillAmount < 0) tempFillAmount = 0;

        return tempFillAmount;
    }
    
    /// <summary>
    /// Spawns an individual panel of a given type
    /// </summary>
    /// <param name="type">The type of panel to spawn</param>
    private GameObject SpawnPanel(string type)
    {
        GameObject toInstantiate = null;
        GameObject toReturn = null;
        //Loads the prefab from file
        toInstantiate = Resources.Load<GameObject>("Prefabs/Vitals/" + type);
        //Instantiates the block
        toReturn = GameObject.Instantiate(toInstantiate);
        //Sets the block's position to the upper right corner
        toReturn.transform.position = ((Camera.main.transform.forward * 10)
                                  + (2 * Camera.main.transform.right) + (2 * Camera.main.transform.up));
        toReturn.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Sets the block's parent so it is always n front of the user
        toReturn.transform.parent = GameObject.Find("VitalsCanvas").transform;
        toReturn.transform.localScale *= 0.005f;
        toReturn.tag = "IndividualPanel"; //Tags the block so it can be removed
        /*
        //Spawns a new vitals block in the correct location
        CurrentBlock = Instantiate(SOP, (Camera.main.transform.forward * 10)
                                        + (-2 * Camera.main.transform.right) + (-2 * Camera.main.transform.up),
            new Quaternion(0, 0, 0, 0),
            GameObject.Find("VitalsCanvas").transform);
        */
        return toReturn;
    }
    //Runs the timer down
    private void tickCoundown()
    {
        if (CurrentBlock != null) //Makes sure a block has been spawned
        {
            PinTime = PinCountdown(PinTime);

            if (PinTime <= 0) //If the time is 0, destory the current block
            {
                helpText.SetActive(false);
                Destroy(CurrentBlock);
            }
        }
    }

    //Stick the current panel in place
    private void PinIndividualPanel()
    {
        if (CurrentBlock != null) //Makes sure there is a panel spawned to pin
        {
            helpText.SetActive(false);
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
