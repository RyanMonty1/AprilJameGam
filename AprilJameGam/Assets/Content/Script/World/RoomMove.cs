using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;

    private CameraMovement camMove;

    // for Map names
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;


    void Start()
    {
        camMove = Camera.main.GetComponent<CameraMovement>();
    }


    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            camMove.maxPosition += cameraChange;
            camMove.minPosition += cameraChange;
            col.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
