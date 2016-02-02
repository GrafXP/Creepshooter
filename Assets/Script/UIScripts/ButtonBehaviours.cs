using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ButtonBehaviours : MonoBehaviour {

    public GameObject PauseUi;


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menü")
        {
            if (Input.GetButtonDown("Cancel"))
            {
                //print("AddedPlayer");
                if (Settings.NrOfPlayers < 2)
                {
                    Settings.NrOfPlayers += 1;
                    GameManager.FindObjectOfType<Canvas>().GetComponentInChildren<panelScript>().updateCounter();
                }
                else
                {
                    Settings.NrOfPlayers -= 1;
                    GameManager.FindObjectOfType<Canvas>().GetComponentInChildren<panelScript>().updateCounter();
                }


            } 
        }
        //print(Settings.NrOfPlayers);
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }


    public void ReturnToMenü()
    {
        SceneManager.UnloadScene("Level1");
        SceneManager.LoadScene("Menü");
        

    }

    public void ResumeGame()
    {

        FindObjectOfType<GameManager>().UnPause();
        
    }
}
