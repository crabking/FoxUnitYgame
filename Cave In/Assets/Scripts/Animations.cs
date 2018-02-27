using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

    private Vector2 playerVelocity;
    private Animator playerAnimator;
    private SpriteRenderer faceDirection;
    private BoxCollider2D crouchCollider;
    private PolygonCollider2D standCollider;
    private bool collidersEnabled;
    private int colliderChangeTimer;

    [SerializeField]
    private GameObject rightWalls;
    [SerializeField]
    private GameObject leftWalls;
    [SerializeField]
    private LayerMask groundItems;


    void Start () {
        playerAnimator = GetComponent<Animator>();
        faceDirection = GetComponent<SpriteRenderer>();
        playerVelocity = GetComponent<Rigidbody2D>().velocity;
        crouchCollider = GetComponent<BoxCollider2D>();
        standCollider = GetComponent<PolygonCollider2D>();
        crouchCollider.enabled = true;
        standCollider.enabled = true;
    }
	
	
	void FixedUpdate () {
        playerAnimator.SetFloat("speed", Mathf.Abs(playerVelocity.x));
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            standCollider.enabled = false;
        }
        else
        {
            standCollider.enabled = true;
        }
        
        playerVelocity = GetComponent<Rigidbody2D>().velocity;
        if (gameObject.GetComponent<Jump>().isGrounded)
        {
            playerAnimator.SetInteger("jump", 0);
        }
        else if (!this.gameObject.GetComponent<Jump>().isGrounded && playerVelocity.y >= 0)
        {
            playerAnimator.SetInteger("jump", 1);
        }
        else if (!this.gameObject.GetComponent<Jump>().isGrounded && playerVelocity.y < 0)
        {
            playerAnimator.SetInteger("jump", 2);
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("Crouching", true);
        }
        else
        {
            playerAnimator.SetBool("Crouching", false);
        }

        if(!this.gameObject.GetComponent<Jump>().isGrounded && (Physics2D.IsTouchingLayers(rightWalls.GetComponent<Collider2D>(), groundItems) || Physics2D.IsTouchingLayers(leftWalls.GetComponent<Collider2D>(), groundItems)))
        {
            if (Physics2D.IsTouchingLayers(leftWalls.GetComponent<Collider2D>(), groundItems))
            {
                faceDirection.flipX = false;
            }
            if (Physics2D.IsTouchingLayers(rightWalls.GetComponent<Collider2D>(), groundItems))
            {
                faceDirection.flipX = true;
            }
        }
        else if (playerVelocity.x != 0)
        {
            if (playerVelocity.x > 1)
            {
                faceDirection.flipX = false;
            }
            if (playerVelocity.x < -1)
            {
                faceDirection.flipX = true;
            }
        }
        
        if (gameObject.GetComponent<Dash>().dashAnimation)
        {
            playerAnimator.SetTrigger("dashAttack");
            gameObject.GetComponent<Dash>().dashAnimation = false;
        }
    }
}



/*  References:
 *  Fox Character sprites and animations - IDK Studios, Unity Asset Store free assets - https://www.assetstore.unity3d.com/en/#!/content/59175
 *  Mine Tileset - Grande Pixel, Unity Asset Store free assets - https://www.assetstore.unity3d.com/en/#!/content/43476
 *  Sounds - Dustyroom, Unity Asset Store free assets - https://www.assetstore.unity3d.com/en/#!/content/54116
 *  Music - Muz Station Productions, Unity Asset Store free assets - https://www.assetstore.unity3d.com/en/#!/content/70362
 *  Menu Fonts - http://font.chinaz.com
 *  Menu Sound effects - http://www.2gei.com/sound/
 *  Menu tutorial - http://www.cg.com.tw/Unity
 */