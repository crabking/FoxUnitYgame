using UnityEngine;
using System.Collections;

public class GrappleAbility : MonoBehaviour {

    //a vector to tell the hook where to spawn
    public Vector3 location;

    //this attatches to the prefab of the grappling hook, and spawns copies of it when instantiated
    [SerializeField]
    private GameObject grapple;

    //gets the rigidbody to allow the hook to apply forces to the player
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float grappleRange;

    //vector calculations for grappling forces and distances
    public float x;
    private float y;
    public float magnitude;
    public float normX;
    public float normY;
    private Vector2 playerVelocity;

    //a springjoint to allow the player to swing on the rope
    private SpringJoint2D grabHinge;

    //indicates when the player is being pulled to the hook
    public bool toGrapple;
    private bool instaGrapple;

    void Start () {
        magnitude = 0;
        toGrapple = false;
        instaGrapple = false;
	}

	void Update () {
        playerVelocity = GetComponent<Rigidbody2D>().velocity;

        //vector calculations, occurs only if a hook object exists
        if (GameObject.Find("grapple(Clone)") != null)
        {
            x = GameObject.Find("grapple(Clone)").transform.position.x - this.gameObject.transform.position.x;
            y = GameObject.Find("grapple(Clone)").transform.position.y - this.gameObject.transform.position.y;
            magnitude = Mathf.Sqrt((x * x) + (y * y));
            normX = x / magnitude;
            normY = y / magnitude;
        }
        //resets if the hook is destroyed
        else
        {
            x = 0;
            y = 0;
            magnitude = 0;
            normX = 0;
            normY = 0;
        }

        //with input, this insantiates the grapple, or tells it to return and destroy if one already exists
        if (Input.GetButtonDown("grapple"))
        {
            if (GameObject.Find("grapple(Clone)") != null)
            {
                if (gameObject.GetComponent<SpringJoint2D>() != null)
                {
                    Destroy(gameObject.GetComponent<SpringJoint2D>());
                }
                GameObject.Find("grapple(Clone)").GetComponent<GrappleHook>().grappleReturn = true;
                toGrapple = false;
                instaGrapple = false;
            }
            else
            {
                location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                Instantiate(grapple);
                magnitude = 0;
            }
        }

        if (GameObject.Find("grapple(Clone)") == null && Input.GetAxis("pullGrapple") > 0)
        {
            location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            Instantiate(grapple);
            magnitude = 0;
            instaGrapple = true;
        }

        if (GameObject.Find("grapple(Clone)") != null)
        {
            if (GameObject.Find("grapple(Clone)").GetComponent<GrappleHook>().anchored && instaGrapple)
            {
                toGrapple = true;
                instaGrapple = false;
            }
        }
        //destroys joints and sets grapple to pull if input is recieved
        if (GameObject.Find("grapple(Clone)") != null && Input.GetAxis("pullGrapple") > 0)
        {
            if (GameObject.Find("grapple(Clone)").GetComponent<GrappleHook>().anchored)
            {
                Destroy(gameObject.GetComponent<SpringJoint2D>());
                toGrapple = true;
            }
        }

        

        //if not pulling, will catch a falling player on a spring joint, which will act as a pendulum rope
        else if (GameObject.Find("grapple(Clone)") != null && !this.gameObject.GetComponent<Jump>().isGrounded && !toGrapple && !GameObject.Find("grapple(Clone)").GetComponent<GrappleHook>().grappleReturn)
        {
            if (GameObject.Find("grapple(Clone)").GetComponent<GrappleHook>().anchored && gameObject.GetComponent<SpringJoint2D>() == null && playerVelocity.y < 0 && y > -0.2)
            {
                grabHinge = gameObject.AddComponent<SpringJoint2D>();
                grabHinge.connectedBody = GameObject.Find("grapple(Clone)").GetComponent<Rigidbody2D>();
                grabHinge.autoConfigureDistance = false;
                grabHinge.distance = magnitude;
                grabHinge.frequency = 10;
            }
            //with input, the player can slowly ascend or descend the rope
            if (Input.GetAxis("Vertical") > 0 && gameObject.GetComponent<SpringJoint2D>() != null && magnitude > 1)
            {
                grabHinge.distance = grabHinge.distance - 0.03f;
            }
            if (Input.GetAxis("Vertical") < 0 && gameObject.GetComponent<SpringJoint2D>() != null && magnitude < 12)
            {
                grabHinge.distance = grabHinge.distance + 0.03f;
            }
        }

        //prevents joint from being made when player is grounded
        if ((this.gameObject.GetComponent<Jump>().isGrounded && gameObject.GetComponent<SpringJoint2D>() != null) || y < -0.2)
        {
            Destroy(gameObject.GetComponent<SpringJoint2D>());
        }

        //causes the grapple to return if it goes too far from the player, or if the player jumps in mid air
        if (GameObject.Find("grapple(Clone)") != null && (magnitude > grappleRange || (Input.GetButtonDown("Jump") && !this.gameObject.GetComponent<Jump>().isGrounded)))
        {
            Destroy(gameObject.GetComponent<SpringJoint2D>());
            GameObject.Find("grapple(Clone)").GetComponent<GrappleHook>().grappleReturn = true;
        }

        //draws the rope to the hook, if the hook exists
        if (GameObject.Find("grapple(Clone)") != null)
        {
            gameObject.GetComponent<LineRenderer>().enabled = true;
            gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(x, y, 0));
        }
        else
        {
            gameObject.GetComponent<LineRenderer>().enabled = false;
        }

        //drags the player to the hook if enabled
        if (toGrapple)
        {
            rb.AddForce(new Vector2(normX * 3000, normY * 3000));
            if (magnitude < 1)
            {
                toGrapple = false;
                if (GameObject.Find("grapple(Clone)") != null) { GameObject.Find("grapple(Clone)").GetComponent<GrappleHook>().grappleReturn = true; }
            }
        }
	}
}
