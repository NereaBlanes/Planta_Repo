using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Declaracion del Singleton
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null) Debug.Log("No hay AudioManager");
            return instance;
        }
    }
    //Fin del singleton

    public AudioSource musicSource;
    public AudioSource SFXsource;
    public AudioClip[] musicLibrary;
    public AudioClip[] sfxLibrary;

    private void Awake()
    {
        if (instance == null)
        {
            //Si no hay Audiomanager lo referenciamos y hacemos que perdure entre escenas
            instance = this;
        }
        else
        {
            //Si ya hay  AudioManager, el duplicado se destruye
            Destroy(gameObject);
        }
    }

    public void PlayMusic(int musicToPlay)
    {
        musicSource.clip = musicLibrary[musicToPlay];
        musicSource.Play(); //Reproducir la musica desde el principio
    }

    public void PlaySFX(int SFXToPlay)
    {
        SFXsource.PlayOneShot(sfxLibrary[SFXToPlay]);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
