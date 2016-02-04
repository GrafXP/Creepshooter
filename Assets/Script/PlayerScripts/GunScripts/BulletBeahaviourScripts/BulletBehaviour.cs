using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {


    public float LifeTime = 0.5f;

    private float _lifeTime = 0;

    public float Damage = 20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        _lifeTime += Time.deltaTime;
        //if (_lifeTime> LifeTime)
        //{
        //    transform.gameObject.SetActive(false);
        //}


	}

    public void Shoot(float force, Quaternion orientation)
    {
        transform.rotation = orientation;
        transform.GetComponent<Rigidbody>().velocity = transform.forward * force ;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //print(collision.transform.tag);
        if (collision.transform.tag == "Enemy")
        {
			collision.transform.GetComponent<IDamageable>().ApplyDamage(Damage);
            
        }
        _lifeTime = LifeTime + 1;
    }

    public bool isDead()
    {
        
        return _lifeTime > LifeTime;
    }

    void OnDisable()
    {
        _lifeTime = 0;
    }
}
