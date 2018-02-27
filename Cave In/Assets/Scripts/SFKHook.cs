using UnityEngine;
using System.Collections;

public class SFKHook : MonoBehaviour {

    public AudioSource SfxHook;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // audio sound for jump button space
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SfxHook.Play();

        }


    }
}
