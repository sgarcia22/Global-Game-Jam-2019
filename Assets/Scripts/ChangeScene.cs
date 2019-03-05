// SceneB.
// SceneB is given the sceneBuildIndex of 0 which will
// load SceneA from the Build Settings

using UnityEngine;
using UnityEngine.SceneManagement;



public class ChangeScene : MonoBehaviour
{
    public int i;
    public bool isQuitObject;
    bool isColide;



    void Start()
    {
        isColide = false;
    }

    void Update()
    {
        LoadSceneNumberFromArr(i);  
    }

    public void LoadSceneNumberFromArr(int i)
    {
      
        if (isColide)
        {
            
            if (isQuitObject)
            {
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }

            Debug.Log("sceneBuildIndex to load: " + i);
            SceneManager.LoadScene(i);
        }
    }

    public void ForceScene(int i)
    {
        

            if (isQuitObject)
            {
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }

            Debug.Log("sceneBuildIndex to load: " + i);
            SceneManager.LoadScene(i);

    }
  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PINGAS");
            isColide = true;
            // playerCollides with the Enemy
        }
    }
}