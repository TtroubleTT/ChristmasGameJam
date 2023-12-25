using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager3 : MonoBehaviour
{
    [SerializeField] AudioSource voicelineSource;
    
    [Header("VoiceLines")]
    [SerializeField] private AudioClip scene3FirstLine;
    [SerializeField] private AudioClip scene3SecondLine;
    [SerializeField] private AudioClip scene3ThirdLine;

    public enum AudioType
    {
        Scene3FirstLine,
        Scene3SecondLine,
        Scene3ThirdLine,
    }

    private Dictionary<AudioType, AudioClip> _audioStorage = new();

    private void InitializeAudio()
    {
        _audioStorage.Add(AudioType.Scene3FirstLine, scene3FirstLine);
        _audioStorage.Add(AudioType.Scene3SecondLine, scene3SecondLine);
        _audioStorage.Add(AudioType.Scene3ThirdLine, scene3ThirdLine);
    }

    private void Start()
    {
        InitializeAudio();
        StartCoroutine(Scene2BeginningVoicelines());
    }

    public void PlayVoiceLine(AudioType type)
    {
        AudioClip myClip = _audioStorage[type];
        voicelineSource.clip = myClip;
        voicelineSource.volume = .6f;
        voicelineSource.Play();
    }

    public void PlayMusic()
    {
        /*
        musicSource.clip = background;
        musicSource.volume = 0.3f;
        musicSource.Play();
        */
    }

    public void CheckIfEndSceneTwo()
    {
        // beep
    }

    private IEnumerator Scene2BeginningVoicelines()
    {
        yield return new WaitForSeconds(1);
        PlayVoiceLine(AudioType.Scene3FirstLine);
        yield return new WaitForSeconds(2);
        PlayVoiceLine(AudioType.Scene3SecondLine);
        yield return new WaitForSeconds(2);
        PlayVoiceLine(AudioType.Scene3ThirdLine);
    }
    
    private IEnumerator Scene2EndingVoicelines()
    {
        yield return new WaitForSeconds(2);
    }
    
}


