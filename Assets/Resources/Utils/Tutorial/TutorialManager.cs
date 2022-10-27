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
            }
        }
        else gameObject.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
            Debug.Log("starting conversation from " + dialogue.name);

            sentences.Clear();

        foreach (string sentence in dialogue.sentences) { sentences.Enqueue(sentence); }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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
    }
}