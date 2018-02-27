using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	public Vector3 Speed = new Vector3(0,0.01f,0);
	
	void Start () {
	
	}
	

	void Update () {
		this.transform.Translate(Speed * Mathf.Sin(Time.time)); 
	}
}
