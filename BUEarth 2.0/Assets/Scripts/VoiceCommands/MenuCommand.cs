using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCommand : MonoBehaviour {
	public GameObject newVitalsPanel;
	public AnimationManager NewVitalsAnimation;
    public GameObject dimpanel;
    public bool isOpen;

    public GameObject helpPanel;

    // Use this for initialization
    void Start()
    {
		NewVitalsAnimation = GameObject.FindWithTag("Manager").GetComponent<AnimationManager>();

    }
	//vitals panel 
    void Menu()
    {
        if (!isOpen)
        {
            isOpen = true;
            newVitalsPanel.SetActive(true);
		    AudioLibrary.OpenMenuSFX ();
            dimpanel.SetActive(true);
        }
    }

    void CloseVitals()
    {
        newVitalsPanel.SetActive(false);
        if (isOpen)
        {
			isOpen = false;
            AudioLibrary.CloseMenuSFX();
		    //NewVitalsAnimation.SlideOut ();
            dimpanel.SetActive(false);
        }
    }

    void ToggleCommandsOn()
    {
        helpPanel.SetActive(true);
    }

    void ToggleCommandsOff()
    {
        helpPanel.SetActive(false);
    }
}
