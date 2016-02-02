using UnityEngine;
using System.Collections;

public static class Settings {


    private static int _nrOfPlayers = 1;
	public static int  NrOfPlayers
    {
        get
        {
            return _nrOfPlayers;
    
        }
        set 
        {
            _nrOfPlayers = value;
        }
    }
    

}
