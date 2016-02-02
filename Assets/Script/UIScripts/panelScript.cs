using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class panelScript : MonoBehaviour {

    private int _nrOfPlayers;
    private bool _update = false;
	// Use this for initialization
	void Start () {

        _nrOfPlayers = Settings.NrOfPlayers;
        GetComponent<Text>().text += _nrOfPlayers;
	
	}
	
	// Update is called once per frame
	void Update () {
        _nrOfPlayers = Settings.NrOfPlayers;
        if (_update)
        {
            
            var text = GetComponent<Text>().text;
            text = text.Remove(text.Length - 1);
            text += _nrOfPlayers;
            GetComponent<Text>().text = text;

            _update = false;
        }
	}

    public void updateCounter()
    {
        
        _nrOfPlayers = Settings.NrOfPlayers;
        _update = true;
    }
}
