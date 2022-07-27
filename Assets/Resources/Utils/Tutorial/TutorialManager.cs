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
    //public Text nameText;
    public TMP_Text dialogueText;

    bool alreadyView;

    void Start()
    {
        if (PlayerPrefs.GetInt("alreadyView") == 0)
            alreadyView = false;
        else
            alreadyView = true;

        if (SceneManager.GetActiveScene().name == "Start")
            PlayerPrefs.SetInt("alreadyView", 1);

        PlayerPrefs.Save();

        Debug.Log(alreadyView);

        if (alreadyView)
        {
            gameObject.SetActive(false);
        }

        sentences = new Queue<string>();
        GameObject.Find("TutorialManager/Canvas/Continue").GetComponent<DialogueTrigger>().TriggerDialogue();

        if(canvas != null) {
            canvas.GetComponent<Canvas>().enabled = false;
            //canvas.gameObject.SetActive(false);
        }
            
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
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogueText.text = sentence;
    }

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
