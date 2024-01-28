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
    private MonkeyController currentPlayer;

    [SerializeField] bool retrieveBanana = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enteredBananaJar = true;
            numberOfBananasBox.SetActive(true);
            popUpBox.SetActive(true);
            currentPlayer = other.GetComponent<MonkeyController>();
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
        numberOfBananasText.text = currentNumberOfBananas.ToString() + "/" + totalNumberOfBananas.ToString();


        if (enteredBananaJar)
        {
            if (currentNumberOfBananas > 0)
            {
                currentText = "A to Pickup, B to Return";
                if (Input.GetButtonDown("Fire1"))
                {
                    currentNumberOfBananas--;
                    currentPlayer.currentHeldBananas++;
                }
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if (currentPlayer.currentHeldBananas > 0)
                {
                    currentNumberOfBananas++;
                    currentPlayer.currentHeldBananas--;
                }
            }

            if (currentNumberOfBananas <= 0)
            {
                currentText = "NO MORE BANANAS";
            }
        }

     
    }
}
