using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings_Manager : MonoBehaviour
{
    public GameObject startScreenCanvas;

    public void TurnOnSettingMenu()
    {
        startScreenCanvas.SetActive(false);
        gameObject.SetActive(true);

        Game_Manager.instance.Pause();
    }

    public void SoundOn()
    {
        AudioListener.volume = 1;
    }

    public void SoundOff()
    {
        AudioListener.volume = 0;
    }
}
