using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    private static MusicScript current = null;
    private AudioSource[] audio;

    public static MusicScript Instance
    {
        get
        {
            return current;
        }
    }

    void Start()
    {
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(this.gameObject);

        audio = GetComponents<AudioSource>();
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (scene.name == "WalkingToFogMinigame")
        {
            Debug.Log(audio.Length);
            audio[0].Stop();
            audio[1].Play();
        }
        if (scene.name == "ENDING Adoption" || scene.name == "ENDING Group" || scene.name == "ENDING Nature Solitary")
        {
            audio[1].Stop();
            audio[2].Play();
        }
    }

}
