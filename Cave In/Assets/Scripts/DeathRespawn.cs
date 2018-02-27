using UnityEngine;
using System.Collections;

public class DeathRespawn : MonoBehaviour {

    [SerializeField]
    private GameObject deathWall;
	
	public Vector2 respawnLocation;

    private bool death;
    private float deathFade;
    private bool life;
    private float lifeFade;

	// Use this for initialization
	void Start () {
        Respawn();
	}
	
	// Update is called once per frame
	void Update () {
        if (death)
        {
            deathFade += 0.006f;
            if (Time.timeScale > 0.009f) { Time.timeScale -= 0.009f; }
            GameObject.Find("fadeToBlack").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, deathFade);
        }
        if (deathFade >= 1f && !life)
        {
            Time.timeScale = 1f;
            death = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //Respawn();
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
	}
	
	void FixedUpdate () {
        if (life)
        {
            deathFade -= 0.01f;
            lifeFade += 0.005f;
            GameObject.Find("fadeToBlack").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, deathFade);
            if (lifeFade > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, lifeFade);
            }
        }
        if(lifeFade >= 1f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            life = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            lifeFade = 0f;
			gameObject.GetComponent<GrappleAbility>().enabled = true;
        }
    }

    public void Die()
    {
        //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        death = true;
        deathFade = 0;
    }

    public void Respawn()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        life = true;
        lifeFade = -0.9f;
        deathFade = 3f;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.transform.position = respawnLocation;
        GameObject.Find("RespawnEffect").GetComponent<ParticleSystem>().Play();
        //deathWall.GetComponent<DeathWallMove>().speed = 0.1f;
        deathWall.transform.position = new Vector2(-60, deathWall.transform.position.y);
    }
}
