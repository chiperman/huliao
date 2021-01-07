using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    // Use this for initialization
    public GameObject pauseMenu;
    public AudioMixer audioMixer;

    public void PauseGame()
    {
        Debug.Log("按下暂停按钮");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGmae()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }


    public void onLoginButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
