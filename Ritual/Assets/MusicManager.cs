
using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] music;
    int playIndex;

    // Use this for initialization
    void Start()
    {
        playIndex = 0;
        music[playIndex].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!music[playIndex].isPlaying)
        {
            playIndex += 1;
            if (playIndex >= music.Length)
            {
                playIndex = 0;
            }
            music[playIndex].Play();
        }
    }
}
