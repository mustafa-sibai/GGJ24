using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float startingTimer;
    [SerializeField] TMP_Text winnerText;
    float currentTimer;

    MonkeyController[] monkeyControllers;

    bool pickWinner = false;

    void Start()
    {
        currentTimer = startingTimer;
        monkeyControllers = FindObjectsOfType<MonkeyController>();
    }

    void Update()
    {
        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0 && !pickWinner)
        {
            MonkeyController monkeyWithMostHp = monkeyControllers[0];

            for (int i = 1; i < monkeyControllers.Length; i++)
            {
                if (monkeyWithMostHp.health > monkeyControllers[i].health)
                {
                    monkeyWithMostHp = monkeyControllers[i];
                }
            }

            winnerText.text = $"Winner is {monkeyWithMostHp.playerNumber}";
            winnerText.gameObject.SetActive(true);
            pickWinner = true;
        }
    }
}