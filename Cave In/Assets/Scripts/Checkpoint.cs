using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    [SerializeField]
    private GameObject mainCamera;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            mainCamera.GetComponent<CameraMovement>().inCheckpoint = true;
            mainCamera.GetComponent<CameraMovement>().checkpointCamera = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -15);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            mainCamera.GetComponent<CameraMovement>().inCheckpoint = false;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
