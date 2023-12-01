using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource mainAudioSource; // เพิ่ม AudioSource หลักที่นี้

    [SerializeField] private Sound[] sounds;

    [Serializable]
    public struct Sound
    {
        public SoundName soundName;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
        public bool loop;
        [HideInInspector] public AudioSource audioSource;
    }

    public void Play(SoundName soundName)
    {
        Sound sound = GetSound(soundName);
        if (sound.audioSource == null)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
        }
        sound.audioSource.clip = sound.clip;
        sound.audioSource.volume = sound.volume;
        sound.audioSource.loop = sound.loop;
        sound.audioSource.Play();

        // ใช้ mainAudioSource ในการเล่นเสียงหลัก
        mainAudioSource.clip = sound.clip;
        mainAudioSource.volume = sound.volume;
        mainAudioSource.loop = sound.loop;
        mainAudioSource.Play();
    }

    private Sound GetSound(SoundName soundNamePlay)
    {
        return Array.Find(sounds, s => s.soundName == soundNamePlay);
    }

    public enum SoundName
    {
        Slash,
        BG,
        enemyHurt,
        playerHurt,
        bowShoot,
        collectSoul,
        campFire,
        gameOver,
        purchase,
        waterFall
    }

    void Start()
    {
        Play(SoundName.BG);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}