using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLogic : MonoBehaviour
{
    private MainLogic logic;
    private ChoiceManager choiceManager;
    private DialogueManager dialogue;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
        logic = GetComponent<MainLogic>();
        choiceManager = new ChoiceManager();
        Debug.Log("CHOICE MANAGER" + choiceManager);


        (string, string) choice = choiceManager.getNextAction(0);
        dialogue.sentences.Enqueue(choice.Item2);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
