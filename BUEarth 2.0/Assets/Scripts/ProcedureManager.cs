using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Procedure
{
    public int steps;
    public int id;
    public string name;
}

[System.Serializable]
public class ProcedureCollection
{
    public Procedure[] procedures;
}

public class ProcedureManager : MonoBehaviour {

    public DataController dataController;
    public Image procedureScreen1;
    public Image procedureScreen2;
    public Text Procedure1Title;
    public Text Procedure2Title;
    public Text Procedure1Steps;
    public Text Procedure2Steps;
    public Image onboardingScreen;
    public AnimationManager onboardScreen;
    public AnimationManager FadeInProcedures;
    public AnimationManager FadeOutProcedures;
    public bool isActive = false;
    private int procedureIndex = 0;

    private void Start()
    {
        FadeInProcedures = GameObject.FindWithTag("Manager").GetComponent<AnimationManager>();
        FadeOutProcedures = GameObject.FindWithTag("Manager").GetComponent<AnimationManager>();
    }

    public void LoadProcedures()
    {
        onboardScreen.PlayAnimationFadeOut(onboardingScreen);
        Procedure1Title.text = DataController.pdata.procedures[procedureIndex].name;
        Procedure1Steps.text = "" + DataController.pdata.procedures[procedureIndex].steps + " Steps";
        if (procedureIndex + 1 < DataController.pdata.procedures.Length)
        {
            Procedure2Title.text = DataController.pdata.procedures[procedureIndex + 1].name;
            Procedure2Steps.text = "" + DataController.pdata.procedures[procedureIndex + 1].steps + " Steps";
        }
       
       

        //procedureScreen1.gameObject.SetActive(true);
        //procedureScreen2.gameObject.SetActive(true);
        FadeInProcedures.PlayAnimationFadeIn(procedureScreen1);
        FadeInProcedures.PlayAnimationFadeIn(procedureScreen2);
        isActive = true;
    }

    public void NextProcedures()
    {
        if(procedureIndex < (DataController.pdata.procedures.Length-2))
        {
            procedureIndex += 2;
            LoadProcedures();
        }
    }

    public void PreviousProcedures()
    {
        if (procedureIndex > 1)
        {
            procedureIndex -= 2;
            LoadProcedures();
        }
    }

    public void StartProcedure(int i)
    {
        if(isActive)
        {
            dataController.LoadProcedure(i);
            FadeOutProcedures.PlayAnimationFadeOut(procedureScreen1);
            FadeOutProcedures.PlayAnimationFadeOut(procedureScreen2);
            isActive = false;
        }
    }

    public void DismissProcedureScreen()
    {
        if (isActive)
        {
            FadeOutProcedures.PlayAnimationFadeOut(procedureScreen1);
            FadeOutProcedures.PlayAnimationFadeOut(procedureScreen2);
            isActive = false;
        }
    }
}
