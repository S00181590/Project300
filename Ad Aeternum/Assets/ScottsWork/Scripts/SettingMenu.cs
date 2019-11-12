using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resoutiondropdpwnsetting;

     void Start()
    {
     resolutions =  Screen.resolutions;
        resoutiondropdpwnsetting.ClearOptions();
        List<string> resoultionoptions = new List<string>();

        int CurrentResoultion = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resoultionoptions.Add(option);
            if(resolutions[i].height ==Screen.currentResolution.height &&
                resolutions[i].height ==Screen.currentResolution.width)
            {
                CurrentResoultion = i;

            }
        }
        resoutiondropdpwnsetting.AddOptions(resoultionoptions);
        resoutiondropdpwnsetting.value = CurrentResoultion;
        resoutiondropdpwnsetting.RefreshShownValue();
    }

    public void setResoultion (int Resoultionsetting)
    {
        Resolution resoultion = resolutions[Resoultionsetting];
        Screen.SetResolution(resoultion.width, resoultion.height, Screen.fullScreen);
    }
    public AudioMixer audioMixer;
    public void SettingtheVolume(float Volume)
    {
        audioMixer.SetFloat("volume", Volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
