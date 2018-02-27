using UnityEngine;
using System.Collections;

public class BreakableWall : MonoBehaviour {
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
    private bool startWall;
	
	[SerializeField]
	private float wallSpeed;

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.7f);
        deathWall.transform.position = new Vector2(gameObject.transform.position.x - 30, deathWall.transform.position.y);
        deathWall.GetComponent<DeathWallMove>().speed = wallSpeed;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player.GetComponent<Dash>().trailSize > 0)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            smash.Play();
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(wall1);
            Destroy(wall2);
            Destroy(wall3);
            if (startWall)
            {
                GameObject.Find("Player").transform.position = new Vector2(gameObject.transform.position.x + 0.3f, GameObject.Find("Player").transform.position.y);
                StartCoroutine(Timer());
            }
        }
    }
}
