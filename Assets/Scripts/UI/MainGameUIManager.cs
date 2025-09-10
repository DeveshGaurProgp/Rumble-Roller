using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    private Animation anim;
    public Button pause;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void PauseGame()
    {
        pause.gameObject.SetActive(false);
        anim.Play("MainUI");
    }

    public void ResumeGame()
    {
        anim.Play("MainUIReverse");
        StartCoroutine(SetActive());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(0.2f);
        pause.gameObject.SetActive(true);
    }
}
