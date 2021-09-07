using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public float speed = 10f;
    public float boundY = 2.25f;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;

    private Rigidbody2D rb2d;
    private int score;
    private ContactPoint2D lastContactPoint;

    public ContactPoint2D LastContactPoint{
        get { return lastContactPoint; }
    }


    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update(){
        var vel = rb2d.velocity;
        if (Input.GetKey(moveUp)) {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown)) {
            vel.y = -speed;
        }
        else {
            vel.y = 0;
        }
        rb2d.velocity = vel;

        var pos = transform.position;
        if (pos.y > boundY){
            pos.y = boundY;
        } else if (pos.y < -boundY){
            pos.y = -boundY;
        }

        transform.position = pos;
    }

    public void IncrementScore(){
        score++;
    }
    public void ResetScore(){
        score = 0;
    }
    public int Score{
        get {return score;}
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name.Equals("Ball")){
            lastContactPoint = coll.GetContact(0);
            coll.collider.attachedRigidbody.velocity = Vector2.ClampMagnitude(coll.collider.attachedRigidbody.velocity, 10f);
        }
    }
}
