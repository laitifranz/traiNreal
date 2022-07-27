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
    //LoadCharacter waitInizialized;
    //bool outOfBounds = false;

    void Start()
    {
        //Debug.Log("wait > " + waitInizialized.IsInitialized);

        //while (!waitInizialized.IsInitialized)
        //{
        //    Debug.Log("waiting...");
        //}

        Debug.Log(anim);

        //Debug.Log("boundaries:" + transform.parent.gameObject.GetComponent<Renderer>().bounds.extents);
    }

    void Update()
    {
        if (gameObject.activeSelf && firstTime) //TODO find a better solution for this, beacuse it is recalled just one time
        {
            calibration_pos = headTracking.calibration_position;
            firstTime = false;
        }

        anim = GameObject.Find("TrainerPosition").transform.GetChild(0).GetComponent<Animator>();
        transform.position = new Vector3(transform.parent.transform.position[0] + NRFrame.HeadPose.position[0] - calibration_pos[0], transform.parent.transform.position[1] + (NRFrame.HeadPose.position[2] - calibration_pos[2])*0.5f, transform.parent.transform.position[2] - 0.1f);

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
}
