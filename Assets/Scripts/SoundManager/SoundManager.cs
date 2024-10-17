using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource bgmSource;

    private Dictionary<string, AudioClip> clips;

    public SoundManager()
    {
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
    }

    public void PlayBGM(string res)
    {
        // 
        if (clips.ContainsKey(res) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip);
        }
        bgmSource.clip = clips[res];
        bgmSource.Play();
    }
}
