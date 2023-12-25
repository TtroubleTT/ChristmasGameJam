using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager2 : MonoBehaviour
{
    [SerializeField] AudioSource voicelineSource;
    
    [Header("VoiceLines")]
    [SerializeField] private AudioClip scene2FirstLine;
    [SerializeField] private AudioClip scene2SecondLine;
    [SerializeField] private AudioClip scene2ThirdLine;
    [SerializeField] private AudioClip scene2ForthLine;
    [SerializeField] private AudioClip scene2FifthLine;

    [Header("References")]
    [SerializeField] private int numberOfEnemies;
    private int _currentEnemiesKilled = 0;

    public enum AudioType
    {
        Scene2FirstLine,
        Scene2SecondLine,
        Scene2ThirdLine,
        Scene2ForthLine,
        Scene2FifthLine,
    }

    private Dictionary<AudioType, AudioClip> _audioStorage = new();

    private void InitializeAudio()
    {
        _audioStorage.Add(AudioType.Scene2FirstLine, scene2FirstLine);
        _audioStorage.Add(AudioType.Scene2SecondLine, scene2SecondLine);
        _audioStorage.Add(AudioType.Scene2ThirdLine, scene2ThirdLine);
        _audioStorage.Add(AudioType.Scene2ForthLine, scene2ForthLine);
        _audioStorage.Add(AudioType.Scene2FifthLine, scene2FifthLine);
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

    private void StartSpawningEnemies()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<EnemySpawner>().StartSpawn();
        }
    }

    public void CheckIfEndSceneTwo()
    {
        _currentEnemiesKilled++;

        if (_currentEnemiesKilled >= numberOfEnemies)
        {
            StartCoroutine(Scene2EndingVoicelines());
        }
    }

    private IEnumerator Scene2BeginningVoicelines()
    {
        yield return new WaitForSeconds(2);
        PlayVoiceLine(AudioType.Scene2FirstLine);
        StartSpawningEnemies();
        yield return new WaitForSeconds(8);
        PlayVoiceLine(AudioType.Scene2SecondLine);
        yield return new WaitForSeconds(2);
        PlayVoiceLine(AudioType.Scene2ThirdLine);
    }
    
    private IEnumerator Scene2EndingVoicelines()
    {
        yield return new WaitForSeconds(2);
        PlayVoiceLine(AudioType.Scene2ForthLine);
        yield return new WaitForSeconds(3);
        PlayVoiceLine(AudioType.Scene2FifthLine);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("FinalScene");
    }
    
}

