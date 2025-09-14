using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private float currentDuration;
    private bool volOn = true;

    public Sprite volOnImage;
    public Sprite volOffImage;

    private AudioSource audio;
    private Image image;
    private Slider volControl;

    void Start()
    {
        audio = GetComponent<AudioSource> ();
        image = GetComponent<Image>();
        volControl = GameObject.Find("Slider").GetComponent<Slider>();

        volControl.value = 1f;
    }

    void Update()
    {
        audio.volume = volControl.value;
    }

    public void VolOnOff()
    {
        if (volOn)
        {
            volOn = !volOn;
            currentDuration = audio.time;
            audio.Stop ();
            image.sprite = volOffImage;
        }
        else
        {
            volOn = !volOn;
            audio.time = currentDuration;
            audio.Play ();
            image.sprite = volOnImage;
        }
    }
}
