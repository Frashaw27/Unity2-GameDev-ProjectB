using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

//The main component for determining what the buttons do. Taken from Project A directly as there is no need to remake it from scratch.
public class ButtonPress : MonoBehaviour
{
    //Determines what the button does.
    public int buttonLogic;
    //Determines what the scene will go to when using this option.
    public int sceneTransfer;
    //Sets itself as a button as I couldn't figure out a option to do it without.
    public Button button;
    //The group of where the button lies for their various functions.
    public GameObject rootGroup;
    //The group that will be activated by pressing the button.
    public GameObject differentGroup;

    void Start(){
        //Attaches a listener to check when the button is clicked.
		button.GetComponent<Button>().onClick.AddListener(startOnClick);
    }

    //Button Logic Executer.
    public void startOnClick(){
        //Changing the current scene
        if (buttonLogic == 1){
            SceneManager.LoadScene(sceneTransfer);
            //Sets to deactive as the lingering ui would linger on the screne.
            if (rootGroup != null) rootGroup.SetActive(false);
        //Showing different UI on Titlescreen
        } else if (buttonLogic == 2){
            if (rootGroup != null) rootGroup.SetActive(false);
            if (differentGroup != null) differentGroup.SetActive(true);
        //Exits the game
        } else if (buttonLogic == -1){
            Application.Quit();//
        }
	}
}
