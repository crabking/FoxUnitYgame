using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

    [SerializeField]
    private ParticleSystem moneyDrop;

    [SerializeField]
    private ScoreManager score;

    public bool damageFrames;

    IEnumerator Timer()
    {
        damageFrames = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);
        damageFrames = false;
    }

    void Start () {
	
	}
	
	void Update () {
	
	}

    public void TakenDamage()
    {
        moneyDrop.Stop();
        moneyDrop.Play();
        score.ChangeScore(-10);
        StartCoroutine(Timer());
    }
}
