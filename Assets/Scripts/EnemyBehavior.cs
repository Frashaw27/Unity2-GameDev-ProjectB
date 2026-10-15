using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int hp = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        collisionLogic(collision);
    }

    void collisionLogic(Collision2D collision){
        //Debug.Log("coll");
        if (collision.gameObject.CompareTag("Damage")){
            takeDamage(1);
        }
    }

    void takeDamage(int damage){
        hp -= damage;
        if (hp < 0) hp = 0;
    }
}
