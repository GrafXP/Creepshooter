using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int MaxPlayerCount;

    public GameObject PlayerCanvas;
    public GameObject Player;
    public Camera Camera;


    public GameObject PauseMenü;
    private bool _isPaused;

    private int _playerCount = 0;

    //list of all prefabs for this Scene;

	// Use this for initialization


    void Awake()
    {

        MaxPlayerCount = Settings.NrOfPlayers;
          

        for (int i = 0; i < MaxPlayerCount; i++)
        {
            var player = (GameObject)Instantiate(Player);
            _playerCount++;
            if (_playerCount ==1)
            {
                player.transform.position = new Vector3(-2.0f, 0.87f, 0.0f);
            }
            else
            {
                player.transform.position = new Vector3(2.0f, 0.87f, 0.0f);
            }
            
            var camera = Instantiate(Camera);
            var canvas = Instantiate(PlayerCanvas);
            player.transform.GetComponent<PlayerController>().PlayerCamera = camera;
            camera.transform.GetComponent<CameraControllScript>().TargetLookat = player.transform.FindChild("LookAtTarget");

            if (MaxPlayerCount == 2)
            {
                switch (_playerCount)
                {
                    case 1:

                        camera.rect = new Rect(0.0f, 0.5f, 1, 1);

                        break;
                    case 2:
                        camera.rect = new Rect(0.0f, 0.0f, 1, 0.5f);
                        break;
                   
                } 
            }
            

            canvas.GetComponent<Canvas>().worldCamera = camera;

            foreach (Transform tr in player.transform)
            {
                if (tr.tag == "Gun")
                {
                    tr.GetComponent<GunBehaviour>().camera = camera;
                }
            }

            canvas.GetComponent<Canvas>().planeDistance = 0.5f;
            player.GetComponent<PlayerController>().PlayerNr = i;
            camera.GetComponent<CameraControllScript>().CameraNr = i;
            //player.GetComponentInChildren<GunBehaviour>().camera = camera;




        }
        UnPause();
    }
    
    
    
    void Start () {

        UnPause();

        
	    //instantiate and positioning of all Prefabs in the scene.
        Cursor.visible = false;
        PauseMenü.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	//keep Track of Players Enemys and Stats.
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Cancel1"))
        {

            if (_isPaused)
            {
                UnPause();
            }
            else
            {
                _isPaused = true;
                Cursor.visible = true;
                Time.timeScale = 0.0f;
                PauseMenü.SetActive(true);
            }
            
            
        }
        
	}


    public void UnPause()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        PauseMenü.SetActive(false);
        Cursor.visible = false;
    }
}
