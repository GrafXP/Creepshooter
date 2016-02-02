using UnityEngine;
using System.Collections;

public class GunBehaviour : MonoBehaviour {

    public Camera camera;


    

	// Use this for initialization
	void Start () {
        var ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if (Physics.Raycast(ray))
        {
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }

        
	
	}
	
	// Update is called once per frame
	void LateUpdate() {
        var ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray))
        {
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }
        
	}
}
