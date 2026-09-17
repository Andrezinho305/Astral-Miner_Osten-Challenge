using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip ambience;
    [SerializeField] private AudioClip asteroidexplosion;
    [SerializeField] private AudioClip engine;
    [SerializeField] private AudioClip impact;
    [SerializeField] private AudioClip spaceshipexplosion;
    [SerializeField] private AudioClip tirolaser;
    [SerializeField] private AudioClip upgrade;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource sfxSource;

    public static SoundManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayAmbience()
    {
        ambienceSource.clip = ambience;
        ambienceSource.loop = true;
        ambienceSource.Play();
    }

    public void PlayAsteroidExplosion()
    {
       sfxSource.PlayOneShot(asteroidexplosion);
    }

    public void PlayEngine()
    {
        sfxSource.clip = engine;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    public void PlayImpact()
    {
        sfxSource.PlayOneShot(impact);
    }

    public void PlaySpaceshipExplosion()
    {
        sfxSource.PlayOneShot(spaceshipexplosion);
    }

    public void PlayTirolaser()
    {
        sfxSource.PlayOneShot(tirolaser);
    }

    public void PlayUpgrade()
    {
        sfxSource.PlayOneShot(upgrade);
    }
}
