using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    private Animator animMenu;
    public Button pause;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        animMenu = GetComponent<Animator>();

        animMenu.SetBool("GameStart", true);
        StartCoroutine(GameStarting());
    }

    public void PauseGame()
    {
        animMenu.SetBool("Paused", true);
        pause.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        animMenu.SetBool("Paused" , false);
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

    IEnumerator GameStarting()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 1f;
        animMenu.SetBool("GameStart", false);
    }
}