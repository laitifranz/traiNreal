using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NRKernal;

public class HeadTracking : MonoBehaviour
{

    [SerializeField]
    private Text _coordinates, _countReps;

    Vector3 headpose;
    int count = 0;
    public int totalReps;
    bool loop = true;

    public float startPoint, endPoint;

    // Start is called before the first frame update
    void Start()
    {
        //Pose headpose = NRFrame.HeadPose;
        headpose = NRFrame.HeadPose.position;

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

        if (loop)
        {
            if (headpose[1] < endPoint)
            {
                count += 1;
                loop = false;
            }
        }
        if (headpose[1] > startPoint)
        {
            loop = true;
        }

        _countReps.text = count.ToString() + "/" + totalReps.ToString();
        _coordinates.text = headpose.ToString();
        //Debug.Log("headpose coordinates: " + headpose.position);
    }
}

// headpose.position returns x,y,z
// headpose.rotation returns angles rotations
