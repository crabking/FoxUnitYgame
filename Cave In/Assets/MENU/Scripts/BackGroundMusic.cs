using UnityEngine;
using System.Collections;

public class BackGroundMusic : MonoBehaviour {
	
	public static float soundSize = 0.05f;
	public static void SetSoundSize(float size){
		soundSize = size;
	}
	public static void SetSoundV(){
		if (GameObject.Find ("Music")) {
			GameObject.Find ("Music").GetComponent<AudioSource> ().volume = soundSize;
		}
	}
}
