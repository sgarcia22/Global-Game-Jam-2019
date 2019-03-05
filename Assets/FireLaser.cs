using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    //public GameObject laser;
   // private Vector3 init;
    private int count = 0;
    public Animator animator;
    public AudioClip myclip;
    public float thisTime;
    private float adjustTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //laser.SetActive(false);
        this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().clip = myclip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //laser.SetActive(true);
            animator.SetBool("isShot", true);
            this.GetComponent<AudioSource>().Play();

        }
        //count++;

        if (Time.timeSinceLevelLoad - adjustTime >= thisTime)
        {
            adjustTime = Time.timeSinceLevelLoad;

            animator.SetBool("isShot", false);
            //count = 0;
        }

    }
}
