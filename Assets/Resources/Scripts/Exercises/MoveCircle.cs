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
    //LoadCharacter waitInizialized;
    //bool outOfBounds = false;

    void Start()
    {

        //Debug.Log("wait > " + waitInizialized.IsInitialized);

        //while (!waitInizialized.IsInitialized)
        //{
        //    Debug.Log("waiting...");
        //}
        countCheck = headTracking.count;
        Debug.Log(anim);

        //Debug.Log("boundaries:" + transform.parent.gameObject.GetComponent<Renderer>().bounds.extents);
    }

    void Update()
    {
        if (gameObject.activeSelf && firstTime) //TODO find a better solution for this, beacuse it is recalled just one time
        {
            calibration_pos = headTracking.calibration_position;
            firstTime = false;
            //Vector3 currentPos_3D = new Vector3(transform.parent.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], transform.parent.transform.position[1] + (NRFrame.HeadPose.position[2] - calibration_pos[2]), transform.parent.transform.position[2] - 0.1f);
            initPos_2D = new Vector2(transform.parent.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], transform.parent.transform.position[1] + (NRFrame.HeadPose.position[2] - calibration_pos[2]));
            //Vector2 calibPos_2D = new Vector2(calibration_pos[0], calibration_pos[1]);
            //Vector2 temp = new Vector2(NRFrame.HeadPose.position[0]- , NRFrame.HeadPose.position[1])
            //distanceCalib = Vector2.Distance(currentPos_2D, calibPos_2D);
        }

        anim = GameObject.Find("TrainerPosition").transform.GetChild(0).GetComponent<Animator>();
        transform.position = new Vector3(transform.parent.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], transform.parent.transform.position[1] + (NRFrame.HeadPose.position[2] - calibration_pos[2])*0.5f, transform.parent.transform.position[2] - 0.1f);
        //TODO: add public variable to the shift of y *0.5f
        if (Time.time >= nextUpdate)
        {
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
            //Vector2 currentPos_2D = new Vector2(transform.parent.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], transform.parent.transform.position[1] + (NRFrame.HeadPose.position[2] - calibration_pos[2]));
            Vector2 currentPos_2D = new Vector2(transform.position[0], transform.position[1]);
            Score(currentPos_2D);
        }
        if (headTracking.count != countCheck)
        {
            countCheck = headTracking.count;
            totMark += totDist / count;
        }
        //bool isBadExercise = anim.GetBool("isBadExercise");
        //Debug.Log(transform.position);

        if (!transform.parent.gameObject.GetComponent<Renderer>().bounds.Contains(transform.position))
        {
            //Debug.Log("Out of boundaries");
            anim.SetBool("isBadExercise", true);
        }
        else
            anim.SetBool("isBadExercise", false);
    }

    void Score(Vector2 currentPosition)
    {
        distanceScore = Mathf.Abs(Vector2.Distance(currentPosition, initPos_2D) - distanceCalib);
        //distanceScore = Mathf.Exp(-11*distanceScore) * 100;
        distanceScore = (1 / (distanceScore + 0.5f) - 1) * 100;

        Debug.Log("Score: " + distanceScore);

        totDist += distanceScore;

        count++;
        
        //switch (distanceScore)
        //{
        //    case float n when (n>=100):
        //        break;


        //}
    }
}
