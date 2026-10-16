using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }
    public int roomsCleared = 0;
    public GameObject room1;
    public GameObject room2;
    public GameObject camera;
    public GameObject currentRoom;


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

    // Start is called before the first frame update
    void Start()
    {
        currentRoom = room1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transferRoomLogic(GameObject player, Collider2D trigger){
        GameObject transferRoom = Instantiate(room2, new Vector2 (currentRoom.transform.position.x -35.5f,0f), Quaternion.identity);
        Destroy(currentRoom);
        currentRoom = transferRoom;
        camera.transform.position = new Vector3(camera.transform.position.x - 35.5f, 0f, -10f);
    }
}
