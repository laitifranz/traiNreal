//references: https://www.youtube.com/watch?v=BgxbCej0GOg

using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class PlayerPref : MonoBehaviour
{
    public TMP_Text lastSeenText;
    public AudioMixer audioMixer;

    void Awake() //Use Awake instead of Start in order to avoid crashes or missing references 
    {
        if (!PlayerPrefs.HasKey("lastAccess"))
        {
            PlayerPrefs.SetString("handChoice", "rightHand");
            PlayerPrefs.SetInt("age", 18);
            PlayerPrefs.SetInt("height", 180);
            PlayerPrefs.SetString("name", "");
            PlayerPrefs.SetFloat("volume", -40f);
            PlayerPrefs.SetInt("avatarChoice", 0);
            PlayerPrefs.SetString("lastAccess", System.DateTime.Now.ToShortDateString());
            PlayerPrefs.SetInt("firstAccess", 1);
            PlayerPrefs.SetInt("tutorialEnabled", 1);
            PlayerPrefs.SetFloat("score", 0);
            PlayerPrefs.SetInt("betterThanLastTime", 1);
            PlayerPrefs.SetInt("totalReps", 10);
        }
        else
        {
            Debug.Log("using data already stored");
        }
            
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("volume"));
        lastSeenText.text = "Last seen: " + PlayerPrefs.GetString("lastAccess");
    }
}
