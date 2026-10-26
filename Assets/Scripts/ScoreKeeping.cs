using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class serves to both initalize the scores to their base values and also accept new scores and place them within the rankings
public class ScoreKeeping : MonoBehaviour
{
    public int rooms = 0;
    // Start is called before the first frame update
    void Start(){
        //If this value doesn't exist, sets all all the values
        if (!PlayerPrefs.HasKey("FirstPlace")){
            PlayerPrefs.SetInt("FirstPlace", 10);
            PlayerPrefs.SetInt("SecondPlace", 5);
            PlayerPrefs.SetInt("ThirdPlace", 3);
            PlayerPrefs.SetInt("FourthPlace", 0);
            PlayerPrefs.SetInt("FifthPlace", 0);
        }
        UpdateScore();
    }

    // Update is called once per frame
    void UpdateScore()
    {
        //If the room manager exists, get the Rooms cleared from that, otherwise keep it at 0
        if (RoomManager.Instance != null) rooms = RoomManager.Instance.roomsCleared;
        name = "";
        if (rooms > PlayerPrefs.GetInt("FirstPlace")){
            //If the number of rooms beats this place then it starts at the bottom and grabs the next rank up until
            //You have reached the desired point where you put in the new number of rooms
            PlayerPrefs.SetInt("FifthPlace", PlayerPrefs.GetInt("FourthPlace"));
            PlayerPrefs.SetInt("FourthPlace", PlayerPrefs.GetInt("ThirdPlace"));
            PlayerPrefs.SetInt("ThirdPlace", PlayerPrefs.GetInt("SecondPlace"));
            PlayerPrefs.SetInt("SecondPlace", PlayerPrefs.GetInt("FirstPlace"));
            PlayerPrefs.SetInt("FirstPlace", rooms);
        } else if (rooms > PlayerPrefs.GetInt("SecondPlace")){
            PlayerPrefs.SetInt("FifthPlace", PlayerPrefs.GetInt("FourthPlace"));
            PlayerPrefs.SetInt("FourthPlace", PlayerPrefs.GetInt("ThirdPlace"));
            PlayerPrefs.SetInt("ThirdPlace", PlayerPrefs.GetInt("SecondPlace"));
            PlayerPrefs.SetInt("SecondPlace", rooms);
        } else if (rooms > PlayerPrefs.GetInt("ThirdPlace")){
            PlayerPrefs.SetInt("FifthPlace", PlayerPrefs.GetInt("FourthPlace"));
            PlayerPrefs.SetInt("FourthPlace", PlayerPrefs.GetInt("ThirdPlace"));
            PlayerPrefs.SetInt("ThirdPlace", rooms);
        } else if (rooms > PlayerPrefs.GetInt("ForthPlace")){
            PlayerPrefs.SetInt("FifthPlace", PlayerPrefs.GetInt("FourthPlace"));
            PlayerPrefs.SetInt("FourthPlace", rooms);
        } else if (rooms > PlayerPrefs.GetInt("FifthPlace")){
            PlayerPrefs.SetInt("FifthPlace", rooms);
        }
        if (RoomManager.Instance != null) RoomManager.Instance.roomsCleared = 0;
    }
}
