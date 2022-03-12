using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public bool isUp = true;
    private bool isPlayerOn = false;
    [HideInInspector] public bool isMoving = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOn && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            anim.SetTrigger(isUp ? "GoUp" : "GoDown");
            isMoving = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerOn = true;
            other.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerOn = false;
            other.transform.parent = null;
        }
    }
}
