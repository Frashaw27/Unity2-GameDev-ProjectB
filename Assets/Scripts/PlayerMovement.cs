using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xSpeed = 1f;
    public float ySpeed = 1f;
    public int hp = 5;
    public GameObject sword;
    public GameObject camera;
    public bool acting = false;
    // Start is called before the first frame update
    void Start()
    {
        //https://discussions.unity.com/t/how-to-spawn-an-prefab/776669/2
        //Instantiate(sword, new Vector2 (0f,0f), Quaternion.identity);
        //camera.transform.position = new Vector2(10f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Sword")){
            sword.SetActive(true);
            acting = true;
        }
    }

    void FixedUpdate(){
        if (acting) return;
        //Gets the horizontal movement.
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        //Sets the player's direct position so there is no end lag/slipping when the player stops holding the input key.
        transform.position = new Vector2(transform.position.x + horz * xSpeed, transform.position.y + vert * ySpeed);
        //
        if (horz > 0){
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        } else if (horz < 0){
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        } else if (vert > 0){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        } else if (vert < 0){
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    //Calls the logic for collisions when they happen.
    void OnCollisionEnter2D(Collision2D collision){
        collisionLogic(collision);
    }

    void collisionLogic(Collision2D collision){
        //Debug.Log("coll");
        if (collision.gameObject.CompareTag("Enemy")){
            takeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        RoomManager.Instance.transferRoomLogic(gameObject, col);
    }

    void takeDamage(int damage){
        hp -= damage;
        if (hp < 0) hp = 0;
    }
}
