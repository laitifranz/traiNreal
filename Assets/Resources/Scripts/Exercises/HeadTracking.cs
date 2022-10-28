using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NRKernal;
using TMPro;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class HeadTracking : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _coordinates, _countReps;

    Vector3 headpose;
    public int count = 0;
    public int totalReps;
    public bool loop = true, calibrated = false;

    public float startPoint, endPoint, floatValue=0.0f;
    public Vector3 calibration_position;

    public Button calibration_button;
    public GameObject circle, line;

    public DrawLine lineObj;
    public SceneChanger changeScene;
    public MoveCircle moveCircle;

    GameObject canvas;
    public GameObject arrow;
    Material arrowMaterial;

    void Start()
    {
        canvas = GameObject.Find("ReferenceLine/Canvas").gameObject;
        headpose = NRFrame.HeadPose.position;
        calibration_button.gameObject.SetActive(true);
        circle.SetActive(false);
        canvas.SetActive(false);
        line.GetComponent<LineRenderer>().enabled = false;
        _countReps.gameObject.SetActive(false);

        arrowMaterial = arrow.GetComponent<Image>().material;
    }

    void Update()
    {
        headpose = NRFrame.HeadPose.position;

        if(calibrated)
            CountRep();
    }

    public void Calibration()
    {
        calibration_position = NRFrame.HeadPose.position;
        Debug.Log("calibration set");
        calibration_button.gameObject.SetActive(false);
        circle.SetActive(true);
        _countReps.gameObject.SetActive(true);
        calibrated = true;
    }

    public void CountRep()
    {
        if (loop)
        {
            if (headpose[1] < endPoint)
            {
                count += 1;
                loop = false;
                circle.SetActive(false);
                canvas.SetActive(true);
                line.GetComponent<LineRenderer>().enabled = true;
                line.GetComponentInChildren<LineRenderer>().enabled = true;
                line.transform.GetChild(0).GetComponentInChildren<LineRenderer>().enabled = true;
                lineObj.DrawTheLine();
            }
        }
        if (headpose[1] > startPoint-0.05f)
        {
            lineObj.CleanLineValues();
            loop = true;
            circle.SetActive(true);
            canvas.SetActive(false);
            line.GetComponent<LineRenderer>().enabled = false;
            line.GetComponentInChildren<LineRenderer>().enabled = false;
            line.transform.GetChild(0).GetComponentInChildren<LineRenderer>().enabled = false;

            if (count >= totalReps)
            {
                circle.SetActive(false);
                canvas.SetActive(false);
                if (PlayerPrefs.GetFloat("score") >= moveCircle.totMark / totalReps) PlayerPrefs.SetInt("betterThanLastTime", 0);
                else PlayerPrefs.SetInt("betterThanLastTime", 1);
                PlayerPrefs.SetFloat("score", moveCircle.totMark/totalReps);
                PlayerPrefs.SetInt("totalReps", totalReps);

                arrowMaterial.SetFloat("_Metallic", floatValue);
                if(floatValue>=1.0f) floatValue = 0.0f  ;
                floatValue += 0.01f;
            }
        }

        if (count < totalReps)  _countReps.text = count.ToString() + "/" + totalReps.ToString();
        else _countReps.text = "Now you can go ahead!";
        _coordinates.text = headpose.ToString();
    }
}
