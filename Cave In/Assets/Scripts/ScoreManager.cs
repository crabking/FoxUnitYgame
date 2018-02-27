using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    public Text scoreText;
 
    [SerializeField]
    public static int scoreCount;

    
	// Use this for initialization
	void Start () {
        scoreCount = 0;
	}
	
	//
	void Update () {

        scoreText.text = "Score: " + scoreCount;
    }

    // function that destroys and adds 1 to score once player collides with a "coin"
    void OnTriggerEnter2D(Collider2D other)
    {
       
        
        if (other.name == "Player")
        {
            ChangeScore(1);
            
           

            Destroy(gameObject);
            
        }
        

    }

    public void ChangeScore(int amount)
    {
        scoreCount += amount;
    }


}
