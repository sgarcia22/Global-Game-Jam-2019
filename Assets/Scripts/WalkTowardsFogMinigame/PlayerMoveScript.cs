using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 10f;
    private Vector3 origPosition;
    private float fogRange = 100f, maxFogDensity = 0.1f;
    public bool isColide;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        origPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || GvrControllerInput.ClickButton)
        {
            Debug.Log("Pressing Button");
            Vector3 moveTo = Camera.main.transform.forward;
            moveTo.y = 0f;
            transform.position += moveTo * speed * Time.deltaTime;
        }
        float dist = Vector3.Distance(transform.position, origPosition);
        //https://answers.unity.com/questions/12311/increasedecrease-fog-density-with-distance.html
        RenderSettings.fogDensity = (1 - Mathf.Clamp01(dist / fogRange)) * maxFogDensity;
        if (isColide/*RenderSettings.fogDensity < .01f*/)
        {
            //End Minigame
            SceneManager.LoadScene("InterviewRoom 1");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "warp")
        {
            Debug.Log("PINGAS");
            isColide = true;
            // playerCollides with the Enemy
        }
    }
}
