using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {


    public int PlayerNr = 0;
    public float WalkVelocity = 10f;
    public Camera PlayerCamera;
    public float BoostForce = 30f;
    


    private Rigidbody _rigidbody;
    private bool _onGround;
    public float Jumpforce = 20;
    public float BoostDuration = 2;

    private bool _canBoost= true;
   
   // public float Gravity = -9.81f;
	// Use this for initialization
	void Start () {
        _rigidbody = transform.GetComponent<Rigidbody>();
        transform.position = new Vector3(0.0f, 1, 0.0f);
        
	}
	
	// Update is called once per frame
	void Update () {
       
     
	
	}
    void FixedUpdate()
    {


        var posy = Input.GetAxis("Vertical"+PlayerNr);



        var posx = Input.GetAxis("Horizontal" + PlayerNr);

        _rigidbody.AddForce(transform.TransformDirection(posx, 0, posy) * WalkVelocity);


        //var foreward = transform.forward;
        if (Input.GetButton("Boost" + PlayerNr)&&_onGround && BoostDuration>0&&_canBoost)
        {
            _rigidbody.AddForce(transform.forward.normalized * BoostForce);
            BoostDuration -= 0.5f;
            if (BoostDuration<= 0)
            {
                _canBoost = false;
            }
        }
        else if(BoostDuration< 20)
        {
            
            BoostDuration += 0.1f;
            if (BoostDuration> 20)
            {
                BoostDuration = 20;
                _canBoost = true;
            }
        }

        


        if (Input.GetButton("Fire" + PlayerNr) && _onGround)
        {
            //print("Fire" + PlayerNr);
            
            _rigidbody.AddForce(Vector3.up * Jumpforce);
            //print("jump");
        }
        

       
      
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, PlayerCamera.transform.rotation.eulerAngles.y, 0.0f));


       // transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        


       // _rigidbody.velocity = transform.TransformDirection(new Vector3(posx, 0.0f, posy)) * WalkVelocity;
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Floor")
        {
            _onGround = false;
            //print("Air");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            _onGround = true;
        }
        
        //print("Ground");
    }


    void OnGUI()
    {
        GUI.Label(new Rect(0.0f, 0.0f, 20f, 20f), BoostDuration.ToString() + " " + _rigidbody.velocity.magnitude);
        GUI.Label(new Rect(0.0f, 50.0f, 100.0f, 100.0f),  _rigidbody.velocity.magnitude.ToString());
    }
}
