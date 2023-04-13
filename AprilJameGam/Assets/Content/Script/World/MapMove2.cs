using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapMove2 : MonoBehaviour
{

    // The teleport point game object
    public GameObject teleportPoint;

    private bool playerInRange = false;

    // Move the Camera
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;

    private CameraMovement camMove;

    // Place Names on teleport
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Prefab to spawn and destroy
    public GameObject prefabToSpawn;

    void Start()
    {
        // Get the Camera
        camMove = Camera.main.GetComponent<CameraMovement>();
    }

    // InteractText > enable
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
            // Spawn the prefab and wait for 1 second
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
            StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab, 1f));

            // Start the PlaceName coroutine
            StartCoroutine(placeNameCo());

            // Move the player game object to the teleport point's position
            GameObject.FindGameObjectWithTag("Player").transform.position = teleportPoint.transform.position;

            camMove.maxPosition += cameraChangeMax;
            camMove.minPosition += cameraChangeMin;
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

    private IEnumerator DestroyPrefabAfterDelay(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(prefab);
    }
}
