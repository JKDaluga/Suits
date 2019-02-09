﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour {
    public GameObject panel;
    public Text alertText;

    private void Start() //JASON.reinstate("Francais");
    {
        panel = this.gameObject;
        alertText = this.GetComponentInChildren<Text>();
    }
}