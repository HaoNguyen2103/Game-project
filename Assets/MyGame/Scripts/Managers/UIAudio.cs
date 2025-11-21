using UnityEngine;
using UnityEngine.UI;
public class UIAudio : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float musicVolume = DataManager.DataMusic * 100;
        float sfxVolume = DataManager.DataSfx * 100;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }
    public void SetMusic(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume / 100);
        DataManager.DataMusic = volume / 100;
    }
    public void SetSfx(float volume)
    {
        AudioManager.Instance.SetSfxVolume(volume / 100);
        DataManager.DataSfx = volume / 100;
    }
   
}
