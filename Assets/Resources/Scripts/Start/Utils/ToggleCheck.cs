using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheck : MonoBehaviour
{
    public Toggle selectedToggle;

    void Start()
    {
        selectedToggle.onValueChanged.AddListener(delegate { ToggleValueChangedOccured(selectedToggle);});
    }

    void ToggleValueChangedOccured(Toggle tglValue)
    {
        if (tglValue.isOn) PlayerPrefs.SetInt("tutorialEnabled", 1);
        else PlayerPrefs.SetInt("tutorialEnabled", 0);
        PlayerPrefs.Save();
    }
}
