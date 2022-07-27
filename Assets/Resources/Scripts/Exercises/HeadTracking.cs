using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NRKernal;
using TMPro;

public class HeadTracking : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _coordinates, _countReps;

    Vector3 headpose;
    int count = 0;
    public int totalReps;
    public bool loop = true, calibrated = false;

    public float startPoint, endPoint;
    public Vector3 calibration_position;

    public Button calibration_button;
    public GameObject circle, line;

    public DrawLine lineObj;

    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        //Pose headpose = NRFrame.HeadPose;
        canvas = GameObject.Find("ReferenceLine/Canvas").gameObject;
        headpose = NRFrame.HeadPose.position;
        calibration_button.gameObject.SetActive(true);
        circle.SetActive(false);
        canvas.SetActive(false);
        line.GetComponent<LineRenderer>().enabled = false;
        //line.SetActive(false);
        _countReps.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        headpose = NRFrame.HeadPose.position;
        //Debug.Log(headpose[1]);
        //Debug.Log("headpose coordinates: " + headpose);

        // @TODO
        // - count reps only when the movement is finished
        // -
        if(calibrated)
            CountRep();
        //Debug.Log("headpose coordinates: " + headpose.position);
    }

    public void Calibration()
    {
        calibration_position = NRFrame.HeadPose.position;
        Debug.Log("calibration set");
        calibration_button.gameObject.SetActive(false);
        circle.SetActive(true);
        //line.SetActive(true);
        //line.GetComponent<LineRenderer>().enabled = false;
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
        }


        _countReps.text = count.ToString() + "/" + totalReps.ToString();
        _coordinates.text = headpose.ToString();
    }
}

// headpose.position returns x,y,z
// headpose.rotation returns angles rotations
