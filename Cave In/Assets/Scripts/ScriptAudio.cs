using UnityEngine;
using System.Collections;

public class ScriptAudio : MonoBehaviour {

    public AudioSource JumpSound;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        // audio sound for jump button space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpSound.Play();

        }

        

    }
}
