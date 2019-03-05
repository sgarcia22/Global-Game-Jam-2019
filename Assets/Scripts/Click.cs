using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public GameObject Object;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length != 0 || Input.GetMouseButtonDown(0))
        {
            Debug.Log("touch");
            Object.SetActive(false);
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
        }
        */
    }
}
