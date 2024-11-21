using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTheme : MonoBehaviour
{
    public string theme;
    public void PlayTheme(string theme)
    {
        AudioManager.instance.PlayMusic(theme);
    }
    public void StopLoop()
    {
        AudioManager.instance.StopSFXLoop();
    }
}
