using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings 
{
    [SerializeField] private bool isGameSoundOn;
    [SerializeField] private bool isMusicSoundOn;
    public bool IsGameSoundOn { get => isGameSoundOn; }
    public bool IsMusicSoundOn { get => isMusicSoundOn; }
}
