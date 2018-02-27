using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SetController : MonoBehaviour {

	public GameObject setting;
	public GameObject teaching;
	public GameObject[] pause;

	public void OpenTeaching(){
		teaching.SetActive (true);
	}
	public void Reopen(){
//		Time.timeScale = 1;
		SceneManager.LoadScene ("Tutorial Level");
	}
	public void Retrun(){
		SceneManager.LoadScene ("01_start");
	}
	void Start () {

	}


	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (setting.activeSelf) {
				setting.SetActive (false);
				teaching.SetActive (false);
				Time.timeScale = 1;
				returnObject ();
			} else {
				setting.SetActive (true);
				teaching.SetActive (false);
				Time.timeScale = 0;
				pauseObject ();
			}
		}
	}
	void pauseObject(){
		foreach (GameObject g in pause) {
			g.SetActive (false);
		}
	}
	void returnObject(){
		foreach (GameObject g in pause) {
			g.SetActive (true);
		}
	}
}
