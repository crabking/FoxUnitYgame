using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject dangerWall;

    [SerializeField]
    private int shakeDistance;

    private Vector3 targetPosition;
    private bool locked;

    private float shake;
    private float shakeX;
    private float shakeY;

    public bool inCheckpoint;

    public Vector3 checkpointCamera;

    public float xs;
    private float ys;
    private float zs;
    private float magnitudes;
    private float normXs;
    private float normYs;
    private float normZs;

    private float velocityOverTime;

    void Start () {
        locked = true;
	}

    void FixedUpdate () {
        velocityOverTime += Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x);
        if(velocityOverTime > 15 * 100)
        {
            velocityOverTime = 15 * 100;
        }
        velocityOverTime -= 5;
        if (velocityOverTime < 0)
        {
            velocityOverTime = 0;
        }




        shake = player.transform.position.x - dangerWall.transform.position.x;
        if (shake < shakeDistance && shake > -10)
        {
            shake = (-shake + shakeDistance) / (shakeDistance * 4);
            shakeX = (Random.value * 2 - 1) * Time.timeScale;
            shakeY = (Random.value * 2 - 1) * Time.timeScale;
        }
        else
        {
            shake = 0;
            shakeX = 0;
            shakeY = 0;
        }
        if (!inCheckpoint && locked)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x + (shake * shakeX), player.transform.position.y + (shake * shakeY), -12 - (shake * shakeDistance) - (velocityOverTime / 150));
        }
        else if (!inCheckpoint)
        {
            targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, -12 - (shake * shakeDistance) - (velocityOverTime / 150));

            xs = targetPosition.x - gameObject.transform.position.x;
            ys = targetPosition.y - gameObject.transform.position.y;
            zs = targetPosition.z - gameObject.transform.position.z;
            magnitudes = Mathf.Pow((xs * xs) + (ys * ys) + (zs * zs), 1f / 3f);
            normXs = xs / magnitudes;
            normYs = ys / magnitudes;
            normZs = zs / magnitudes;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 25 * (normXs * Time.deltaTime) + (shake * shakeX), gameObject.transform.position.y + 25 * (normYs * Time.deltaTime) + (shake * shakeY), gameObject.transform.position.z + 25 * (normZs * Time.deltaTime));
            if (Mathf.Abs(xs) < 0.5 && Mathf.Abs(ys) < 0.5)
            {
                locked = true;
            }
        }
        else
        {
            locked = false;
            targetPosition = checkpointCamera;

            xs = targetPosition.x - gameObject.transform.position.x;
            ys = targetPosition.y - gameObject.transform.position.y;
            zs = targetPosition.z - gameObject.transform.position.z;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 5 * (xs * Time.deltaTime) + (shake * shakeX), gameObject.transform.position.y + 5 * (ys * Time.deltaTime) + (shake * shakeY), gameObject.transform.position.z + 5 * (zs * Time.deltaTime));
        }
    }
}
