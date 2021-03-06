using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    private float masterVolumeSFX = 1f;
    private float masterVolumeBGM = 1f;

    private float volumeBGM = 1f;

    // 배경음악
    [SerializeField]
    private Bgm[] bgmAudioClips;

    // 효과음들 지정하는 배열
    [SerializeField]
    private AudioClip[] sfxAudioClips;

    Dictionary<string, AudioClip> sfxAudioClipsDic = new Dictionary<string, AudioClip>(); // 효과음들을 string으로 관리할 수 있게 만든 딕셔너리

    private void Awake()
    {
        if (Instance != this)
        { // 이미 SoundManager가 있으면 이 SoundManager 삭제
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); // 여러 씬에서 사용

        // sfxPlayer는 (자동으로) 생성한 자식 오브젝트에 있는 AudioSource 컴포넌트
        GameObject sfxChild = new GameObject("SFX");
        sfxChild.transform.SetParent(transform);
        sfxPlayer = sfxChild.AddComponent<AudioSource>();

        // bgmPlayer는 (자동으로) 생성한 자식 오브젝트에 있는 AudioSource 컴포넌트
        GameObject bgmChild = new GameObject("BGM");
        bgmChild.transform.SetParent(transform);
        bgmPlayer = bgmChild.AddComponent<AudioSource>();

        // 효과음 배열에 있는 AudioClip들을 딕셔너리에 저장
        foreach (AudioClip audioclip in sfxAudioClips)
        {
            sfxAudioClipsDic.Add(audioclip.name, audioclip);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        
        SceneManager_sceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);  // 테스트용
    }

    // 씬이 바뀔때마다 씬에 맞는 BGM 재생
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Bgm.ContainsScene(bgmAudioClips, scene.name))
        {
            if (bgmPlayer.clip == null || bgmPlayer.clip.name != Bgm.GetAudioClip(bgmAudioClips, scene.name).name)
            {
                StopBGM();
                PlayBGM(Bgm.GetAudioClip(bgmAudioClips, scene.name), 0.2f);
            }
        }
        else
        {
            StopBGM();
        }
    }

    // 효과음 재생
    public void PlaySFX(string name, float volume = 1f)
    {
        if (sfxAudioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained in audioClipsDic");
            return;
        }

        sfxPlayer.PlayOneShot(sfxAudioClipsDic[name], volume * masterVolumeSFX);
    }

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        volumeBGM = volume;

        bgmPlayer.loop = true; // 루프로 설정
        bgmPlayer.volume = volume * masterVolumeBGM;

        bgmPlayer.clip = clip;
        bgmPlayer.Play();
    }

    // BGM 중단
    public void StopBGM()
    {
        bgmPlayer.clip = null;
        bgmPlayer.Stop();
    }

    // SFX 볼륨 설정
    public void SetSFXVolume(float volume)
    {
        masterVolumeSFX = volume;
    }

    // BGM 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        masterVolumeBGM = volume;
        bgmPlayer.volume = volumeBGM * masterVolumeBGM;
    }
}
