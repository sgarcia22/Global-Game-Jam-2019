using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGrab : MonoBehaviour
{
    public GameObject grabable;
    public GameObject myHand;

    bool inHands;

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            if (inHands)
            {
                this.GetComponent<PlayerGrab>().enabled = false;
                grabable.transform.SetParent(null);
                inHands = false;
            }

            else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                grabable = hit.transform.gameObject;
                grabable.transform.SetParent(myHand.transform);
                grabable.transform.localPosition = myHand.transform.localPosition;
                inHands = true;
            }
        }
        if (grabable.activeSelf)
        {
            inHands = false;
        }
    }
}
