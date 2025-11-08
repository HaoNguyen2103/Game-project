using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get => instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource playerSource;
    public AudioSource enemySource;
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip playerDeathClip;
    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.volume = DataManager.DataMusic;
        sfxSource.volume = DataManager.DataSfx;
        PlayBackgroundMusic();
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayPlayerDeath()
    {
        if (playerDeathClip != null)
        {
            playerSource.PlayOneShot(playerDeathClip);
        }
    }
    public void PlayEnemysfxmusic(AudioClip clip)
    {
        enemySource.PlayOneShot(clip);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        DataManager.DataMusic = volume;
    }
    //public void SetPlayerVolume(float volume) {
    //playerSource.volume = volume;
    //}
    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
        enemySource.volume = volume;
        playerSource.volume = volume;
        DataManager.DataSfx = volume;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
