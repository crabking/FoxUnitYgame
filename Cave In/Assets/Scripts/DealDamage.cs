using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    public AudioSource damageSound;

    [SerializeField]
    private bool fatal;
    [SerializeField]
    private bool knockback;
    [SerializeField]
    private bool oneTime;

    private float xDamage;
    private float yDamage;
    private float magnitudeDamage;
    private float normXDamage;
    private float normYDamage;

    IEnumerator Timer()
    {
        player.GetComponent<Movement>().rebounding += 1;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Movement>().rebounding -= 1;
    }

    void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (!fatal && !player.GetComponent<Damage>().damageFrames)
            {
				damageSound.Play();
                if (knockback && !player.GetComponent<Damage>().damageFrames)
                {
                    xDamage = player.transform.position.x - gameObject.transform.position.x;
                    yDamage = player.transform.position.y - gameObject.transform.position.y;
                    magnitudeDamage = Mathf.Sqrt((xDamage * xDamage) + (yDamage * yDamage));
                    normXDamage = xDamage / magnitudeDamage;
                    normYDamage = yDamage / magnitudeDamage;
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(normXDamage * 50000, normYDamage * 50000));
                }
                player.GetComponent<Damage>().TakenDamage();
                if (GameObject.Find("grapple(Clone)") != null)
                {
                    Destroy(GameObject.Find("grapple(Clone)"));
                }
                StartCoroutine(Timer());
                if (oneTime) { gameObject.GetComponent<Collider2D>().enabled = false; }
                if (player.GetComponent<Movement>().currentHealth > 0) { player.GetComponent<Movement>().currentHealth -= 1; }
            }
            if (fatal)
            {
                player.GetComponent<DeathRespawn>().Die();
            }
        }
    }
}
