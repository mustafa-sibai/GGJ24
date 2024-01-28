using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] panels;
    private int index = 0;
    private int currentPanelIndex;

    void Update()
    {
        
            panels[index].gameObject.SetActive(true);
        
    }

    public void NextPanel()
    {
        if (index < panels.Length)
        {
            panels[index].gameObject.SetActive(false);
            index++;
            panels[index].gameObject.SetActive(true);
        }
            
        
       
    }
}
