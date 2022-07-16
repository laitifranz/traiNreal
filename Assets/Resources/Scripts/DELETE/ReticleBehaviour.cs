using System.Collections;
using System.Collections.Generic;
using NRKernal;
using UnityEngine;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject Child;
    // Start is called before the first frame update
    public GameObject CurrentPlane;


    void Start()
    {
        Child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var handControllerAnchor = NRInput.DomainHand == ControllerHandEnum.Left ? ControllerAnchorEnum.LeftLaserAnchor : ControllerAnchorEnum.RightLaserAnchor;
        Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : handControllerAnchor);

        RaycastHit hitResult;
        if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
        {
            var hit = hitResult.collider.gameObject;
            if (hit != null &&
                 hit.GetComponent<NRTrackableBehaviour>()?.Trackable.GetTrackableType() == TrackableType.TRACKABLE_PLANE)
            {
                // Move this reticle to the location of the hit.
                CurrentPlane = hit;
                transform.position = hitResult.point;
                Child.SetActive(true);
            }
            else
            {
                Child.SetActive(false);
            }
        }
    }
}