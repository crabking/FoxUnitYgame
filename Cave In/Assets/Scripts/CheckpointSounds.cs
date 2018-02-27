using UnityEngine;
using System.Collections;

public class CheckpointSounds : MonoBehaviour {

    public AudioSource CheckpointSFX;

    public bool checkpointReached2;

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
            // makes sure the SFX is only fired of once
            if (this.enabled)
            {
                this.enabled = false;

                checkpointReached2 = true;
                // check if player has reached checkpoint, if so play sound
                if (checkpointReached2 == true)
                {
                    CheckpointSFX.Play();
                }

            }
        }
    }


    

}
