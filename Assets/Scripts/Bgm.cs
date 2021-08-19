using System;
using UnityEngine;

[Serializable]
public class Bgm
{
    [SerializeField]
    public string scene;

    [SerializeField]
    public AudioClip audio;

    public static bool ContainsScene(Bgm[] bgmArray, string sceneName)
    {
        foreach (var bgm in bgmArray)
        {
            if (bgm.scene == sceneName) return true;
        }
        return false;
    }

    public static AudioClip GetAudioClip(Bgm[] bgmArray, string sceneName)
    {
        foreach (var bgm in bgmArray)
        {
            if (bgm.scene == sceneName) return bgm.audio;
        }
        return null;
    }
}