using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int numberOfMovements = 80;
    private Vector3 [] headAngles;
    private int index;
    private Vector3 startingAngle;
    private bool wait = false;

    private MainLogic logic;
    private ChoiceManager choiceManager;
    private DialogueManager dialogue;
    //public GameObject sphere;

    private static PlayerController current = null;

    public static PlayerController Instance
    {
        get
        {
            return current;
        }
    }

    void Start()
    {
        headAngles = new Vector3[numberOfMovements];
        index = 0;
        startingAngle = Camera.main.transform.eulerAngles;
        InvokeRepeating("MeasureHeadAngle", 0f, .001f);
        InvokeRepeating("ResetMovement", 0f, 5f);

        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
        logic = GameObject.FindGameObjectWithTag("MainLogic").GetComponent<MainLogic>();
        choiceManager = new ChoiceManager();

        choiceManager.setPersist(PlayerPrefs.GetInt("FirstItem",0), PlayerPrefs.GetInt("SecondItem",0), PlayerPrefs.GetInt("ThirdItem",0), PlayerPrefs.GetInt("FourthItem"));
        GotoNext(0);

        
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    private void MeasureHeadAngle ()
    {
        if (SceneManager.GetActiveScene().name == "InterviewRoom") {
            if (!wait)
            {
                headAngles[index++] = Camera.main.transform.eulerAngles;
                if (index == headAngles.Length)
                {
                    wait = true;
                    MeasureMovement();
                    ResetMovement();
                }
            }
        }
    }

    private void MeasureMovement ()
    {
        bool left = false, right = false, up = false, down = false;
        for (int i = 0; i < numberOfMovements; ++i)
        {
            if (headAngles[i].x < startingAngle.x - 5f && !up)
                up = true;
            else if (headAngles[i].x < startingAngle.x + 5f && !down)
                down = true;
            if (headAngles[i].y < startingAngle.y - 5f && !left)
                left = true;
            else if (headAngles[i].y < startingAngle.y + 5f && !right)
                right = true;
        }
        //No
        if (left && right && !(down && up))
        {
            Debug.Log("No");


            GotoNext(0);



            //sphere.GetComponent<Renderer>().material.color = Color.red;
            

        }
        //Yes
        if (down && up && !(left && right))
        {
            Debug.Log("Yes");

            GotoNext(1);

            //sphere.GetComponent<Renderer>().material.color = Color.green;
            dialogue.DisplayNextSentence();

        }
        StartCoroutine("WaitForInput");
    }

    public void MinigameScore (int action)
    {
        choiceManager.getNextAction(action);
    }

    IEnumerator WaitForInput ()
    {
        yield return new WaitForSeconds(2f);
        wait = false;
    }

    private void ResetMovement ()
    {
        index = 0;
        headAngles = new Vector3[numberOfMovements];
        startingAngle = Camera.main.transform.eulerAngles;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "InterviewRoom")
            dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
        logic = GameObject.FindGameObjectWithTag("MainLogic").GetComponent<MainLogic>();
    }

    private void GotoNext(int yesOrNo)
    {

        (int, int, int, int) playerPersist;

        (string, string) choice = choiceManager.getNextAction(yesOrNo);
        Debug.Log("----AT PLAYER CONTROL");
        Debug.Log(choice.Item1 + " " + choice.Item2);
        if (choice.Item1 == "question")
        {
            dialogue.AddSentence(choice.Item2);
            dialogue.DisplayNextSentence();
        }
        else if(choice.Item1 == "minigame")
        {
            Debug.Log("WE MADE IT THIS FAR" + choice.Item2);
            playerPersist = choiceManager.getPersistInfo();
            PlayerPrefs.SetInt("FirstItem", playerPersist.Item1);
            PlayerPrefs.SetInt("SecondItem", playerPersist.Item2);
            PlayerPrefs.SetInt("ThirdItem", playerPersist.Item3);
            PlayerPrefs.SetInt("FourthItem", playerPersist.Item4);
            SceneManager.LoadScene(choice.Item2);

        }



    }
}
