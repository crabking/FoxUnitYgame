using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

    [SerializeField]
    //gets the rigidbody in order to apply forces to the player
    private Rigidbody2D rb;

    private float rightDoubleTapTimer;
    private float leftDoubleTapTimer;
    private int rightTapCount;
    private int leftTapCount;
    public int trailSize;
    public bool dashAnimation;

    // Use this for initialization
    void Start () {
        rightTapCount = 0;
        leftTapCount = 0;
        rightDoubleTapTimer = 0;
        leftDoubleTapTimer = 0;
        trailSize = 0;
        dashAnimation = false;
    }

    // Update is called once per frame
    void Update() {
        if (trailSize > 0)
        {
            SpawnTrail();
            trailSize--;
        }

        if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && GameObject.Find("grapple(Clone)") == null)
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            {
                if (rightDoubleTapTimer > 0 && rightTapCount > 0)
                {
                    rb.AddForce(new Vector2(200000, 0));
                    rightTapCount = 0;
                    trailSize = 15;
                    dashAnimation = true;
                    rightDoubleTapTimer = 0f;
                }
                else
                {
                    rightDoubleTapTimer = 0.3f;
                    rightTapCount += 1;
                }
                leftDoubleTapTimer = 0;
            }

            if (rightDoubleTapTimer > 0)
            {
                rightDoubleTapTimer -= 1 * Time.deltaTime;
            }
            else
            {
                rightTapCount = 0;
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            {
                if (leftDoubleTapTimer > 0 && leftTapCount > 0)
                {
                    rb.AddForce(new Vector2(-200000, 0));
                    leftTapCount = 0;
                    trailSize = 15;
                    dashAnimation = true;
                    leftDoubleTapTimer = 0f;
                }
                else
                {
                    leftDoubleTapTimer = 0.3f;
                    leftTapCount += 1;
                }
                rightDoubleTapTimer = 0;
            }

            if (leftDoubleTapTimer > 0)
            {
                leftDoubleTapTimer -= 1 * Time.deltaTime;
            }
            else
            {
                leftTapCount = 0;
            }

        }

    }

    void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            trailPartRenderer.flipX = true;
        }
        trailPart.transform.position = transform.position;
        Color color = trailPartRenderer.color;
        color.a -= 0.7f;
        trailPartRenderer.color = color;

        StartCoroutine(FadeTrailPart(trailPartRenderer, trailPart));
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer, GameObject trailPart)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.02f;
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
        if (color.a > 0)
        {
            StartCoroutine(FadeTrailPart(trailPartRenderer, trailPart));
        }
        else
        {
            Destroy(trailPart);
        }
    }

}
