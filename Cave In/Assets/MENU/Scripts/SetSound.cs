using UnityEngine;
using System.Collections;

public class SetSound : MonoBehaviour {

	AudioSource ads;
	void Start(){
		if (GetComponent<AudioSource> ()) {
			ads = GetComponent<AudioSource> ();
			SetSV ();
		}
	}
	void SetSV(){
		ads.volume = BackGroundMusic.soundSize;
	}
}
