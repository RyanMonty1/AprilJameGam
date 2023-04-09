using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class MapMove : MonoBehaviour
{
    // The teleport point game object
    public GameObject teleportPoint;

    public float fadeDuration = 1f;
    public Image fadePanel;

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
            // Play the fade animation
            StartCoroutine(FadeCoroutine());
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
}
