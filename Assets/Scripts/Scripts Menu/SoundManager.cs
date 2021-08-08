using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Globales;

public class SoundManager : MonoBehaviour {

    public List<AudioSource> gameSounds, efectoSounds;
    public string currentScene;

	void Start()
	{
        
		foreach (AudioSource audioSource in FindObjectsOfType(typeof(AudioSource)))
		{
		    gameSounds.Add(audioSource);
		}

        for (int i = 0; i < gameSounds.Capacity; i++)
        {
            if (gameSounds[i].gameObject.layer == LayerMask.NameToLayer("Enemys"))
            {
                efectoSounds.Add(gameSounds[i]);
            }
        }
    }

	void Update()
	{
		if (currentScene != SceneManager.GetActiveScene().name)
		{
            gameSounds.Clear();

			foreach (AudioSource audioSource in FindObjectsOfType(typeof(AudioSource)))
			{
                gameSounds.Add(audioSource);
			}

            for (int i = 0; i < gameSounds.Capacity; i++)
            {
                if (gameSounds[i].gameObject.layer == LayerMask.NameToLayer("Enemys"))
                {
                    efectoSounds.Add(gameSounds[i]);
                }
            }

            currentScene = SceneManager.GetActiveScene().name;
		}

		if (gameSounds.Count > 0)
		{
			for (int i = 0; i < gameSounds.Count; i++)
			{
                gameSounds[i].volume = 0f;
			}
		} 
	}
}