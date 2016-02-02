using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootBehaviour : MonoBehaviour {

    public GameObject Bullet;

    List<GameObject> bulletList;
    public int MagazineSize = 20;
    public float BulletVelocity=100;
    public float fireRate= 2;
    public GameObject SpawnObject;

    private List<GameObject> ShotBullets;
    private float _delay;
    private Vector3 startPosition;


    private int _playerNR;
	// Use this for initialization
	void Start () {

        _playerNR = transform.parent.parent.GetComponent<PlayerController>().PlayerNr;

        startPosition = SpawnObject.transform.position; 
        ShotBullets = new List<GameObject>();
        _delay = fireRate;
        bulletList = new List<GameObject>();
        fillBullets();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetAxis("PFire"+_playerNR)<0)
        {
            
            _delay += Time.deltaTime;
          
           
            if (_delay>fireRate)
            {
                //print(_delay);

                var shot = getBullet().GetComponent<BulletBehaviour>();
                shot.transform.parent = null;
                shot.Shoot(BulletVelocity, transform.rotation);
               
                _delay = 0;
            }
            
        }
        if (Input.GetAxis("PFire"+_playerNR)==0)
        {
            _delay = fireRate;
        }


        backToPool();
	}



    void fillBullets()
    {
        for (int i = 0; i < MagazineSize; i++)
        {
            var bullet = (GameObject)Instantiate(Bullet,startPosition,transform.rotation);
            bullet.transform.parent = transform;
            bullet.SetActive(false);
            bulletList.Add(bullet);
        }
    }

    GameObject getBullet()
    {
        if (bulletList.Count> 0)
        {
            //print("yes");
            var Obj = bulletList[0];
            //print(Obj == null);
            bulletList.RemoveAt(0);
            //print(bulletList.Count);
            Obj.SetActive(true);
            //print(Obj.activeSelf);
            ShotBullets.Add(Obj);
            return Obj;
        }
        return null;
    }


    public void backToPool()
    {
        //print(ShotBullets.Count);
        if (ShotBullets.Count>0)
        {
            var bullet = ShotBullets[0];
            //print(bullet.GetComponent<BulletBehaviour>().isDead());
            if (bullet.GetComponent<BulletBehaviour>().isDead())
            {
               
                bullet.transform.position = SpawnObject.transform.position;
                bullet.transform.rotation = SpawnObject.transform.rotation;
                bullet.transform.parent = transform;
                ShotBullets.RemoveAt(0);
                
                bullet.SetActive(false);
                bulletList.Add(bullet);
            } 
        }
      
    }
}
