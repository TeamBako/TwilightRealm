using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_OptionMenu : MonoBehaviour
{
    public UI_ToggleButton gameOptionButton;
    public UI_ToggleButton autoWaveButton;


    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public AudioMixer mixer;

    public void HandleMasterSliderValueChange(float value)
    {
        SetLevel("Master", value);
    }
    public void HandleBGMSliderValueChange(float value)
    {
        SetLevel("BGM", value);
    }
    public void HandleSFXSliderValueChange(float value)
    {
        SetLevel("SFX", value);
    }

    public void SetLevel(string name, float value)
    {
        mixer.SetFloat(name, Mathf.Log10(value) * 20);
    }

    public void SetAutoWaveStatus(UI_ToggleButton button)
    {
        GameManager.Instance.SetAutoWaveStatus(button.isToggled);
    }

    public void SetVolumeMixer(Vector3 soundLevel)
    {
        masterSlider.value = soundLevel.x;
        bgmSlider.value = soundLevel.y;
        sfxSlider.value = soundLevel.z; 
        HandleMasterSliderValueChange(masterSlider.value);
        HandleBGMSliderValueChange(bgmSlider.value);
        HandleSFXSliderValueChange(sfxSlider.value);
    }

    public Vector3 GetVolumeMixer()
    {
        return new Vector3(masterSlider.value, bgmSlider.value, sfxSlider.value);
    }

    public void UI_Start()
    {
        if (!SystemManager.Instance.isMenu)
        {
            gameOptionButton.UI_Start();
        }
        autoWaveButton.UI_Start();
    }

    public void UI_Initialize()
    {
        if (!SystemManager.Instance.isMenu)
        {
            gameOptionButton.isToggled = false;
            gameOptionButton.SetToggleState(false);
        }
        autoWaveButton.SetToggleState(GameManager.Instance.autoWaveStart);
    }
}
