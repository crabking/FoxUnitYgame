using UnityEngine;
using System.Collections;

public class AudioLand : MonoBehaviour {

   

    public AudioSource LandAudio;

    //variables for the height of each jump, and to allow duoble jumping
    [SerializeField]
    private int jumpHeight;
    private Vector2 playerVelocity;
    public bool isGrounded;
    private bool doubleJump;
    [SerializeField]
    private LayerMask groundItems;

    //initialize two empty objects, these are used to detect if the player is touching a wall when they jump
    [SerializeField]
    private GameObject rightWalls;
    [SerializeField]
    private GameObject leftWalls;

    //the outward velocity of a wall jump
    [SerializeField]
    private int wallJumpForce;

    //variables to control longer jumps by holding down space
    private bool duringJump;
    private int jumpTime;

    //accessing the rigidbody for force based jumps
    [SerializeField]
    private Rigidbody2D rb;

    public bool isJumping;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // audio sound for jump button space
        if (isGrounded)
        {
            LandAudio.Play();

        }


    }

    void FixedUpdate()
    {
    
            //checks if the player is on the ground
            if (Physics2D.IsTouchingLayers(this.gameObject.GetComponent<PolygonCollider2D>(), groundItems) || Physics2D.IsTouchingLayers(this.gameObject.GetComponent<BoxCollider2D>(), groundItems))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
     }

    
}
