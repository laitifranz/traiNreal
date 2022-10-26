using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_check : MonoBehaviour
{
    public Toggle selectedToggle;
    // Start is called before the first frame update
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
