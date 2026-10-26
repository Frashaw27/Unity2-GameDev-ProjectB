using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the manager for all the Proc. Generation
public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }
    //Marker of the player's progress
    public int roomsCleared = 0;
    //A int about how many enemies left need to be killed before the doors are removed
    public int enemiesRemain;
    public GameObject room1;
    //The main camera so that it can transfer between the different rooms
    public GameObject camera;
    //The current room so that we can get their coordinates and also destroy it after spawning a new one
    public GameObject currentRoom;
    //The doors that block the player from going to the next room before all the enemies are dead
    public GameObject doors;
    //The transfers that allow you to go to a different room
    public GameObject transfers;
    //The player
    public GameObject player;
    //A list of rooms to pull from
    public GameObject[] roomList;
    //The amount of enemies from each of the above rooms
    public int[] enemyList;
    
    //Used instead of Start as this starts faster
    void Awake()
    {
        //Checks to see if there is another instance of GameManager, if there is then destroy it so only one remains.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        // Persist this GameObject across all scenes
        DontDestroyOnLoad(gameObject);
    }

    //Makes sure the starting room is the curerent room to start with
    void Start(){
        currentRoom = room1;
    }

    //When the room is cleared, open all doors, increase rooms cleared, and give the player one hp while not exceeding 5
    public void endRoom(){
        doors.SetActive(false);
        if (player.GetComponent<PlayerMovement>().hp < 5) player.GetComponent<PlayerMovement>().hp += 1;
        roomsCleared++;
    }

    //Removes 1 off the remaining enemies. If 0, end the room
    public void killEnemy(){
        enemiesRemain--;
        if (enemiesRemain == 0){
            endRoom();
        }
    }

    //The big portion for this class
    public void transferRoomLogic(GameObject player, Collider2D trigger){
        //Gets a random variable from 0 to 5 to pull a level from so that the # of enemies and the level are consistent
        var rand = Random.Range(0, 5);
        enemiesRemain = enemyList[rand];
        //A list of variables to be used when moving the stuff
        var playerX = 0f;
        var playerY = 0f;
        var roomX = 0f;
        var roomY = 0f;
        //When entering the left door, change the player and other things by this much on the X
        if (trigger.gameObject.CompareTag("RoomTransferLeft")){
            playerX = -5f;
            roomX = -35.5f;
        } else if (trigger.gameObject.CompareTag("RoomTransferRight")){
            playerX = 5f;
            roomX = 35.5f;
        } else if (trigger.gameObject.CompareTag("RoomTransferUp")){
            playerY = 5f;
            roomY = 20f;
        } else if (trigger.gameObject.CompareTag("RoomTransferDown")){
            //For whatever reason, this doesn't work and whenever I tested going down the player would not be in the room and instead the abyss
            playerY = 5f;
            roomY = -20f;
        }
        //Credit for Instantiate Code: https://discussions.unity.com/t/how-to-spawn-an-prefab/776669/2
        GameObject transferRoom = Instantiate(roomList[rand], new Vector2 (currentRoom.transform.position.x + roomX, currentRoom.transform.position.y + roomY), Quaternion.identity);
        Destroy(currentRoom);
        currentRoom = transferRoom;
        //Moves over all of the related objects
        camera.transform.position = new Vector3(camera.transform.position.x + roomX, camera.transform.position.y + roomY, -10f);
        player.transform.position = new Vector3(player.transform.position.x + playerX, player.transform.position.y + playerY, 0f);
        doors.transform.position = new Vector3(doors.transform.position.x + roomX, doors.transform.position.y + roomY, 0f);
        transfers.transform.position = new Vector3(transfers.transform.position.x + roomX, transfers.transform.position.y + roomY, 0f);
        //Reactivates all the doors
        doors.SetActive(true);
    }
}
