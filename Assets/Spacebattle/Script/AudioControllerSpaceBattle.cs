using UnityEngine;

public class AudioControllerSpaceBattle : MonoBehaviour
{
    [Header ("----AudioSource----")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("----MusicSound----")]
    public AudioClip bg;
    public AudioClip bossFight;

    [Header("----AudioClip----")]
    public AudioClip getHit;
    public AudioClip shoot;
    public AudioClip gameOver;
    public AudioClip pickPowerUp;

    private void Awake()
    {
        musicSource.clip = bg;
        musicSource.Play();
    }
    public void MakeSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
