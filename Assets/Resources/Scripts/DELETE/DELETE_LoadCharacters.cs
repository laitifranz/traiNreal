using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DELETE_LoadCharacters : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    //public TMP_Text label;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("avatarChoice");
        GameObject clone = Instantiate(characterPrefabs[selectedCharacter], spawnPoint.position, Quaternion.identity);
        //label.text = prefab.name;
    }
}
