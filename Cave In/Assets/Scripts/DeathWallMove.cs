using UnityEngine;
using System.Collections;

public class DeathWallMove : MonoBehaviour {
    [SerializeField]
    public float speed;

    [SerializeField]
    private ParticleSystem rocks;
    [SerializeField]
    private ParticleSystem smallRocks;
    [SerializeField]
    private ParticleSystem dust;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed, gameObject.transform.position.y, gameObject.transform.position.z);
        if(speed == 0 && rocks.isPlaying)
        {
            rocks.Stop();
            smallRocks.Stop();
            dust.Stop();
            rocks.Clear();
            smallRocks.Clear();
            dust.Clear();
        }
        if (speed > 0 && !rocks.isPlaying)
        {
            rocks.Play();
            smallRocks.Play();
            dust.Play();
        }
    }
}
