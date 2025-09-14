using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button backButton;

    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation> ();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        backButton.gameObject.SetActive(true);
        anim.Play("OptionSlider");
    }

    public void Back()
    {
        backButton.gameObject.SetActive(false);
        anim.Play("ReverseOptionSilder");
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
