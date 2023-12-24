using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource voicelineSource;
    
    [Header("VoiceLines")]
    [SerializeField] private AudioClip scene1FirstLine;
    [SerializeField] private AudioClip scene1SecondLine;
    [SerializeField] private AudioClip scene1ThirdLine;
    [SerializeField] private AudioClip scene1ForthLine;
    [SerializeField] private AudioClip scene1FifthLine;

    [Header("References")] 
    [SerializeField] private GameObject bonkStick;
    [SerializeField] private GameObject laserGun;
    [SerializeField] private int numberOfEnemies;
    private int _currentEnemiesKilled = 0;

    public enum AudioType
    {
        Scene1FirstLine,
        Scene1SecondLine,
        Scene1ThirdLine,
        Scene1ForthLine,
        Scene1FifthLine,
    }

    private Dictionary<AudioType, AudioClip> _audioStorage = new();

    private void InitializeAudio()
    {
        _audioStorage.Add(AudioType.Scene1FirstLine, scene1FirstLine);
        _audioStorage.Add(AudioType.Scene1SecondLine, scene1SecondLine);
        _audioStorage.Add(AudioType.Scene1ThirdLine, scene1ThirdLine);
        _audioStorage.Add(AudioType.Scene1ForthLine, scene1ForthLine);
        _audioStorage.Add(AudioType.Scene1FifthLine, scene1FifthLine);
    }

    private void Start()
    {
        InitializeAudio();
        StartCoroutine(Scene1BeginningVoicelines());
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

    private void GiveBonkStick()
    {
        bonkStick.SetActive(true);
    }

    private void GiveLaserGun()
    {
        bonkStick.SetActive(false);
        laserGun.SetActive(true);
    }

    public void CheckIfEndSceneOne()
    {
        _currentEnemiesKilled++;

        if (_currentEnemiesKilled >= numberOfEnemies)
        {
            StartCoroutine(Scene1EndingVoicelines());
        }
    }

    private IEnumerator Scene1BeginningVoicelines()
    {
        yield return new WaitForSeconds(2);
        PlayVoiceLine(AudioType.Scene1FirstLine);
        yield return new WaitForSeconds(30);
        PlayVoiceLine(AudioType.Scene1SecondLine);
        yield return new WaitForSeconds(18);
        PlayVoiceLine(AudioType.Scene1ThirdLine);
        yield return new WaitForSeconds(4);
        PlayVoiceLine(AudioType.Scene1ForthLine);
        yield return new WaitForSeconds(15);
        GiveBonkStick();
        yield return new WaitForSeconds(2);
        StartSpawningEnemies();
    }
    
    private IEnumerator Scene1EndingVoicelines()
    {
        PlayVoiceLine(AudioType.Scene1FifthLine);
        yield return new WaitForSeconds(10);
        GiveLaserGun();
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("SecondScene");
    }
}
