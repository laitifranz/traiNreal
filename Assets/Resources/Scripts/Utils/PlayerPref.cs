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

    string handChoice = "handChoice";
    string age = "age";
    string height = "height";
    string namePlayer = "name";
    string volume = "volume";
    string lastAccess = "lastAccess";
    string avatar = "avatarChoice";
    string firstAccess = "firstAccess";
    // Start is called before the first frame update
    void Awake() //not Start, in order to avoid crashes or missing references 
    {
        if (!PlayerPrefs.HasKey(lastAccess))
        {
            PlayerPrefs.SetString(handChoice, "rightHand");
            PlayerPrefs.SetInt(age, 18);
            PlayerPrefs.SetInt(height, 180);
            PlayerPrefs.SetString(namePlayer, "");
            PlayerPrefs.SetFloat(volume, -50f);
            PlayerPrefs.SetInt(avatar, 0);
            PlayerPrefs.SetString(lastAccess, System.DateTime.Now.ToShortDateString());
            PlayerPrefs.SetInt(firstAccess, 1);
            //PlayerPrefs.DeleteKey("alreadyView");
            PlayerPrefs.SetInt("alreadyView", 0);
        }
        else
        {
            //PlayerPrefs.DeleteKey("alreadyView");
            //PlayerPrefs.SetInt("alreadyView", 0);
            Debug.Log("using data already stored");
        }
            
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat(volume));
        lastSeenText.text = "Last seen: " + PlayerPrefs.GetString(lastAccess);
        Debug.Log("last access: " + PlayerPrefs.GetString(lastAccess));
    }
}
