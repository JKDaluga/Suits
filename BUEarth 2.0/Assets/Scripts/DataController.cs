using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class DataCollection
{
    public SpaceData[] data;
    public SwitchData[] switchData;
    public HelpData[] helpData;
}

public class DataController : MonoBehaviour
{
    public VitalsText vitalsText;
    public GameObject AlertPanel;
    public Text AlertNum;
    public static DataCollection data;
    public static DataCollection sdata;
    public static ProcedureCollection pdata;
    public StepIterator stepIterator;
    //private string gameDataFileName = "SpaceData.json";
    //private string gameDataStepData = "Procedure1.json";
    private static string url = "https://bradleyinteractive.com/suits-api/procedure/";
    private static string vitalsUrl = "http://mercury-program.herokuapp.com/api/suit/recent";
    private static string switchUrl = "http://mercury-program.herokuapp.com/api/suitswitch/recent";
    private static string HelpUrl = "https://bradleyinteractive.com/suits-api/objects";
    private static bool isGetData = true;
    private static string proceduresUrl = "https://bradleyinteractive.com/suits-api/procedures";
    public string alertMessege;
    public GameObject CriticalAlertPanel;
    public Text alertText;
    private bool[] savedAlerts = new bool[7];
    private List<string> alertList = new List<string>();
    public Alert[] alertTexts;
    private bool isNewAlert = false;
    private SpeechManager speechManager;

    // Use this for initialization
    void Start()
    {
        //DataController.data = JsonUtility.FromJson<DataCollection>(LoadData(gameDataFileName));
        //StartCoroutine(getData());
        //speechManager = GameObject.FindWithTag("Screens").GetComponent<SpeechManager>();
        StartCoroutine(getHelpData());
        StartCoroutine(getVitalsData());
        StartCoroutine(getProcedureData());
        StartCoroutine(getSwitchData());
    }

    IEnumerator getData(int i)
    {
        using (WWW www = new WWW(url + i))
        {
            yield return www;
            DataController.sdata = JsonUtility.FromJson<DataCollection>("{\"data\":" + www.text + "}");
            print(DataController.sdata);
            stepIterator.gameObject.SetActive(true);
            stepIterator.reset();
        }
    }

    IEnumerator getSwitchData()
    {
        while (isGetData)
        {
            using (WWW alerts = new WWW(switchUrl))
            {
                yield return alerts;
                DataController.data = JsonUtility.FromJson<DataCollection>("{\"switchData\":" + alerts.text + "}");
                int alertNum = checkAlert();
                if (alertNum > 0)
                {
                    if (isNewAlert)
                    {
                        alertText.text = alertMessege;
                        CriticalAlertPanel.SetActive(true);
                        AudioLibrary.AlertSound();
                    }
                    else CriticalAlertPanel.SetActive(false);
                    AlertNum.text = "" + alertNum;
                    AlertPanel.SetActive(true);
                    for(int i = 0; i < alertTexts.Length; i++)
                    {
                        if (alertList.Count > i)
                        {
                            alertTexts[i].panel.SetActive(true);
                            alertTexts[i].alertText.text = alertList[i];
                        }
                        else
                        {
                            alertTexts[i].panel.SetActive(false);
                        }
                    }
                    
                }
                else
                {
                    AlertPanel.SetActive(false);
                    CriticalAlertPanel.SetActive(false);
                }
            }
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator getProcedureData()
    {
        using (WWW www = new WWW(proceduresUrl))
        {
            yield return www;
            DataController.pdata = JsonUtility.FromJson<ProcedureCollection>("{\"procedures\":" + www.text + "}");
        }
    }

    IEnumerator getVitalsData()
    {
        using (WWW vitals = new WWW(vitalsUrl))
        {
            yield return vitals;
            DataController.data = JsonUtility.FromJson<DataCollection>("{\"data\":" + vitals.text + "}");
            vitalsText.UpdateText();
        }
        Invoke("vitalsCall", 1);
    }

    IEnumerator getHelpData()
    {
        using (WWW www = new WWW(HelpUrl))
        {
            yield return www;
            DataController.data = JsonUtility.FromJson<DataCollection>("{\"helpData\":" + www.text + "}");
        }
        //speechManager.addHelpKeywords();
    }

    private static string LoadData(string filename)
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, filename);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it

            return dataAsJson;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
            return "no data";
        }
    }

    public int checkAlert()
    {
        isNewAlert = false;
        int numAlerts = 0;
        if (DataController.data.switchData[0].sop_on)
        {
            numAlerts++;
            if (!savedAlerts[0])
            {
                isNewAlert = true;
                savedAlerts[0] = true;
                alertMessege = "Secondary Oxygen Pack is Active";
                alertList.Add(alertMessege);
            }
        }
        else
        {
            savedAlerts[0] = false;
            alertList.Remove("Secondary Oxygen Pack is Active");
        }
        if (DataController.data.switchData[0].sspe)
        {
            numAlerts++;
            if (!savedAlerts[1])
            {
                isNewAlert = true;
                savedAlerts[1] = true;
                alertMessege = "Spacesuit Pressure Emergency";
                alertList.Add(alertMessege);
            }
        }
        else
        {
            savedAlerts[1] = false;
            alertList.Remove("Spacesuit Pressure Emergency");
        }
        if (DataController.data.switchData[0].fan_error)
        {
            numAlerts++;
            if (!savedAlerts[2])
            {
                print("Fan Error Detected");
                isNewAlert = true;
                savedAlerts[2] = true;
                alertMessege = "Cooling Fan Failure";
                alertList.Add(alertMessege);
            }
        }
        else
        {
            savedAlerts[2] = false;
            alertList.Remove("Cooling Fan Failure");
        }
        if (DataController.data.switchData[0].vent_error)
        {
            numAlerts++;
            if (!savedAlerts[3])
            {
                isNewAlert = true;
                savedAlerts[3] = true;
                alertMessege = "No Ventilation Flow is Detected";
                alertList.Add(alertMessege);
            }
        }
        else
        {
            savedAlerts[3] = false;
            alertList.Remove("No Ventilation Flow is Detected");
        }
        if (DataController.data.switchData[0].vehicle_power)
        {
            numAlerts++;
            if (!savedAlerts[4])
            {
                isNewAlert = true;
                savedAlerts[4] = true;
                alertMessege = "Spacesuit is Receiving Power through Spacecraft";
                alertList.Add(alertMessege);
            }
        }
        else
        {
            savedAlerts[4] = false;
            alertList.Remove("Spacesuit is Receiving Power through Spacecraft");
        }
        if (DataController.data.switchData[0].h2o_off)
        {
            numAlerts++;
            if (!savedAlerts[5])
            {
                isNewAlert = true;
                savedAlerts[5] = true;
                alertMessege = "H20 System is Offline";
                alertList.Add(alertMessege);
            }
        }
        else
        {
            savedAlerts[5] = false;
            alertList.Remove("H20 System is Offline");
        }
        if (DataController.data.switchData[0].o2_off)
        {
            numAlerts++;
            if (!savedAlerts[6])
            {
                isNewAlert = true;
                savedAlerts[6] = true;
                alertMessege = "O2 System is Offline";
                alertList.Add(alertMessege);
            }
        }
        else
        {
            savedAlerts[6] = false;
            alertList.Remove("O2 System is Offline");
        }
        if (numAlerts > 1)
        {
            alertMessege = numAlerts + " Critical Errors";
        }

        return numAlerts;
    }

    public void LoadProcedure(int i)
    {
        StartCoroutine(getData(i));
    }

    public void vitalsCall()
    {
        StartCoroutine(getVitalsData());
    }

}
