using UnityEngine;

public class AudioControllerMazeRunner : MonoBehaviour
{
    public AudioSource musicSound;
    public AudioSource sfxSound;

    public AudioClip music;
    public AudioClip finalBoss;
    public AudioClip getHit;
    public AudioClip getHealed;
    public AudioClip shootPlayer;
    public AudioClip shootEnemy;
    public AudioClip collectWeapon;
    public AudioClip levelUp;
    public AudioClip emptyAmmo;
    public AudioClip gameOver;
    public AudioClip win;

    private void Awake()
    {
        musicSound.clip = music;
        musicSound.Play();
    }

    public void MakeSound(AudioClip clip)
    {
        sfxSound.PlayOneShot(clip);
    }
}
