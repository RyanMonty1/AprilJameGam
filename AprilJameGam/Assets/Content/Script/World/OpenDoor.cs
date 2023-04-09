using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class OpenDoor : MonoBehaviour
{

    public GameObject openDoor;
    public GameObject closeDoor;

    void Start()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            openDoor.SetActive(true);
            closeDoor.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            openDoor.SetActive(false);
            closeDoor.SetActive(true);
        }
    }
}
