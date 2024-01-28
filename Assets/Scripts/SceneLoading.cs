using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public string nextScene;
    Animator fadeInAndOut;

    private void Start()
    {
        Debug.Log("SceneTransition: Make sure this script is attached to the scene panel");
        fadeInAndOut = this.GetComponent<Animator>();
        fadeInAndOut.Play("FadeOut");
    }

    public void LoadTheScene()
    {
        fadeInAndOut.Play("FadeOut");
        SceneManager.LoadScene(nextScene);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            fadeInAndOut.Play("FadeIn");
        }
    }
}
