using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    private Queue<string> sentences;

    public Canvas canvas;
    public TMP_Text dialogueText;

    void Start()
    {
        if (PlayerPrefs.GetInt("tutorialEnabled") == 1)
        {
            gameObject.SetActive(true);
            Debug.Log(PlayerPrefs.GetInt("tutorialEnabled"));
            sentences = new Queue<string>();
            GameObject.Find("TutorialManager/Canvas/Continue").GetComponent<DialogueTrigger>().TriggerDialogue();

            if (canvas != null)
            {
                canvas.GetComponent<Canvas>().enabled = false;
                //canvas.gameObject.SetActive(false);
            }
        }
        else gameObject.SetActive(false);


        //if (PlayerPrefs.GetInt("firstAccess") == 0)
        //    GameObject.Find("TutorialManager").SetActive(false); //TODO implement a better solution for optimization, like avoiding use this class and all the childrens
        //if (PlayerPrefs.GetInt("firstAccess"))
        //{

        //}
    }

    public void StartDialogue(Dialogue dialogue)
    {
            Debug.Log("starting conversation from " + dialogue.name);

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //if (SceneManager.GetActiveScene().name == "Warmup" && sentences.Count == 0)
        //{
        //    StartCoroutine(WaitSec());
        //    return;
        //}

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        //dialogueText.text = sentence;
    }

    //IEnumerator WaitSec()
    //{
    //    yield return new WaitForSeconds(3);
    //    EndDialogue();
    //}

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        GameObject.Find("TutorialManager").SetActive(false);

        if (canvas != null) { }
            canvas.GetComponent<Canvas>().enabled = true;

        Debug.Log("end of conversation");
        //PlayerPrefs.SetInt("alreadyView", 1);
    }
}
//    public GameObject[] popUps;
//    private int popUpIndex;
//    public float waitTime = 2f;
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        for (int i = 0; i < popUps.Length; i++)
//        {
//            if (i == popUpIndex)
//                popUps[popUpIndex].SetActive(true);
//            else
//                popUps[popUpIndex].SetActive(false);
//        }

//        if(popUpIndex == 0)
//        {
//            if()
//            popUpIndex++;
//        }
//        else if(popUpIndex == 1)
//        {
//            popUpIndex++;
//        }
//        else if(popUpIndex == 2)
//        {
//            if(waitTime <= 0)
//            {
//                //active something
//                popUpIndex++;
//            }
//            else
//            {
//                waitTime -= Time.deltaTime;
//            }
//        }
//    }
//}
