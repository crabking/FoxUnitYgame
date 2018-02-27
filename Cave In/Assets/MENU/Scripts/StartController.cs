using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class StartController : MonoBehaviour {

	public GameObject start;
	public GameObject set;
	AudioSource audioS;
	void Start () {
		if (start) {
			start.SetActive (true);
			start.transform.localScale = new Vector3 (0.1f, 0.1f, 1);
			start.transform.DOScale (new Vector3 (1, 1, 1), 0.5f);
		}
		audioS = GetComponent<AudioSource> ();
	}
	

	void Update () {

	}
	public void PlaySource(){
		audioS.Play ();
	}
	public void OpenSet(){
		start.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 0.01f);
		Invoke ("OpenOne", 0.01f);
	}
	void OpenOne(){
		start.SetActive (false);
		set.SetActive (true);
		set.transform.localScale = new Vector3 (0.1f, 0.1f, 1);
		set.transform.DOScale (new Vector3 (1, 1, 1), 0.5f);
	}

	public void OpenStart(){
		set.transform.DOScale (new Vector3 (0.1f, 0.1f, 0.1f), 0.01f);
		Invoke ("OpenTwo", 0.01f);
	}
	void OpenTwo(){
		set.SetActive (false);
		start.SetActive (true);
		start.transform.DOScale (new Vector3 (1, 1, 1), 0.5f);
	}

	public void GoOn(){
		SceneManager.LoadScene ("Tutorial Level");
	}
	public void StartGame(){
		SceneManager.LoadScene ("Tutorial Level");//在双引号里把游戏场景的名字一字不差的输入进去，然后保存就可以了
	}
	public void QuitGame(){
		Application.Quit ();
	}
}
