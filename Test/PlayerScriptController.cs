using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScriptController : MonoBehaviour
{
    [SerializeField] private int numberOfMovements = 80;
    private Vector3[] headAngles;
    private int index;
    private Vector3 startingAngle;
    private bool wait = false;

    //public GameObject sphere
    private DialogueManager dialogue;
    private ReadStory story;
    void Start()
    {
        headAngles = new Vector3[numberOfMovements];
        index = 0;
        startingAngle = Camera.main.transform.eulerAngles;
        InvokeRepeating("MeasureHeadAngle", 0f, .001f);
        InvokeRepeating("ResetMovement", 0f, 5f);

        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
        story = GameObject.FindGameObjectWithTag("MainLogic").GetComponent<ReadStory>();
        dialogue.AddSentence(story.parts[story.getIndex()]);
    }

    private void MeasureHeadAngle()
    {
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

    private void MeasureMovement()
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
        if (left && right && !(down && up))
        {
            Debug.Log("No");

            string[] words = story.parts[2 * story.getIndex() + 1].Split('#');
            if (words.Length > 1)
            {
                if (words[1] == "solitude")
                    story.solitude++;
                else if (words[1] == "group")
                    story.group++;
                else
                    story.adoption++;
            }

            if (words[0] == "Wisp")
            {
                story.setIndex(2 * story.getIndex() + 1);
                SceneManager.LoadScene("ShootingWispsMinigame");
            }
            else if (words[0] == "Fog")
            {
                story.setIndex(2 * story.getIndex() + 1);
                SceneManager.LoadScene("WalkingToFogMinigame");
            }
            else if (words[0] == "Puzzle")
            {
                if (story.solitude > story.adoption && story.solitude > story.group)
                    SceneManager.LoadScene("PuzzleIsolation");
                else if (story.adoption > story.solitude && story.adoption > story.group)
                    SceneManager.LoadScene("PuzzleAdoption");
                else if (story.group > story.adoption && story.group > story.solitude)
                    SceneManager.LoadScene("PuzzleGroup");
                else
                {
                    int rand = Random.Range(0, 3);
                    if (rand == 0) SceneManager.LoadScene("PuzzleIsolation");
                    if (rand == 1) SceneManager.LoadScene("PuzzleAdoption");
                    if (rand == 2) SceneManager.LoadScene("PuzzleGroup");
                }
            }

            dialogue.AddSentence(words[0]);
            story.setIndex(2 * story.getIndex() + 1);
            dialogue.DisplayNextSentence();

        }
        if (down && up && !(left && right))
        {
            Debug.Log("Yes");

            string[] words = story.parts[2 * story.getIndex() + 2].Split('#');
            if (words.Length > 1)
            {
                if (words[1] == "solitude")
                    story.solitude++;
                else if (words[1] == "group")
                    story.group++;
                else
                    story.adoption++;
            }

            if (words[0] == "Wisp") {
                story.setIndex(2 * story.getIndex() + 2);
                SceneManager.LoadScene("ShootingWispsMinigame");
            }
            else if (words[0] == "Fog") {
                story.setIndex(2 * story.getIndex() + 2);
                SceneManager.LoadScene("WalkingToFogMinigame");
            }
            else if (words[0] == "Puzzle")
            {
                if (story.solitude > story.adoption && story.solitude > story.group)
                    SceneManager.LoadScene("PuzzleIsolation");
                else if (story.adoption > story.solitude && story.adoption > story.group)
                    SceneManager.LoadScene("PuzzleAdoption");
                else if (story.group > story.adoption && story.group > story.solitude)
                    SceneManager.LoadScene("PuzzleGroup");
                else
                {
                    int rand = Random.Range(0, 3);
                    if (rand == 0) SceneManager.LoadScene("PuzzleIsolation");
                    if (rand == 1) SceneManager.LoadScene("PuzzleIsolation");
                    if (rand == 2) SceneManager.LoadScene("PuzzleAdoption");
                    if (rand == 3) SceneManager.LoadScene("PuzzleGroup");
                }
            }



            dialogue.AddSentence(words[0]);
            story.setIndex(2 * story.getIndex() + 2);
            dialogue.DisplayNextSentence();
        }
        StartCoroutine("WaitForInput");
    }

    IEnumerator WaitForInput()
    {
        ResetMovement();
        yield return new WaitForSeconds(3f);
        wait = false;
    }

    private void ResetMovement()
    {
        index = 0;
        headAngles = new Vector3[numberOfMovements];
        startingAngle = Camera.main.transform.eulerAngles;
    }

    void Update()
    {
    }
}
