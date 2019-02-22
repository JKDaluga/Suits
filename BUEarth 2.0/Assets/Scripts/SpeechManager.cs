using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

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
            this.BroadcastMessage("Continue");
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

        keywords.Add("Choose Procedure", () =>
        {
            print("Called Command: Choose Procedure");
            // Call the Menu method on every descendant object.
            this.BroadcastMessage("LoadProcedures");
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

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
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