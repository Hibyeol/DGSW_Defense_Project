using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class SoundManger : MonoBehaviour
{
    //AudioMixer 
    public AudioMixer mixer;

    //배경음 처리 AudioSource
    public AudioSource bgSound;
    //배경음 목록
    public AudioClip[] bglist;

    //효과음
    //총 효과음 처리 AudioSource
    public AudioSource GumEFXSound;
    //플레이어 효과음 처리 AudioSource
    public AudioSource PlayerEFXSound;

    //총 발사
    public AudioClip shoot_up;
    //총 재장전
    public AudioClip gun_reloding;

    //플레이어 맞는 소리
    public AudioClip player_hurt;
    //플레이어 죽는 소리
    public AudioClip player_die;


    public static SoundManger instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i<bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                BgSoundPlay(bglist[i]);
        }
    }

    //BGM 볼륨 조절
    public void BgSoundVolume(float val)
    {
        mixer.SetFloat("BgSoundVolume", Mathf.Log10(val) * 20);
    }

    //Gun 효과음 볼륨 조절
    public void GunEFXVolume(float val)
    {
        mixer.SetFloat("GunEFXVolume", Mathf.Log10(val) * 20);
    }

    //Player 효과음 볼륨 조절
    public void PlayerEFXVolume(float val)
    {
        mixer.SetFloat("PlayerEFXVolume", Mathf.Log10(val) * 20);
    }

    //Bgm
    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
        
    }

    //플레이어가 맞을 때
    public void PlayerHurt()
    {
        PlayerEFXSound.outputAudioMixerGroup = mixer.FindMatchingGroups("PlayerEFX")[0];
        PlayerEFXSound.clip = player_hurt;
        PlayerEFXSound.Play();
    }

    //사망 시 
    public void PlayerDie()
    {
        PlayerEFXSound.outputAudioMixerGroup = mixer.FindMatchingGroups("PlayerEFX")[0];
        PlayerEFXSound.clip = player_die;
        PlayerEFXSound.Play();
    }

    //총 발사
    public void ShootUp()
    {
        GumEFXSound.outputAudioMixerGroup = mixer.FindMatchingGroups("GunEFX")[0];
        GumEFXSound.clip = shoot_up;
        GumEFXSound.Play();
    }

    //총 재장전
    public void GunReloding()
    {
        GumEFXSound.outputAudioMixerGroup = mixer.FindMatchingGroups("GunEFX")[0];
        GumEFXSound.clip = gun_reloding;
        GumEFXSound.Play();
    }

}
