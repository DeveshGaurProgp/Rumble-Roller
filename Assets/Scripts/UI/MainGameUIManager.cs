using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    private Animator animMenu;
    public Button pause;
    public Button Resume;

    private bool oneTime = true;

    private PlayerController playerControllerScript;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        animMenu = GetComponent<Animator>();
        animMenu.SetBool("GameStart", true);

        StartCoroutine(GameStarting());
    }

    void Update()
    {
        if(playerControllerScript.gameOver == true && oneTime)
        {
            StartCoroutine(GameOver());
            oneTime = false;
        }
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
        animMenu.SetBool("GameStart", false);
        Time.timeScale = 1f;
    }

    IEnumerator GameOver()
    {
        pause.gameObject.SetActive(false);
        animMenu.SetBool("Paused", true);
        Destroy(Resume.gameObject);
        yield return null;
    }
}