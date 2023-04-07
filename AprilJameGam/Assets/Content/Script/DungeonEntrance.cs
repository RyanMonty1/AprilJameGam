using UnityEngine;
using UnityEngine.UI;

public class DungeonEntrance : MonoBehaviour
{
    // The teleport point game object
    public GameObject teleportPoint;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Activate the InteractText UI object
            Transform canvasTransform = other.gameObject.transform.Find("Canvas");
            if (canvasTransform != null)
            {
                Transform interactTextTransform = canvasTransform.Find("InteractText");
                if (interactTextTransform != null)
                {
                    interactTextTransform.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Deactivate the InteractText UI object
            Transform canvasTransform = other.gameObject.transform.Find("Canvas");
            if (canvasTransform != null)
            {
                Transform interactTextTransform = canvasTransform.Find("InteractText");
                if (interactTextTransform != null)
                {
                    interactTextTransform.gameObject.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Move the player game object to the teleport point's position
            GameObject.FindGameObjectWithTag("Player").transform.position = teleportPoint.transform.position;
        }
    }
}
