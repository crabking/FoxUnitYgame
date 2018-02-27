using UnityEngine;
using System.Collections;

public class Thorns : MonoBehaviour {

    //[SerializeField]
    //private Movement movement;

    void Start() {

        //movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        
    }


    void OnTriggerEnter2D(Collider2D col) {

        // checks if player collides
        //if (col.CompareTag("Player"))
        //{
        //    // does 1 dmg to the player
        //    movement.Damage(1);
        //    Debug.Log("takes damgae");
        //}
    }

}
