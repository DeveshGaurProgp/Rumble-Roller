using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button backButton;

    private Animator menuAnim;

    void Start()
    {
        menuAnim = GetComponent<Animator>();
    }

    public void Play()
    {
        menuAnim.SetBool("SettingDifficulty", true);
        menuAnim.SetBool("BackToMenu", false);
        backButton.gameObject.SetActive(true);
    }

    public void Options()
    {
        menuAnim.SetBool("Settings", true);
        menuAnim.SetBool("BackToMenu", false);
        backButton.gameObject.SetActive(true);
    }

    public void Back()
    {
        menuAnim.SetBool("SettingDifficulty", false);
        menuAnim.SetBool("BackToMenu", true);
        menuAnim.SetBool("Settings", false);
        backButton.gameObject.SetActive(false);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
