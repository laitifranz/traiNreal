using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class DisplayResult : MonoBehaviour
{
    public TMP_Text _summary;
    private string summaryText;

    void Start()
    {
        summaryText = "SUMMARY:\n";
        summaryText += ("\n-Precision: " + PlayerPrefs.GetFloat("score"));
        summaryText += ("\n-Total reps: " + PlayerPrefs.GetInt("totalReps"));
        summaryText += ("\n-Workout time: " + Mathf.Round(Time.realtimeSinceStartup) + "s");
        if (PlayerPrefs.GetFloat("betterThanLastTime") == 0) summaryText += "\n\nYEAH BUDDY!\n You did better than last time!";
        else summaryText += "\nBRUH?\n You did worse than last time, keep up!";

        _summary.text = summaryText;
    }
}
