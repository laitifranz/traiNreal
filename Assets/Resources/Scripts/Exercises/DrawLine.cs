using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class DrawLine : MonoBehaviour
{
    List<Vector3> linePoints;
    float timer;
    public float timerDelay;
    public float lineWidth;

    GameObject newLine;
    LineRenderer drawLine;

    Vector3 calibration_pos;
    public HeadTracking headTracking;
    bool firstTime = true, risePlayer;

    void Start()
    {
        linePoints = new List<Vector3>();
        timer = timerDelay;

        newLine = new GameObject("Line");
        newLine.transform.parent = gameObject.transform;

        drawLine = newLine.AddComponent<LineRenderer>();
        newLine.GetComponent<LineRenderer>().enabled = false;
        drawLine.material = new Material(Shader.Find("Sprites/Default"));

        drawLine.startColor = Color.blue;
        drawLine.endColor = Color.blue;
        drawLine.startWidth = lineWidth;
        drawLine.endWidth = lineWidth;
    }

    void Update()
    {
        if (headTracking.calibrated && firstTime) //@TODO find a better solution for this, it works but it is recalled just one time
        {
            calibration_pos = headTracking.calibration_position;
            firstTime = false;
        }

        risePlayer = headTracking.loop;

        if (risePlayer && !firstTime)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                linePoints.Add(new Vector3(gameObject.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], gameObject.transform.position[1] + NRFrame.HeadPose.position[1] - calibration_pos[1], gameObject.transform.position[2]));
                timer = timerDelay;
            }
        }
    }

    public void DrawTheLine()
    {
        newLine.GetComponent<LineRenderer>().enabled = true;
        drawLine.positionCount = linePoints.Count;
        drawLine.SetPositions(linePoints.ToArray());
    }

    public void CleanLineValues()
    {
        linePoints.Clear();
    }

    Color randomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}