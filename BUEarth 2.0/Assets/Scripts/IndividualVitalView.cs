using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualVitalView : MonoBehaviour
{
    public GameObject VitalsBlock;
    public GameObject CurrentBlock = null;
    public List<GameObject> PinnedBlocks;
    public float PinTime = 10f;

    public void Update()
    {
        tickCoundown();

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
        Destroy(CurrentBlock);
        PinTime = 10f;
        CurrentBlock = Instantiate(VitalsBlock, new Vector3(0, 2, 4), new Quaternion(0, 0, 0, 0), GameObject.Find("VitalsCanvas").transform);
        CurrentBlock.GetComponent<VitalsSlot>().fillamount = DataController.data.data[0].cap_battery;
        CurrentBlock.GetComponent<VitalsSlot>().title.text = "Battery Capacity";
        CurrentBlock.GetComponent<VitalsSlot>().subTitle.text = "HH:MM:SS";
        CurrentBlock.transform.GetChild(0).gameObject.SetActive(true);
        CurrentBlock.transform.GetChild(0).GetComponent<PieMeterController>().SetProgressPercentage(DataController.data.data[0].cap_battery);
        CurrentBlock.transform.GetChild(1).gameObject.SetActive(false);

    }

    private void tickCoundown()
    {
        if (CurrentBlock != null)
        {
            PinTime = PinCountdown(PinTime);

            if (PinTime <= 0)
            {
                Destroy(CurrentBlock);
            }
        }
    }

    private void PinIndividualPanel()
    {
        if (CurrentBlock != null)
        {
            PinnedBlocks.Add(CurrentBlock);
            CurrentBlock = null;
        }
    }

    private float PinCountdown(float timerSeconds)
    {
        timerSeconds -= Time.deltaTime;

        return timerSeconds;
    }
}
