using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    [SerializeField]
    private GameController gameController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.name == "Ball"){
            player.IncrementScore();
            if (player.Score < gameController.maxScore){
                coll.gameObject.SendMessage("ResetGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
