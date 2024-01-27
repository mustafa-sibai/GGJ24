using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaJar : MonoBehaviour
{
    [SerializeField]float totalNumberOfBananas;
    [SerializeField] float currentNumberOfBananas;
    public Sprite noBananasForYou;
    public Sprite yesTakeBananas;
    private bool enteredBananaJar = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enteredBananaJar = true;
          

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enteredBananaJar = false;

    }

    private void Update()
    {
        Debug.Log("Total banana count " + currentNumberOfBananas);
        if (Input.GetButtonDown("Fire3"))
        {
            if (currentNumberOfBananas > 0 && enteredBananaJar)
            {
                currentNumberOfBananas--;
            }

            if (currentNumberOfBananas <= 0)
            {
            }
        };
    }
}
