using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public float speed;
	public GameObject enemyExplosion;
	public GameObject obstructionExplosion;

	void Start()
	{
		rigidbody.velocity = transform.forward * speed;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			Instantiate (enemyExplosion, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
		
		if (other.tag == "Obstruction") 
		{
			Instantiate (obstructionExplosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
		
		//		gameController.AddScore (scoreValue);
		
	}

}
