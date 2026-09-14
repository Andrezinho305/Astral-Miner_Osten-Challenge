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
    [SerializeField] private AudioSource audioSource;

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
        audioSource.clip = ambience;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayAsteroidExplosion()
    {
       audioSource.PlayOneShot(asteroidexplosion);
    }

    public void PlayEngine()
    {
        audioSource.clip = engine;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayImpact()
    {
        audioSource.PlayOneShot(impact);
    }

    public void PlaySpaceshipExplosion()
    {
        audioSource.PlayOneShot(spaceshipexplosion);
    }

    public void PlayTirolaser()
    {
        audioSource.PlayOneShot(tirolaser);
    }

    public void PlayUpgrade()
    {
        audioSource.PlayOneShot(upgrade);
    }
}
