using UnityEngine;
using System.Collections;

public class DeathSFX : MonoBehaviour {


    public AudioSource deathSound;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {

            deathSound.Play();

        }
    }

}
