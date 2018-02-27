using UnityEngine;
using System.Collections;

public class CoinSFX : MonoBehaviour {


    public AudioSource collectCoin;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
     

	}


    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.name == "Player")
        {
            collectCoin.Play();
        }
    }
}
