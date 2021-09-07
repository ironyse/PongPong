using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float xForce = 10f;
    public float yForce = 5f;

    private Vector2 trajectoryOG;

    public Vector2 TrajectoryOG{
        get { return trajectoryOG; }
    }

    void Start(){
        trajectoryOG = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        Invoke(nameof(PushBall), 2);
    }

    void ResetGame()
    {
        ResetBall();
        Invoke(nameof(PushBall), 2);
    }

    void PushBall(){
        float yRand = Random.Range(-yForce, yForce);
        float rand = Random.Range(0,2);
        if (rand<1.0f){
            rb2d.velocity = new Vector2(xForce, yRand);
        } else {
            rb2d.velocity = new Vector2(-xForce, yRand);
        }
    }

    void ResetBall(){
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }    

    void OnCollisionExit2D(Collision2D coll) {
        trajectoryOG = transform.position;
    }
}
