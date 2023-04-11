using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class MapMove : MonoBehaviour
{
    // The teleport point game object
    public GameObject teleportPoint;

    //UI Fadeout
    public float fadeDuration = 1f;
    public Image fadePanel;

    private bool playerInRange = false;

    //Move the Camera
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;

    private CameraMovement camMove;

    //Place Names on teleport
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    void Start()
    {
        //Get the Camera
        camMove = Camera.main.GetComponent<CameraMovement>();
    }

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
            // Play the fade animation
            StartCoroutine(FadeCoroutine());
            StartCoroutine(placeNameCo());

        }
    }

    IEnumerator FadeCoroutine()
    {
        // Fade in
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Move the player game object to the teleport point's position
        GameObject.FindGameObjectWithTag("Player").transform.position = teleportPoint.transform.position;

        camMove.maxPosition += cameraChangeMax;
        camMove.minPosition += cameraChangeMin;

        // Fade out
        elapsed = 0;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

    }


    private IEnumerator placeNameCo()
    {
        yield return new WaitForSeconds(1.5f);
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
