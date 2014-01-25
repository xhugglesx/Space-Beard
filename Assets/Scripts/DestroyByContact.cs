using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject enemyExplosion;
	public GameObject obstructionExplosion;
	public int scoreValue;
	//private GameController gameController;

	void Start()
	{
//		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
//		if (gameControllerObject != null) 
//		{
//			gameController = gameControllerObject.GetComponent <GameController>();
//		}
//		if (gameController == null) 
//		{
//			Debug.Log("Cannot find 'GameController' object.");
//		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			Instantiate (enemyExplosion, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
		}
		
		if (other.tag == "Obstruction") 
		{
			Instantiate (obstructionExplosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
		}

//		gameController.AddScore (scoreValue);

		Destroy (gameObject);
	}

}
