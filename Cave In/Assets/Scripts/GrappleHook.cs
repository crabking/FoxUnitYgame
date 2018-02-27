using UnityEngine;
using System.Collections;

public class GrappleHook : MonoBehaviour {

    //get the player to position the hook when it spawns
    private GameObject player;
    private GameObject mainCamera;

    //variables to control the hook movement and keep it consistent
    private float grappleX;
    private float grappleY;
    private float grappleMagnitude;
    private float grappleNormX;
    private float grappleNormY;
	
	private float frustumHeight;
	private float frustumWidth;

    //variables to indicate if the hook has anchored, or if it is returning to the player
    public bool anchored;
    public bool grappleReturn;

    //a layermask for things the hook can anchor to
    [SerializeField]
    private LayerMask groundItems;

    void Start () {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
        //initialises the hook at players location
        this.gameObject.transform.position = player.GetComponent<GrappleAbility>().location;

        //detects mouse position and offset from player, calculates direction of movement
		frustumHeight = 2.0f * (player.transform.position.z - mainCamera.transform.position.z) * Mathf.Tan(mainCamera.GetComponent<Camera>().fieldOfView * 0.5f * Mathf.Deg2Rad);
		
        grappleX = (Input.mousePosition.x - (Screen.width * 0.5f)) / (Screen.height / 2) + (mainCamera.transform.position.x - player.transform.position.x) / (frustumHeight / 2);
        grappleY = (Input.mousePosition.y - (Screen.height * 0.5f)) / (Screen.height / 2) + (mainCamera.transform.position.y - player.transform.position.y) / (frustumHeight / 2);
        grappleMagnitude = Mathf.Sqrt((grappleX * grappleX) + (grappleY * grappleY));
        grappleNormX = grappleX / grappleMagnitude;
        grappleNormY = grappleY / grappleMagnitude;
        anchored = false;
        grappleReturn = false;
    }
	
	void FixedUpdate () {
        //locks the grapple when it hits a surface
        if (Physics2D.IsTouchingLayers(this.gameObject.GetComponent<Collider2D>(), groundItems) && !anchored)
        {
            anchored = true;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        //sends hook towards the mouse when not anchored
        else if(!anchored)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(grappleNormX * 25, grappleNormY * 25);
        }

        //returns the hook to the player before destroying it when the ability calls for it
        if (grappleReturn)
        {
            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-GameObject.Find("Player").GetComponent<GrappleAbility>().normX * 25, -GameObject.Find("Player").GetComponent<GrappleAbility>().normY * 25);
            if (GameObject.Find("Player").GetComponent<GrappleAbility>().magnitude < 0.3)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
