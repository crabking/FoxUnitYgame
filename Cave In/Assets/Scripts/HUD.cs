using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    // array for sprite
    [SerializeField]
    public Sprite[] LifeSprites;
    [SerializeField]
    public Image LifeUI;
    
    public Movement movement;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        LifeUI.sprite = LifeSprites[movement.currentHealth];

	}
}
