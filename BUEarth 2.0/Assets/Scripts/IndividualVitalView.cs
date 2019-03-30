using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualVitalView : MonoBehaviour
{
    public GameObject VitalsBlock; //The vitals block prefab that will be spawned
    public GameObject CurrentBlock = null; //The currently-showing vitals block
    public List<GameObject> PinnedBlocks; //Used for holding pinned blocks
    public float PinTime = 10f; //How long in seconds the blocks should show before dissapearing


    public void Update()
    {
        tickCoundown(); //Runs the timer down

        //Debug code for when voice commands don't work very well
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnBatteryBlock();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PinIndividualPanel();
        }
    }

    public void SpawnBatteryBlock()
    {
        Destroy(CurrentBlock); //Removes the current block, if any
        PinTime = 10f; //Resets the pin time
        CurrentBlock = Instantiate(VitalsBlock, new Vector3(1, 1, 10), new Quaternion(0, 0, 0, 0), GameObject.Find("VitalsCanvas").transform); //Spawns a new vitals block in the correct location
        CurrentBlock.tag = "IndividualPanel"; //Tags the block so it can be removed
        VitalsSlot CurrentSlot = CurrentBlock.GetComponent<VitalsSlot>();
        //Sets the data of the block to be accurate
        CurrentSlot.fillamount = DataController.data.data[0].cap_battery;
        CurrentSlot.value.text = DataController.data.data[0].cap_battery.ToString();
        CurrentSlot.title.text = "Battery";
        CurrentSlot.subTitle.text = "Time Remaining";
        //Turns on the correct visual icon for the block
        CurrentBlock.transform.GetChild(0).gameObject.SetActive(true);
        CurrentBlock.transform.GetChild(0).GetComponent<PieMeterController>().SetProgressPercentage(DataController.data.data[0].cap_battery);
        CurrentBlock.transform.GetChild(1).gameObject.SetActive(false);

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
        GameObject[] toRemoveArray = PinnedBlocks.ToArray();
        PinnedBlocks.RemoveAt(0);
        Destroy(toRemoveArray[0]);
    }
}
