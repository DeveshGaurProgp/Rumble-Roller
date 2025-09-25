using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    private Animator anim;
    public Button pause;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PauseGame()
    {
        anim.SetBool("Paused", true);
        pause.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        anim.SetBool("Paused" , false);
        Time.timeScale = 1f;
        pause.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}