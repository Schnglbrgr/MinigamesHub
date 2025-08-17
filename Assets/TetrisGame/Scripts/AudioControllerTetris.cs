using UnityEngine;

public class AudioControllerTetris : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip levelUp;
    public AudioClip gameOver;
    public AudioClip selectPiece;
    public AudioClip loseHeart;
    public AudioClip destroyRow;

    public void MakeSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
