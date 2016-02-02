using UnityEngine;
using System.Collections;

public class CreepSpawner : MonoBehaviour {

	public static int creepBudget = 100;
	public float spawnDelay = 0.5f;
	float timer = 0;



	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (timer > spawnDelay) {

			if (creepBudget != 0) {
				--creepBudget;
				float rAngle = Random.value * 2 * Mathf.PI;
				Instantiate(Resources.Load ("Prefabs/Creep" ,typeof(GameObject)), new Vector3(transform.position.x + 2 * Mathf.Sin(rAngle), 2f, transform.position.z + 2 * Mathf.Cos(rAngle)), Quaternion.identity);
			}

			timer = 0;
		}

		timer += Time.deltaTime;
	}
}
