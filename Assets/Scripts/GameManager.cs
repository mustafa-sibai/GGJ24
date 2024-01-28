using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int totalAlivePlayers;
    [SerializeField] float startingTimer;

    [SerializeField] TMP_Text winnerText;
    [SerializeField] TMP_Text timer;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip startAudioClip;
    [SerializeField] AudioClip musicAudioClip;

    public static bool gameStarted = false;

    float startTimer = 4;

    float currentTimer;

    MonkeyController[] monkeyControllers;

    bool pickWinner = false;

    void Start()
    {
        currentTimer = startingTimer;
        monkeyControllers = FindObjectsOfType<MonkeyController>();
        totalAlivePlayers = monkeyControllers.Length;

        audioSource.clip = startAudioClip;
        audioSource.Play();
    }

    void Update()
    {
        startTimer -= Time.deltaTime * 0.75f;

        if (startTimer <= 0 && !gameStarted)
        {
            gameStarted = true;
            audioSource.clip = musicAudioClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (gameStarted)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
                currentTimer = 0;

            timer.text = ((int)currentTimer).ToString();

            if ((currentTimer <= 0 || totalAlivePlayers <= 1) && !pickWinner)
            {
                MonkeyController monkeyWithMostHp = monkeyControllers[0];

                for (int i = 1; i < monkeyControllers.Length; i++)
                {
                    if (monkeyWithMostHp.health < monkeyControllers[i].health)
                    {
                        monkeyWithMostHp = monkeyControllers[i];
                    }
                }

                winnerText.text = $"Winner is {monkeyWithMostHp.playerNumber}";
                winnerText.gameObject.SetActive(true);
                pickWinner = true;
            }
        }
        else
        {
            timer.text = ((int)startTimer).ToString();
        }
    }
}