using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public GameObject VitalsBlock;
    

    // Use this for initialization.
	// Voice commands
    void Start()
    {
        keywords.Add("View Vitals", () =>
        {
            print("Called Command: View Vitals");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("Menu");
        });

         keywords.Add("Show Vitals", () =>
        {
            print("Called Command: Show Vitals");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("Menu");
        });

        keywords.Add("Hide Vitals", () =>
        {
            print("Called Command: Hide Vitals");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("CloseVitals");
        });

         keywords.Add("Exit", () =>
        {
            print("Called Command: Exit Vitals");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("CloseVitals");
            this.BroadcastMessage("stopProcedure");
            this.BroadcastMessage("Continue");
            this.BroadcastMessage("DismissProcedureScreen");
        });

         keywords.Add("Close Vitals", () =>
        {
            print("Called Command: Close the Vitals");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("CloseVitals");
        });

        keywords.Add("Continue", () =>
        {
            print("Called Command: Continue");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("LoadProcedures");
        });

        keywords.Add("Stop Procedure", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("stopProcedure");
        });

        keywords.Add("Next Step", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("next");
        });

        keywords.Add("Go Back", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("prev");
        });

        keywords.Add("Reset", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("reset");
        });

        keywords.Add("Start Procedure One", () =>
        {
            int i = 2;
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("StartProcedure", i);
        });

        keywords.Add("Start Procedure Two", () =>
        {
            int i = 3;
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("StartProcedure", i);
        });
        keywords.Add("Start Procedure Three", () =>
        {
            int i = 0;
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("StartProcedure", i);
        });

        keywords.Add("Choose Procedure", () =>
        {
            print("Called Command: Choose Procedure");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("LoadProcedures");
        });

        keywords.Add("Next Procedures", () =>
        {
            print("Called Command: Next Procedures");

            this.BroadcastMessage("NextProcedures");
        });

        keywords.Add("Previous Procedures", () =>
        {
            print("Called Command: Previous Procedures");

            this.BroadcastMessage("PreviousProcedures");
        });

        keywords.Add("Hide Instructions", () =>
			{
				print("hiding the display");
				// Call the Menu method on every descendant object.
				this.BroadcastMessage("HideInstructions");
			});

		keywords.Add("Show Instructions", () =>
			{
				print("Show the display");
				// Call the Menu method on every descendant object.
				this.BroadcastMessage("ShowInstructions");
			});

        keywords.Add("View Alerts", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("ShowErrors");
        });

        keywords.Add("Show Alerts", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("ShowErrors");
        });

        keywords.Add("Hide Alerts", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("HideErrors");
        });

        keywords.Add("Repeat", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("repeat");
        });

        keywords.Add("Place Panel", () =>
        {
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("pinProcedure");
        });

        keywords.Add("build catapult", () =>
        {
            print("Called Command:  Build");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("StartProcedure", 1);
        });

        //view battery capacity time
        keywords.Add("view bat cap", () =>
        {
            print("Called Command: view bat cap");
            this.BroadcastMessage("SpawnBatteryBlock");
        });

        //Make the current individual vitals panel stay in place
        keywords.Add("pin bat cap", () =>
        {
            this.BroadcastMessage("PinIndividualPanel");
        });

        //Remove the individual vitals panel being looked at
        keywords.Add("remove panel", () =>
        {
            this.BroadcastMessage("RemoveFocusedPanel");
        });

        //Remove the first pinned vitals panel
        keywords.Add("remove first panel", () =>
        {
            this.BroadcastMessage("RemoveFirstPanel");
        });
        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

        foreach (KeyValuePair<string, string> entry in VitalsText.VitalsToCall)
        {
            keywords.Add("View " + entry.Key, () =>
            {
                print("Called Command: View " + entry.Key);
                this.BroadcastMessage("SpawnBlock", entry.Key);
            });
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    public void addHelpKeywords()
    {
        //Not functional
        //Attempted to create voice commands for a help panel this might not be where the error is but is
        foreach (HelpData data in DataController.data.helpData)
        {
            keywords.Add("What Is " + data.name, () =>
            {
                // Call the Menu method on every descendant object.
                this.BroadcastMessage("WhatIs(data.path)");
            });
        }
    }
}