using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadpoolSound : MonoBehaviour
{
    [SerializeField] private SoundData soundData;
    private AudioSource walk;
    private AudioSource run;
    private AudioSource jump;
    private AudioSource roll;
    private AudioSource slash;
    private AudioSource punch;
    private AudioSource takeDame;
    public AudioSource asWalk => walk;
    public AudioSource asRun => run;
    public AudioSource asJump => jump;
    public AudioSource asRoll => roll;
    public AudioSource asSlash => slash;
    public AudioSource asPunch => punch;
    public AudioSource asTakeDame => takeDame;

    private void Awake()
    {
        walk = gameObject.AddComponent<AudioSource>();
        run = gameObject.AddComponent<AudioSource>();
        jump = gameObject.AddComponent<AudioSource>();
        roll = gameObject.AddComponent<AudioSource>();
        slash = gameObject.AddComponent<AudioSource>();
        punch = gameObject.AddComponent<AudioSource>();
        takeDame = gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < soundData.lst.Count; i++)
        {
            if (soundData.lst[i].name.Equals("Walk"))
            {
                walk.clip = soundData.lst[i].audioClip;
                walk.playOnAwake = false;
                walk.loop = true;
            }
            if (soundData.lst[i].name.Equals("Run"))
            {
                run.clip = soundData.lst[i].audioClip;
                run.pitch = 1.5f;
                run.playOnAwake = false;
                run.loop = true;
            }
            if (soundData.lst[i].name.Equals("Jump"))
            {
                jump.clip = soundData.lst[i].audioClip;
                jump.playOnAwake = false;
                jump.loop = false;
            }
            if (soundData.lst[i].name.Equals("Roll"))
            {
                roll.clip = soundData.lst[i].audioClip;
                roll.playOnAwake = false;
                roll.loop = false;
            }
            if (soundData.lst[i].name.Equals("Slash"))
            {
                slash.clip = soundData.lst[i].audioClip;
                slash.playOnAwake = false;
                slash.loop = false;
            }
            if (soundData.lst[i].name.Equals("Punch"))
            {
                punch.clip = soundData.lst[i].audioClip;
                punch.playOnAwake = false;
                punch.loop = false;
            }
            if (soundData.lst[i].name.Equals("Hurt"))
            {
                takeDame.clip = soundData.lst[i].audioClip;
                takeDame.playOnAwake = false;
                takeDame.loop = false;
            }
        }
    }


    private void Update()
    {
        SetVolume(AudioManager.Instance.asUi.volume);
    }
    public void SetVolume(float volume)
    {
        walk.volume = volume;
        run.volume = volume;
        jump.volume = volume;
        roll.volume = volume;
        slash.volume = volume;
        punch.volume = volume;
        takeDame.volume = volume;
    }
}
