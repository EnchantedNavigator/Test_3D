using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;

    private const string MusicSaveKey = "MusicVolume";
    private const string GameSoundSaveKey = "GameVolume";

    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UpdateAudioMixerState();
    }
    private void UpdateAudioMixerState()
    {
        float GameSoundValue = GetAudioSettings(GameSoundSaveKey) == true ? 0 : -80;
        mainAudioMixer.SetFloat("GameVolume", GameSoundValue);
        float MusicValue = GetAudioSettings(MusicSaveKey) == true ? 0 : -80;
        mainAudioMixer.SetFloat("MusicVolume", MusicValue);
    }
    public bool GetAudioSettings(string key)
    {
        bool result = GameData.Load<bool>(key, () =>
        {
            result = true;
            GameData.Save(result, key);
            });

        return result;
    }
    public void SwitchAudioVolume(string key)
    {
        GameData.Save(!GetAudioSettings(key), key);
        float value = GetAudioSettings(key) == true?0:-80;
        mainAudioMixer.SetFloat(key,value);
    }
    public void SaveAudioSettings(string key)
    {
        GameData.Save(GetAudioSettings(key), key);
    }


}
