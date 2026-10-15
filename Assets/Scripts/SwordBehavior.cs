using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    public int frameDuration = 0;
    public int frameMax = 60;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        frameDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameDuration++;
        if (frameDuration == frameMax){
            frameDuration = 0;
            gameObject.SetActive(false);
            player.gameObject.GetComponent<PlayerMovement>().acting = false;
        }
    }
}
