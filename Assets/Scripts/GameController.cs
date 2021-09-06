using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player1;
    private Rigidbody2D p1r2bd;

    public PlayerController player2;
    private Rigidbody2D p2r2bd;

    public BallControl ball;
    private Rigidbody2D ballR2bd;
    private CircleCollider2D ballColl;

    public Trajectory trajectory;
    public int maxScore;

    private bool isDebugging = false;

    void Start(){
        p1r2bd = player1.GetComponent<Rigidbody2D>();
        p2r2bd = player2.GetComponent<Rigidbody2D>();
        ballR2bd = ball.GetComponent<Rigidbody2D>();
        ballColl = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI(){
        GUI.Label(new Rect(Screen.width/2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width/2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART")){
            player1.ResetScore();
            player2.ResetScore();
            ball.SendMessage("ResetGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if (player1.Score == maxScore){
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height/2 -10, 2000, 1000), "Player One Wins");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        } else if (player2.Score == maxScore){
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height/2 -10, 2000, 1000), "Player Two Wins");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        if (GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53), "Toggle\nDebug Info")){
            isDebugging = !isDebugging;
            trajectory.enabled = !trajectory.enabled;
        }

        if (isDebugging){                  
            float ballMass = ballR2bd.mass;
            float ballSpeed = ballR2bd.velocity.magnitude;
            float ballFriction = ballColl.friction;
            Vector2 ballVel = ballR2bd.velocity;
            Vector2 ballMomentum = ballMass * ballVel;

            Color oldColor = GUI.backgroundColor;      
            GUI.backgroundColor = Color.red;

            float impulseP1X = player1.LastContactPoint.normalImpulse;
            float impulseP1Y = player1.LastContactPoint.tangentImpulse;
            float impulseP2X = player2.LastContactPoint.normalImpulse;
            float impulseP2Y = player2.LastContactPoint.tangentImpulse;

            string debugText = 
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVel + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impluse from P1 = (" + impulseP1X + ", " + impulseP1Y +")\n" +
                "Last impluse from P2 = (" + impulseP2X + ", " + impulseP2Y +")\n";
            
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
            GUI.backgroundColor = oldColor;
        }
    }
}
