using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 [RequireComponent(typeof(Animator))]
public class TowerBehaviour : MonoBehaviour {

   

    private Animator _animator;
    public string TargetTag;
    private int _collisionCount = 0;
    private float _stayDeployedTimer = 4;
    private GameObject _lookatTarget;
    private List<GameObject> _closebyTargets;
    private Transform _gunPointer;
    Quaternion lastRotation = Quaternion.identity;
    Quaternion startRotation = Quaternion.identity;
    Vector3 lastKnownPosition;
    private Transform _gunBase;
   

    
    void Start () {
        _animator = transform.GetComponent<Animator>();
        _closebyTargets = new List<GameObject>();
        lastKnownPosition = Vector3.zero;


        _gunPointer = transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0);
        _gunBase = transform.GetChild(0).GetChild(1);
        print(_gunPointer.name);
        
       
	}
	
	// Update is called once per frame
	void Update () {

        //print(_closebyTargets.Count);

        if (_collisionCount != 0)
        {
            _stayDeployedTimer = 4;
        }
        else
        {
            if (_stayDeployedTimer>=0)
            {
                _stayDeployedTimer -= Time.deltaTime;
            }
            
        }
       
        if (_stayDeployedTimer <= 0)
        {
            _animator.SetBool("Deploing", false);
        }
	    
	}

    void LateUpdate()
    {
        
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Deployed"))
        {
            
            //if (startRotation == Quaternion.identity)
            //{
            //    //print(_gunPointer.transform.rotation);
            //    startRotation = _gunPointer.transform.rotation;
            //}
                
            

            
            if (_closebyTargets.Count> 0)
            {

                

                _gunBase.LookAt(new Vector3(lastKnownPosition.x, _gunBase.transform.position.y, lastKnownPosition.z));
                _gunBase.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

                var oldRotation = _gunPointer.transform.rotation;


                

                _gunPointer.LookAt(new Vector3(lastKnownPosition.x, lastKnownPosition.y, lastKnownPosition.z));
                
                _gunPointer.Rotate(90.0f+180.0f, 0.0f, 180.0f, Space.Self);

                lastKnownPosition = GetClosestTarget().position;
                lastRotation = _gunPointer.rotation;
            }
            else
            {


                //_gunBase.rotation = Quaternion.LookRotation(new Vector3(lastKnownPosition.x, _gunBase.transform.position.y, lastKnownPosition.z));
                _gunBase.LookAt(new Vector3(lastKnownPosition.x, _gunBase.transform.position.y, lastKnownPosition.z));
                _gunBase.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
                

            }
            
        }
        else
        {
            //_gunBase.rotation = Quaternion.LookRotation(new Vector3(lastKnownPosition.x, _gunBase.transform.position.y, lastKnownPosition.z));
            _gunBase.LookAt(new Vector3(lastKnownPosition.x, _gunBase.transform.position.y, lastKnownPosition.z));
           _gunBase.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
           
           

        }
        
    }


    private Transform GetClosestTarget(){
        Transform closestTarget = null;
        
        
            print("looking for closestTarget");
            
            float distance = float.MaxValue;
            foreach (var item in _closebyTargets)
            {
                var tempdist = item.transform.position - transform.position;
                if (tempdist.magnitude < distance)
                {
                    distance = tempdist.magnitude;
                    closestTarget = item.transform;
                }
            }

        
        return closestTarget;
        
     }
    

    void OnTriggerEnter(Collider collision)
    {
        
        if (collision.transform.tag == TargetTag)
        {
         //   print("add");
            if (!_closebyTargets.Contains(collision.gameObject))
            {
                
                _closebyTargets.Add(collision.gameObject);
                _collisionCount++;
                _animator.SetBool("Deploing", true);
            }
              
        }

    }


   

    void OnTriggerExit(Collider other)
    {
        
        if (other.transform.tag == TargetTag)
        {
            if (_closebyTargets.Contains(other.gameObject))
            {
                _closebyTargets.Remove(other.gameObject);

                _collisionCount--;
            }


            
        }
    }

}
