// reference: https://www.youtube.com/watch?v=YOaYQrN1oYQ&list=PLPV2KyIb3jR4JsOygkHOd2q0CFoslwZOZ&index=2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public Text nameText, ageText, heightText;
    public TMP_Text handText, avatarText;
    public Slider slider;

    string handChoice = "handChoice";
    string age = "age";
    string height = "height";
    string namePlayer = "name";
    string volume = "volume";
    string avatar = "avatarChoice";

    public AudioMixer audioMixer;

    void Start() { UpdateTexts(); }

    public void UpdateTexts()
    {
        nameText.text = PlayerPrefs.GetString(namePlayer);
        ageText.text = PlayerPrefs.GetInt(age).ToString();
        heightText.text = PlayerPrefs.GetInt(height).ToString();
        handText.text = "Main hand: " + PlayerPrefs.GetString(handChoice);
        slider.value = PlayerPrefs.GetFloat(volume);

    }
    // @TODO
    // - check validation of the input data
    public void SetVolume(float volumeLevel)
    {
        PlayerPrefs.SetFloat(volume, volumeLevel);
        audioMixer.SetFloat("musicVolume", volumeLevel);
        Debug.Log("Volume set to " + volumeLevel);
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString(namePlayer, name);
        UpdateTexts();
        Debug.Log("name set: " + name);
    }
    public void SetAge(string ageValue)
    {
        PlayerPrefs.SetInt(age, int.Parse(ageValue));
        UpdateTexts();
        Debug.Log("age set: " + ageValue);
    }
    public void SetHeight(string heightValue)
    {
        PlayerPrefs.SetInt(height, int.Parse(heightValue));
        UpdateTexts();
        Debug.Log("height set: " + heightValue);
    }
    // @TODO
    // - set hand button color by default without using texts to inform the user
    public void SetHand(string hand)
    {
        PlayerPrefs.SetString(handChoice, hand);
        PlayerPrefs.SetString(avatar, "Trainer");
        UpdateTexts();
        Debug.Log("hand set: " + hand);
    }
    public void SaveSettings() { PlayerPrefs.Save(); }

    //@TODO
    // - set default data when erase all data
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey(handChoice);
        PlayerPrefs.DeleteKey(age);
        PlayerPrefs.DeleteKey(height);
        PlayerPrefs.DeleteKey(namePlayer);
        UpdateTexts();
    }
}


