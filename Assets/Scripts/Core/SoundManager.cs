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

    // �������
    [SerializeField]
    private Bgm[] bgmAudioClips;

    // ȿ������ �����ϴ� �迭
    [SerializeField]
    private AudioClip[] sfxAudioClips;

    Dictionary<string, AudioClip> sfxAudioClipsDic = new Dictionary<string, AudioClip>(); // ȿ�������� string���� ������ �� �ְ� ���� ��ųʸ�

    private void Awake()
    {
        if (Instance != this)
        { // �̹� SoundManager�� ������ �� SoundManager ����
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); // ���� ������ ���

        // sfxPlayer�� (�ڵ�����) ������ �ڽ� ������Ʈ�� �ִ� AudioSource ������Ʈ
        GameObject sfxChild = new GameObject("SFX");
        sfxChild.transform.SetParent(transform);
        sfxPlayer = sfxChild.AddComponent<AudioSource>();

        // bgmPlayer�� (�ڵ�����) ������ �ڽ� ������Ʈ�� �ִ� AudioSource ������Ʈ
        GameObject bgmChild = new GameObject("BGM");
        bgmChild.transform.SetParent(transform);
        bgmPlayer = bgmChild.AddComponent<AudioSource>();

        // ȿ���� �迭�� �ִ� AudioClip���� ��ųʸ��� ����
        foreach (AudioClip audioclip in sfxAudioClips)
        {
            sfxAudioClipsDic.Add(audioclip.name, audioclip);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        
        SceneManager_sceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);  // �׽�Ʈ��
    }

    // ���� �ٲ𶧸��� ���� �´� BGM ���
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
    }

    // ȿ���� ���
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

        bgmPlayer.loop = true; // ������ ����
        bgmPlayer.volume = volume * masterVolumeBGM;

        bgmPlayer.clip = clip;
        bgmPlayer.Play();
    }

    // BGM �ߴ�
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    // SFX ���� ����
    public void SetSFXVolume(float volume)
    {
        masterVolumeSFX = volume;
    }

    // BGM ���� ����
    public void SetBGMVolume(float volume)
    {
        masterVolumeBGM = volume;
        bgmPlayer.volume = volumeBGM * masterVolumeBGM;
    }
}
