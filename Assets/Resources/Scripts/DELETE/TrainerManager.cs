using System.Collections;
using System.Collections.Generic;
using NRKernal;
using UnityEngine;

public class TrainerManager : MonoBehaviour
{
    public GameObject TrainerPrefab;
    public ReticleBehaviour Reticle;
    public TrainerBehaviour Trainer;

    private GameObject LockedPlane;

    private void Update()
    {
        if (Trainer == null && WasTapped() && Reticle.CurrentPlane != null)
        {
            // Spawn our Trainer at the reticle location.
            //var obj = Instantiate(TrainerPrefab);
            //Trainer = obj.GetComponent<TrainerBehaviour>();
            //Trainer.Reticle = Reticle;
            //Trainer.transform.position = Reticle.transform.position;
            Instantiate(TrainerPrefab, Reticle.transform.position, Reticle.transform.rotation);

            Debug.Log("object added to the scene: ");
        }
    }

    private bool WasTapped()
    {
        return NRInput.GetButtonDown(ControllerButton.TRIGGER);
    }
}// Some code