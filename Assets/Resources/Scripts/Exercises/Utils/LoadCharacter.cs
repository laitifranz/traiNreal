using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NRKernal.NRExamples;
using UnityEngine.UI;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    GameObject trainer, trainer_original;
    string activeScene;
    public bool IsInitialized = false;

    //Avatar paths
    string path_tranier = "Characters/Man/Trainer";
    string path_woman = "Characters/Woman/Woman";
    string path_mouse = "Characters/Mouse/Mouse";
    string path_robot = "Characters/Robot Kyle/Model/Robot Kyle";

    //Controller paths
    string path_squat = "Animations/Controller/Humanoid/SquatController";
    string path_happy_idle = "Animations/Controller/Humanoid/IdleSettingsController";
    string path_victory = "Animations/Controller/Humanoid/VictoryController";
    string path_neutral_idle = "Animations/Controller/Humanoid/MainExerciseController";
    string path_warmup = "Animations/Controller/Humanoid/WarmupController";
    string path_lunges = "Animations/Controller/Humanoid/LungesController";
    string path_stretching = "Animations/Controller/Humanoid/StretchingController";

    //Scene organization and classification
    List<string> no_interactive_avatar = new List<string>()
        {
            "Start",
            "Settings",
            "Helper",
            "EndScene",
            "SquatAnalysis"
        };

    List<string> interactive_avatar = new List<string>()
        {
            "Warmup",
            "Lunges",
            "SquatView",
            "Stretching"
        };

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
        SelectCharacter();
        InstantiateTrainer(); //pay attention: first instantiate to avoid modifying directly the prefabs https://www.reddit.com/r/Unity3D/comments/30udy4/til_resourcesload_will_return_a_reference_not/
        ControllerAssignment();
        AddColliderComponent();
        AddScriptComponent();
        IsInitialized = true;
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
        Debug.Log("scene " + activeScene);

        if (no_interactive_avatar.Contains(activeScene))
        {
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_happy_idle) as RuntimeAnimatorController;
        }

        if (activeScene == "Warmup")
        {
            PlayerPrefs.SetInt("alreadyView", 0);
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_warmup) as RuntimeAnimatorController;
        }

        if (activeScene == "SquatView")
        {
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_squat) as RuntimeAnimatorController;
        }

        if (activeScene == "SquatAnalysis")
        {
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_neutral_idle) as RuntimeAnimatorController;
        }

        if (activeScene == "Lunges")
        {
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_lunges) as RuntimeAnimatorController;
        }

        if (activeScene == "Stretching")
        {
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_stretching) as RuntimeAnimatorController;
        }

        if (activeScene == "EndScene")
        {
            Animator animator = trainer.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load(path_victory) as RuntimeAnimatorController;
        }


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

        if (interactive_avatar.Contains(activeScene)) { 
            TrainerMovement scriptTrainer = trainer.AddComponent<TrainerMovement>();
            scriptTrainer._title = GameObject.Find("Canvas/stateTrainer/Text").GetComponent<TMP_Text>();
        }
    }

    private void InstantiateTrainer()
    {
        trainer = Instantiate(trainer_original, gameObject.transform);
        //if (trainer != null)
            
        //else
        //    Debug.Log("error while creating the trainer");
    }
}
