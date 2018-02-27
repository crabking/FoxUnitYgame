using UnityEngine;
using System.Collections;

public class EnemyBat : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private LayerMask groundItems;
    [SerializeField]
    private LayerMask playerItems;

    private bool ready;

    private bool attack;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player" && ready)
        {
            StartCoroutine(Warning());
            ready = false;
        }
    }

    IEnumerator Warning()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
        yield return new WaitForSeconds(0.3f);
        attack = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    IEnumerator DamageTime()
    {
        explosion.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        if (explosion.GetComponent<CircleCollider2D>().enabled)
        {
            explosion.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void Start () {
        attack = false;
        ready = true;
	}
	
	void FixedUpdate () {
        if(attack)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((player.GetComponent<Rigidbody2D>().position.x - gameObject.GetComponent<Rigidbody2D>().position.x) * 10, (player.GetComponent<Rigidbody2D>().position.y - gameObject.GetComponent<Rigidbody2D>().position.y) * 4));
            if (Physics2D.IsTouchingLayers(this.gameObject.GetComponent<CircleCollider2D>(), groundItems) || Physics2D.IsTouchingLayers(this.gameObject.GetComponent<CircleCollider2D>(), playerItems))
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                explosion.GetComponent<ParticleSystem>().Play();
                attack = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                StartCoroutine(DamageTime());
            }
        }
    }
}
