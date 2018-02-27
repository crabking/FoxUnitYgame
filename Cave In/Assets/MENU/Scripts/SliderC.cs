using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SliderC : MonoBehaviour {

	Slider slider;
	void Start () {
		slider = GetComponent<Slider> ();
		BackGroundMusic.SetSoundSize (slider.value);
	}
	

	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			BackGroundMusic.SetSoundSize (slider.value);
			BackGroundMusic.SetSoundV ();
		}
	}
}
