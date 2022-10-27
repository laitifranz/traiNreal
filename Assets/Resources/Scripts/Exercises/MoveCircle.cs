using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NRKernal;

public class MoveCircle : MonoBehaviour
{
    Animator anim;
    Vector3 calibration_pos;
    public HeadTracking headTracking;

    bool firstTime = true;

    private float nextUpdate = 0.5f;
    private int count = 0;
    private float distanceScore, distanceCalib;
    private Vector2 initPos_2D;
    private float totDist = 0f;
    private int countCheck;
    public float totMark = 0f;
    public float weightY = 0.5f;

    void Start()
    {
        countCheck = headTracking.count;
        Debug.Log(anim);
    }

    void Update()
    {
        if (gameObject.activeSelf && firstTime) //@TODO find a better solution for this, it works but it is recalled just one time
        {
            calibration_pos = headTracking.calibration_position;
            firstTime = false;
            initPos_2D = new Vector2(transform.parent.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], transform.parent.transform.position[1] + (NRFrame.HeadPose.position[2] - calibration_pos[2]));
        }

        anim = GameObject.Find("TrainerPosition").transform.GetChild(0).GetComponent<Animator>();
        transform.position = new Vector3(transform.parent.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], transform.parent.transform.position[1] + (NRFrame.HeadPose.position[2] - calibration_pos[2])*weightY, transform.parent.transform.position[2] - 0.1f);

        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            Vector2 currentPos_2D = new Vector2(transform.position[0], transform.position[1]);
            Score(currentPos_2D);
        }
        if (headTracking.count != countCheck)
        {
            countCheck = headTracking.count;
            totMark += totDist / count;
        }

        if (!transform.parent.gameObject.GetComponent<Renderer>().bounds.Contains(transform.position)) anim.SetBool("isBadExercise", true);
        else anim.SetBool("isBadExercise", false);
    }

    void Score(Vector2 currentPosition)
    {
        distanceScore = Mathf.Abs(Vector2.Distance(currentPosition, initPos_2D) - distanceCalib);
        distanceScore = (1 / (distanceScore + 0.5f) - 1) * 100;

        totDist += distanceScore;
        count++;

        Debug.Log("Score: " + distanceScore);
    }
}
