using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    CircleCollider2D ballColl;
    Rigidbody2D ballRb2d;
    public GameObject ballAtCollision;

    void Start(){
        ballRb2d = ball.GetComponent<Rigidbody2D>();
        ballColl = ball.GetComponent<CircleCollider2D>();
    }

    
    void Update(){
        bool drawBallAtCollision = false;
        Vector2 offsetHitPoint = new Vector2();

        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRb2d.position, ballColl.radius, ballRb2d.velocity.normalized);

        foreach (var circleCastHit in circleCastHit2DArray){
            if (circleCastHit.collider != null && circleCastHit.collider.GetComponent<BallControl>() == null){
                Vector2 hitPoint = circleCastHit.point;
                Vector2 hitNormal = circleCastHit.normal;
                offsetHitPoint = hitPoint + hitNormal * ballColl.radius;
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);
                if (circleCastHit.collider.GetComponent<SideWalls>() == null){
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOG).normalized;
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);
                    float outDot = Vector2.Dot(outVector, hitNormal);

                    if (outDot > -1.0f && outDot < 1.0){
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint, offsetHitPoint + outVector * 10.0f);
                        drawBallAtCollision = true;
                    }
                }
                break;
            }
            
        }

        if (drawBallAtCollision){
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        } else {
            ballAtCollision.SetActive(false);
        }

    }
}
