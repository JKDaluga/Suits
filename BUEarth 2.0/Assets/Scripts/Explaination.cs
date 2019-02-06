using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explaination : MonoBehaviour
{
    public GameObject WhatIsPanel;
    public Image WhatIsPicture;


    // Use this for initialization
    void Start()
    {      
        WhatIsPanel = GameObject.FindWithTag("WhatIsPanel");
        WhatIsPicture = GameObject.FindWithTag("WhatIsPicture").GetComponent<Image>();
    }
    
    public void WhatIs(string path)
    {
        WhatIsPicture.sprite = Resources.Load<Sprite>(path);
        WhatIsPanel.SetActive(true);
        Invoke("WhatIsOff", 5);
    }

    public void WhatIsOff()
    {
        WhatIsPanel.SetActive(false);
    }
}
