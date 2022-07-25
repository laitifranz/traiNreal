using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NRKernal.NRExamples;
using UnityEngine.UI;

public class LoadCharacter : MonoBehaviour
{
    GameObject trainer, trainer_original;

    string path_tranier = "Characters/Man/Trainer";
    string path_woman = "Characters/Woman/Woman";
    string path_mouse = "Characters/Mouse/Mouse";
    string path_robot = "Characters/Robot Kyle/Model/Robot Kyle";

    string path_squat = "Animations/Controller/Humanoid/PTController";


    // Start is called before the first frame update
    void Start()
    {
        SelectCharacter();
        InstantiateTrainer(); //pay attention: first instantiate to avoid modifying directly the prefabs https://www.reddit.com/r/Unity3D/comments/30udy4/til_resourcesload_will_return_a_reference_not/
        ControllerAssignment();
        AddColliderComponent();
        AddScriptComponent();
    }

    private void SelectCharacter()
    {
        Debug.Log("PlayerPrefs avatar > " + PlayerPrefs.GetInt("avatarChoice").ToString());

        trainer_original = PlayerPrefs.GetInt("avatarChoice") switch
        {
            0 => Resources.Load<GameObject>(path_tranier),
            1 => Resources.Load<GameObject>(path_woman),
            2 => Resources.Load<GameObject>(path_mouse),
            3 => Resources.Load<GameObject>(path_robot),
            _ => null,
        };

        //trainer = trainer_original;
        //Destroy(trainer_original);
    }

    private void ControllerAssignment()
    {
        if (SceneManager.GetActiveScene().name == "Warmup")
        {
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_squat) as RuntimeAnimatorController;
        }
        else
            Debug.Log("scene " + SceneManager.GetActiveScene().name);
        

    }

    private void AddColliderComponent()
    {
        BoxCollider boxCollider = trainer.AddComponent<BoxCollider>();
        //TODO
        //  - add auto size and center in function of the shape of the gameobject
        boxCollider.size = new Vector3(1f, 2f, 0.3f);
        boxCollider.center = new Vector3(0f, 1f, 0f); 
        //boxCollider.transform.position = new Vector3(0f, 0.83f, 0.034f);
    }

    private void AddScriptComponent()
    {
        TrainerMovement scriptTrainer = trainer.AddComponent<TrainerMovement>();
        scriptTrainer._title = GameObject.Find("Canvas/Start/Text").GetComponent<Text>();

    }

    private void InstantiateTrainer()
    {
        trainer = Instantiate(trainer_original, gameObject.transform);
        //if (trainer != null)
            
        //else
        //    Debug.Log("error while creating the trainer");
    }
}
