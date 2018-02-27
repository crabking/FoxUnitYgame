using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
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

    void Start()
    {
        
    }

    //initiate a timer function that stops air control during a wall jump
    IEnumerator Timer()
    {
        this.gameObject.GetComponent<Movement>().rebounding += 1;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<Movement>().rebounding -= 1;
    }

    void Update()
    {
        //checks if the player is on the ground
        if (Physics2D.IsTouchingLayers(this.gameObject.GetComponent<BoxCollider2D>(), groundItems))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //get the velocity of the player
        playerVelocity = GetComponent<Rigidbody2D>().velocity;

        if (isGrounded)
        {
            //when on the ground, readies a double jump
            doubleJump = false;
            if (gameObject.GetComponent<SpringJoint2D>() == null)
            {
                isJumping = false;
            }
        } 
        
        //causes the game to register grappling as being mid jump, and prevents double jumping from a grapple
        if (GameObject.Find("Player").GetComponent<GrappleAbility>().toGrapple || gameObject.GetComponent<SpringJoint2D>() != null)
        {
            isJumping = true;
            if (gameObject.GetComponent<SpringJoint2D>() != null) { doubleJump = false; }
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerJump();
        }

        //here the player can hold the button for a longer jump, it will end when they let go or after a short time
        if (!Input.GetKey(KeyCode.Space) || jumpTime <= 0)
        {
            duringJump = false;
        }
        if (duringJump)
        {
            rb.AddForce(new Vector2(0, jumpTime * jumpHeight * 100));
            jumpTime--;
            isJumping = true;
        }

       
    }

    //function for the jump
    void playerJump()
    {
        //checks if touching a wall
        if (Physics2D.IsTouchingLayers(rightWalls.GetComponent<Collider2D>(), groundItems))
        {
            rightJump();
            //registered the double jump used if the player wall jumps
            doubleJump = true;
            isJumping = true;
        }
        else if (Physics2D.IsTouchingLayers(leftWalls.GetComponent<Collider2D>(), groundItems))
        {
            leftJump();
            //registered the double jump used if the player wall jumps
            doubleJump = true;
            isJumping = true;
        }
        //checks if a double jump is allowed
        else if (!doubleJump)
        {
            if (!isGrounded || isJumping)
            {
                //registered the double jump used if the player jumps while not touching the ground
                doubleJump = true;
            }
            //resets horizontal velocity before applying jump forces
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerVelocity.x, 0);
            duringJump = true;
            isJumping = true;
            jumpTime = 10;
        }
    }

    //these two functions are called to propel the player away from walls, creating a wall jump, they start a coroutine that disables air control of the player for a moment during the jump, to stop the player from gaining height by jumping against a single wall
    void rightJump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-wallJumpForce, jumpHeight);
        StartCoroutine(Timer());
    }

    void leftJump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(wallJumpForce, jumpHeight);
        StartCoroutine(Timer());
    }
}
