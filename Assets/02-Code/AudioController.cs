using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip sound;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 2.5f)]
    public float pitch;

    private AudioSource source;

    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();

        volume = 0.5f;
        pitch = 1f;
    }

    void Start()
    {
        source.clip = sound;
        source.volume = volume;
        source.pitch = pitch;

        source.loop = true;
        source.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAndPause();
        }

        source.volume = volume;
        source.pitch = pitch;
    }

    public void PlayAndPause()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
        else
        {
            source.Pause();
        }
    }
}
