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

    public void HandleMasterSliderValueChange()
    {
        mixer.SetFloat("Master", (masterSlider.value * 80) - 80);
    }
    public void HandleBGMSliderValueChange()
    {
        mixer.SetFloat("BGM", (bgmSlider.value * 80) - 80);
    }
    public void HandleSFXSliderValueChange()
    {
        mixer.SetFloat("SFX", (sfxSlider.value * 80) - 80);
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
        HandleMasterSliderValueChange();
        HandleBGMSliderValueChange();
        HandleSFXSliderValueChange();
    }

    public Vector3 GetVolumeMixer()
    {
        return new Vector3(masterSlider.value, bgmSlider.value, sfxSlider.value);
    }

    public void UI_Start()
    {
        gameOptionButton.UI_Start();
        autoWaveButton.UI_Start();
    }

    public void UI_Initialize()
    {
        gameOptionButton.isToggled = false;
        gameOptionButton.SetToggleState(false);
        autoWaveButton.SetToggleState(GameManager.Instance.autoWaveStart);
    }
}
