using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] AudioSource musicCalmAudioS;
    [SerializeField] AudioSource musicBattleAudioS;

    void Awake()
    {
        if (instance == null) instance = this;

        musicBattleAudioS.volume = 0;
    }

    public void StartGame()
    {
        musicCalmAudioS.volume = 0;
        musicBattleAudioS.volume = 1;
    }

    public void EndGame()
    {
        musicCalmAudioS.volume = 1;
        musicBattleAudioS.volume = 0;
    }
}
