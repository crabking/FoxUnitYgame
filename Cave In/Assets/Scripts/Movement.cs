using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    private Vector2 playerVelocity;
    [SerializeField]
    //gets the rigidbody in order to apply forces to the player
    private Rigidbody2D rb;

    //variables for force calculations
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float time;
    private float force;

    [SerializeField]
    private float airControl;

    //a boolean for when the player is walljumping and should not have air control
    public int rebounding;

    //variables for ground stomp (falling faster)
    [SerializeField]
    private float maxDownVelocity;
    private float downForce;
    [SerializeField]
    private float fallTime;

    //player respawnpoint 
    public Vector3 respawnPoint;


    // health stuf
    public int currentHealth;
    public int maxHealth;

    public bool dead;
    

    void Start()
    {
        rebounding = 0;

        // makes sure the current health doesnt exceed the max health
        currentHealth = 3;
        maxHealth = 3;

        respawnPoint = transform.position;

        dead = false;
    }

    
    void Update()
    {
       

        playerVelocity = GetComponent<Rigidbody2D>().velocity;
        //checks for directional input, disables during walljump, disables while crouching
        if (Input.GetAxis("Horizontal") != 0 && rebounding == 0 && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))
        {
            //checks movement direction and applies force calculations to determine acceleration
            if (Input.GetAxis("Horizontal") > 0)
            {
                force = 100 * (maxVelocity - playerVelocity.x) / time;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                force = -(100 * (maxVelocity + playerVelocity.x) / time);
            }

            

            //adds force to the rigidbody
            
            if (this.gameObject.GetComponent<Jump>().isGrounded)
            {
                rb.AddForce(new Vector2(force * Mathf.Abs(Input.GetAxis("Horizontal")), 0));
            }
            else
            {
                //when player is in the air, has less contol over movement
                if (gameObject.GetComponent<SpringJoint2D>() != null)
                {
                    //if grappling, prevents player from swinging against gravity
                    if (gameObject.GetComponent<GrappleAbility>().x <= 0 && force > 0)
                    {
                        force = 0;
                    }
                    if (gameObject.GetComponent<GrappleAbility>().x >= 0 && force < 0)
                    {
                        force = 0;
                    }
                }
                rb.AddForce(new Vector2(force * Mathf.Abs(Input.GetAxis("Horizontal")) * airControl, 0));
            }
            
        }

        //brakes player movement when movement keys are pressed or when in air, also allows sliding by holding down arrow, also brakes when player is above the max velocity
        if (playerVelocity.x > maxVelocity || playerVelocity.x < -maxVelocity)
        {
            rb.AddForce(new Vector2(-playerVelocity.x * 1000f, 0));
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && gameObject.GetComponent<Jump>().isGrounded)
        {
            rb.AddForce(new Vector2(-playerVelocity.x * 1000f, 0));
        }

        //by holding down key, player can fall faster
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !this.gameObject.GetComponent<Jump>().isGrounded && gameObject.GetComponent<SpringJoint2D>() == null && !gameObject.GetComponent<GrappleAbility>().toGrapple)
        {
            downForce = -(100 * (maxDownVelocity + playerVelocity.y) / fallTime);
            rb.AddForce(new Vector2(0, downForce));
        }

        //prevents launching off ramps when player acsends them quickly
        if (playerVelocity.y > 0 && !this.gameObject.GetComponent<Jump>().isJumping && !gameObject.GetComponent<GrappleAbility>().toGrapple && !this.gameObject.GetComponent<Jump>().isGrounded)
        {
            downForce = -(100 * (maxDownVelocity + playerVelocity.y) / fallTime);
            rb.AddForce(new Vector2(0, downForce));
        }





        // makes sure current health doesnt exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // if current health is lower than or equal to 0 calls the die fuc
        if (currentHealth <= 0 && !dead)
        {
            // small delay

            //Kill();
            dead = true;
            Kill();

        }


    }



    // tirgger checker for the falldetector and the checkpoint
    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.tag == "FallDetector")
        {
            // changest he position of the player to the respawnpoint
            transform.position = respawnPoint;
        }*/

        if (other.tag == "Checkpoint")
        {
            // when reach a checkpoint, the position chances to that checkpoint
            respawnPoint = other.transform.position;
        }

        
        
    }
    /*void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ThornsTest")
        {
            Debug.Log("Before: " + currentHealth);
            coll.gameObject.SendMessage("ApplyDamage", 1); 
            Debug.Log("After: " + currentHealth);
        }

    }*/

    public void Kill()
    {
        gameObject.GetComponent<DeathRespawn>().Die();
    }



    // take damage fuinction
    public void Damage(int dmg)
    {

        // reduces the current health
        currentHealth -= dmg;
    }



}
