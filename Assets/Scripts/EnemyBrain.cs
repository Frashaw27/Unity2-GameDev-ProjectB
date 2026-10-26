using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class functions as all the innerworking of how enemies work
public class EnemyBrain : MonoBehaviour
{
    public int hp;
    public int damage = 1;
    //Determines how long they will move in their given path
    public int moveTimer = 0;
    //What the value is for them to reset to once their movetime has reached 0
    public int moveTimerReset;
    //Determines what behavior they'll do
    public int behavior;
    //Assists with the above behavior
    public int subBehavior;
    //Particle spawner when damaged
    public GameObject particle;
    //The delay before the particle producer vanishes again
    public int partDelay;

    //Resets the moveTimer to their base moveTime
    void Start(){
        moveTimer = moveTimerReset;
    }

    //Has most of the behavior
    void FixedUpdate()
    {
        //When reaches 0, it remove the particle producer for the on-hit effect
        if (partDelay > 0){
            partDelay--;
            if (partDelay == 0){
                particle.SetActive(false);
            }
        }
        //The do nothing behavior, mostly here for posterity
        if (behavior == 1){
            
        //The wanderer behavior
        } else if (behavior == 2){
            //While moveTime is above 0, keep going this one direction even if there is a wall
            if (moveTimer > 0){
                moveTimer--;
                //Move Right
                if (subBehavior == 1){
                    transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
                //Move Left
                } else if (subBehavior == 2){
                    transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
                //Move Up
                } else if (subBehavior == 3){
                    transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
                //Move Down
                } else if (subBehavior == 4){
                    transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
                }
            } else {
                //Resets the moveTimer with an extra amount of seconds for some differences
                moveTimer = moveTimerReset + Random.Range(0,31);
                //Randomly chooses a direction to go in
                subBehavior = Random.Range(1,5);
            }
        }
    }

    //Handles collisions
    void OnCollisionEnter2D(Collision2D collision){
        collisionLogic(collision);
    }

    //Handles the logic of collisions
    void collisionLogic(Collision2D collision){
        //Take damage when contacting the sword
        if (collision.gameObject.CompareTag("Damage")){
            takeDamage(1);
        }
    }

    void takeDamage(int damage){
        //Activates blood particles when hit
        particle.SetActive(true);
        hp -= damage;
        if (hp < 0) hp = 0;
        //When Hp reaches 0, destroy the enemy and call the RoomManager to do the effects related to that
        if (hp == 0){
            Destroy(gameObject);
            RoomManager.Instance.killEnemy();
        }
        partDelay = 10;
    }
}
