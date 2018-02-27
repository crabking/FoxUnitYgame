using UnityEngine;
using System.Collections;

public class BreakableWallEnd : MonoBehaviour {
    [SerializeField]
    private GameObject deathWall;

    [SerializeField]
    private ParticleSystem smash;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject wall1;
    [SerializeField]
    private GameObject wall2;
    [SerializeField]
    private GameObject wall3;

    [SerializeField]
    private ParticleSystem dust;
    [SerializeField]
    private ParticleSystem smoke;

    [SerializeField]
    private GameObject newWall1;
    [SerializeField]
    private GameObject newWall2;

    private bool broken;
    private float fade;

    // Use this for initialization
    void Start () {
        broken = false;
	}

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        newWall1.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        newWall2.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        newWall1.GetComponent<SpriteRenderer>().enabled = true;
        newWall2.GetComponent<SpriteRenderer>().enabled = true;
        fade = 0;
        deathWall.transform.position = new Vector2(gameObject.transform.position.x - 60, deathWall.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!broken)
        {
            if (player.GetComponent<Dash>().trailSize > 0)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                gameObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }

        if (newWall1.GetComponent<SpriteRenderer>().enabled && fade < 1f)
        {
            newWall1.GetComponent<SpriteRenderer>().color = new Color(200, 200, 200, fade);
            fade += 0.01f;
        }
        if (newWall2.GetComponent<SpriteRenderer>().enabled && fade < 1f)
        {
            newWall2.GetComponent<SpriteRenderer>().color = new Color(200, 200, 200, fade);
            fade += 0.01f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            GameObject.Find("Player").transform.position = new Vector2(gameObject.transform.position.x + 0.3f, GameObject.Find("Player").transform.position.y);
            smash.Play();
            broken = true;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            Destroy(wall1);
            Destroy(wall2);
            Destroy(wall3);
            dust.Play();
            smoke.Play();
            StartCoroutine(Timer());
            deathWall.transform.position = new Vector2(gameObject.transform.position.x - 10, deathWall.transform.position.y);
            deathWall.GetComponent<DeathWallMove>().speed = 0;
        }
    }
}
