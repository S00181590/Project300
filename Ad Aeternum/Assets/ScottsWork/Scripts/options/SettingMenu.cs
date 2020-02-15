using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    public GameOptions gameOptions;

    public Toggle fullScreenToggle;
    public Dropdown resoutiondropdownsetting;
    public Dropdown TextureQualitySettings;
    public Slider MusicVolumeSlider;

    public Resolution[] resolutions; //stores all resoultions and returns


    public Button buttonapply;
    public AudioSource Musicsources;

    void OnEnable()
    {
        gameOptions = new GameOptions();

        fullScreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });
        resoutiondropdownsetting.onValueChanged.AddListener(delegate { setResoultion(); });
        TextureQualitySettings.onValueChanged.AddListener(delegate { setTextureQuality(); });
        MusicVolumeSlider.onValueChanged.AddListener(delegate { SettingtheVolume(); });
        buttonapply.onClick.AddListener(delegate { OnButtonApplyClick(); });


        resoutiondropdownsetting.ClearOptions();
        List<string> resoultionoptions = new List<string>();


        resolutions =  Screen.resolutions;
       
        foreach (Resolution resolution in resolutions)
        {
            resoutiondropdownsetting.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    public void setResoultion()
    {
        Screen.SetResolution(resolutions[resoutiondropdownsetting.value].width, resolutions[resoutiondropdownsetting.value].height, Screen.fullScreen);
        gameOptions.ResolutionIndex = resoutiondropdownsetting.value;
    }

    public void SettingtheVolume()
    {
        Musicsources.volume = gameOptions.MusicVolume = MusicVolumeSlider.value;
    }

    public void SetFullScreen()
    {

       gameOptions.FullScreen = Screen.fullScreen = fullScreenToggle.isOn;
        
    }

    public void setTextureQuality()
    {
        QualitySettings.masterTextureLimit = TextureQualitySettings.value;
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameOptions,true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }
    public void OnButtonApplyClick()
    {
        SaveSettings();
    }
    public void LoadSettings()
    {
        gameOptions = JsonUtility.FromJson<GameOptions>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        MusicVolumeSlider.value = gameOptions.MusicVolume;
        resoutiondropdownsetting.value = gameOptions.ResolutionIndex;
        TextureQualitySettings.value = gameOptions.TextureQualtity;
        fullScreenToggle.isOn = gameOptions.FullScreen;
        Screen.fullScreen = gameOptions.FullScreen;
        resoutiondropdownsetting.RefreshShownValue();
    }

}
