using UnityEngine;

public class MusicScript : MonoBehaviour
{
    static bool initialized = false;

    AudioSource music;

    private void OnEnable()
    {
        if (initialized)
        {
            Destroy(this.gameObject);
        }
        else
        {
            music = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
            initialized = true;
        }

        //PlayMusic();
    }

    void PlayMusic()
    {
        if (!music.isPlaying)
        {
            music.Play();
        }
    }
}
