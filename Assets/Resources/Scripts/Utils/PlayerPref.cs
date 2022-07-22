//references: https://www.youtube.com/watch?v=BgxbCej0GOg

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class PlayerPref : MonoBehaviour
{
    public TMP_Text lastSeenText;
    public AudioMixer audioMixer;

    string firstAccess = "firstAccess";
    string handChoice = "handChoice";
    string age = "age";
    string height = "height";
    string namePlayer = "name";
    string volume = "volume";
    string lastAccess = "lastAccess";
    string avatar = "avatarChoice";
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(firstAccess))
        {
            PlayerPrefs.SetString(handChoice, "right");
            PlayerPrefs.SetInt(age, 18);
            PlayerPrefs.SetInt(height, 180);
            PlayerPrefs.SetString(namePlayer, "");
            PlayerPrefs.SetFloat(volume, -50f);
            PlayerPrefs.SetString(avatar, "trainer");
        }
        else
            Debug.Log("using data already stored");
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat(volume));
        PlayerPrefs.SetString(lastAccess, System.DateTime.Now.ToShortDateString());
        lastSeenText.text = "Last seen: " + PlayerPrefs.GetString(lastAccess);
        Debug.Log("last access: " + PlayerPrefs.GetString(lastAccess));
    }
}
