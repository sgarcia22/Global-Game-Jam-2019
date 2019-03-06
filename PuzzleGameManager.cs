using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleGameManager : MonoBehaviour
{

    public static int puzzleCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void End ()
    {
        //Change the scene
        if (SceneManager.GetActiveScene().name == "PuzzleAdoption")
        {
            SceneManager.LoadScene("ENDING NEW Adoption");
        }
        else if (SceneManager.GetActiveScene().name == "PuzzleGroup")
        {
            SceneManager.LoadScene("ENDING Group");
        }
        else
            SceneManager.LoadScene("ENDING Nature Solitary");
    }
}
