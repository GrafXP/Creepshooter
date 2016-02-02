using UnityEngine;
using System.Collections;

public class BaseBehaviour : MonoBehaviour {

    public float Health = 500;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Health<= 0)
        {
            Destroy(transform.gameObject);
        }
	}




    void OnCollisionEnter(Collision collision)
    {
       
    }


    public void ApplyDamage(float damageAmount)
    {


        if (damageAmount>0)
        {
            //print("Apply Damage");
            Health -= damageAmount;
        }
    }
}
