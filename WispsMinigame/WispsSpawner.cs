using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WispsSpawner : MonoBehaviour
{
    private MainLogic logic;
    public GameObject wisp;
    [SerializeField] private int maxWisps = 10;
    public static int currentWisps = 0, score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //logic = GameObject.FindGameObjectWithTag("MainLogic").GetComponent<MainLogic>();
        InvokeRepeating("SpawnWisp", 1f, 1f);
        StartCoroutine("EndGame");
    }

    private void SpawnWisp ()
    {
        if (currentWisps > maxWisps)
            return;
        //Spawn inside of a random circle
        Vector3 spawnPoint = wisp.transform.position;
        if (Random.Range(0, 1) == 0) 
            spawnPoint.x += Random.Range(0f,2f);
        else
            spawnPoint.x -= Random.Range(0f,2f);
        if (Random.Range(0, 1) == 0)
            spawnPoint.y += Random.Range(0f,2f);
        else
            spawnPoint.y -= Random.Range(0f,2f);
        //Spawn the wisp
        GameObject spawnedWisp = Instantiate(wisp, spawnPoint, Quaternion.identity, gameObject.transform);
        spawnedWisp.SetActive(true);
        currentWisps++;
    }

    IEnumerator EndGame ()
    {
        yield return new WaitForSeconds(20);
        //PlayerController info = GameObject.FindGameObjectWithTag("MainLogic").GetComponent<PlayerController>();
        //End Game
        if (score >= 10)
            Debug.Log("");
        else if (score >= 5)
            Debug.Log("");
        else
            Debug.Log("");
        SceneManager.LoadScene("InterviewRoom 1");
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
