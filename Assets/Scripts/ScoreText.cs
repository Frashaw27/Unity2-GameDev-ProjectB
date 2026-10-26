using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Purely exists just to set the text on the scoring screen with the actual scores
public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Records:\n1st - " + PlayerPrefs.GetInt("FirstPlace") + " Rooms\n\n2nd - " + PlayerPrefs.GetInt("SecondPlace") + " Rooms\n\n3rd - " + PlayerPrefs.GetInt("ThirdPlace") + " Rooms\n\n4th - " + PlayerPrefs.GetInt("FourthPlace") + " Rounds\n\n5th - " + PlayerPrefs.GetInt("FifthPlace") + " Rounds";
    }
}