using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the class that determines how the sword will be used.
public class SwordBehavior : MonoBehaviour
{
    //This variable counts down until the player can move again.
    public int frameDuration = 0;
    //This is the variable that controls how long the sword will be visible for.
    public int frameMax = 60;
    //The player
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //Makes sure that if it starts active, the frame duration is set to 0
        frameDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Counts the amount of frames that have gone by.
        frameDuration++;
        if (frameDuration == frameMax){
            frameDuration = 0;
            //Sets the sword to inactive and allow the player to move
            gameObject.SetActive(false);
            player.gameObject.GetComponent<PlayerMovement>().acting = false;
        }
    }
}
