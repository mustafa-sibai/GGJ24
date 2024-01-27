using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BananaJar : MonoBehaviour
{
    [SerializeField]float totalNumberOfBananas;
    [SerializeField] float currentNumberOfBananas;
    public Sprite noBananasForYou;
    public Sprite yesTakeBananas;
    private bool enteredBananaJar = false;

    [SerializeField] GameObject popUpBox;
    [SerializeField] GameObject numberOfBananasBox;
    [SerializeField] TMP_Text popUpBoxText;
    [SerializeField] TMP_Text numberOfBananasText;
    private string currentText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enteredBananaJar = true;
            numberOfBananasBox.SetActive(true);
            popUpBox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enteredBananaJar = false;
        numberOfBananasBox.SetActive(false);
        popUpBox.SetActive(false);
    }

    private void Update()
    {
        popUpBoxText.text = currentText;
        numberOfBananasText.text = currentNumberOfBananas.ToString() + " / " + totalNumberOfBananas.ToString();
        if (currentNumberOfBananas > 0 )
        {
            currentText = "Press X to Pickup";
            if (Input.GetButtonDown("Fire3") && enteredBananaJar)
            {
                currentNumberOfBananas--;
            }
        }

        if (currentNumberOfBananas <= 0)
        {
            currentText = "NO MORE BANANAS";
        }
          
    }

}
