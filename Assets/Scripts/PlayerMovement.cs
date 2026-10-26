using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//Called PlayerMovement but in reality controls all of the player based functions
public class PlayerMovement : MonoBehaviour
{
    //Controls the speed that the player goes
    public float xSpeed = 1f;
    public float ySpeed = 1f;
    //The Hp value until the player dies
    public int hp = 5;
    //The GameObject of the Sword so it can be spawned when using the attack button
    public GameObject sword;
    //A Bool that sees if the player is using the Sword, if so then they can't move
    public bool acting = false;
    //The UI Texts during main gameplay
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI clearRoomText;
    //Particle from being hit
    public GameObject particle;
    //Delay between Particle appearing and the object that spawn the particles disappearing
    public int partDelay;

    //Updates the texts and checks if attacking
    void Update(){
        hpText.text = "Hp: " + hp + "/5";
        //Gets the rooms cleared from the RoomManager and throws it into the text
        //Checks to see if the number is below a certain amount to maintain relative visual balance
        clearRoomText.text = "Rooms Cleared: " + ((RoomManager.Instance.roomsCleared < 100) ? " " : "") + ((RoomManager.Instance.roomsCleared < 10) ? " " : "") + RoomManager.Instance.roomsCleared;
        //Attack
        if (Input.GetButtonDown("Sword")){
            sword.SetActive(true);
            acting = true;
        }
    }

    //Controls the Movement
    void FixedUpdate(){
        //Counts down so that the particle producer can vanish and not continue after the inital spurt
        if (partDelay > 0){
            partDelay--;
            if (partDelay == 0){
                particle.SetActive(false);
            }
        }
        //Stops movement inputs if attacking
        if (acting) return;
        //Gets the horizontal movement.
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        //Sets the player's direct position so there is no end lag/slipping when the player stops holding the input key.
        transform.position = new Vector2(transform.position.x + horz * xSpeed, transform.position.y + vert * ySpeed);
        //Sets the direction the Sword will appear out of depending on your last movement in a given direction
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

    //Handles the logic for collisions
    void collisionLogic(Collision2D collision){
        //Makes the player take damage based on the enemy's damage stat on collision
        if (collision.gameObject.CompareTag("Enemy")){
            takeDamage(collision.gameObject.GetComponent<EnemyBrain>().damage);
        }
    }

    //Has the player take damage
    void takeDamage(int damage){
        //Activates the hurt particles
        particle.SetActive(true);
        hp -= damage;
        //Makes sure there isn't overkill
        if (hp < 0) hp = 0;
        //If dead, go to gameover
        if (hp == 0) SceneManager.LoadScene(2);
        //If not dead, this is how long the particles should stay
        partDelay = 10;
    }

    //Calls the RoomManager to spawn a new room when touching a room trigger
    private void OnTriggerEnter2D(Collider2D col){
        RoomManager.Instance.transferRoomLogic(gameObject, col);
    }
}
