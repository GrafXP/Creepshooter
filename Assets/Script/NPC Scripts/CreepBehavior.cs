using UnityEngine;
using System.Collections;

public class CreepBehavior : MonoBehaviour, IDamageable {

	public float health;

	#region IDamageable implementation
	public void ApplyDamage (float damageAmount)
	{
		health -= damageAmount;
	}
	#endregion

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {

		if (health < 0) {
			Destroy (transform.gameObject);
			CreepSpawner.creepBudget++;
		}

		GameObject bs = GameObject.Find ("Base");
		GetComponent<NavMeshAgent> ().SetDestination (bs.transform.position);
	}
}
