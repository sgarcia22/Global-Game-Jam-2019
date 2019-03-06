using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == gameObject.name)
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.SetActive(false);
            PuzzleGameManager.puzzleCount++;
            if (PuzzleGameManager.puzzleCount == 9)
            {
                PuzzleGameManager.End();
            }
        }
    }
}
