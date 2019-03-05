using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    public bool isOpen;

    public bool isWriting;

    public bool isSceneChanger;
    public bool onlyChangeForWashy;
    public bool isConvoWithWashy;
    public int i;
    public string scene;

    public Queue<string> sentences;

    // Use this for initialization
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void AddSentence (string sen)
    {
        sentences.Enqueue(sen);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //PlayerMovement.instance.canMove = false;
        animator.SetBool("IsOpen", true);
        isOpen = true;

        nameText.text = dialogue.name;


        //sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            //PlayerMovement.instance.canMove = true;
            if (nameText.text == "Washy")
                isConvoWithWashy = true;

            //nameText.text = "Smartphone Sports";
            EndDialogue();
            return;
        }
        if (!isWriting)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
           
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isWriting = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        isWriting = false;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", true);
        isOpen = true;
        if((isSceneChanger && !onlyChangeForWashy))
        {
            Debug.Log("sceneBuildIndex to load: " + i);
            SceneManager.LoadScene(scene);
        }
        if ((onlyChangeForWashy && isConvoWithWashy))
        {
            Debug.Log("sceneBuildIndex to load: " + i);
            SceneManager.LoadScene(scene);
        }

    }

}