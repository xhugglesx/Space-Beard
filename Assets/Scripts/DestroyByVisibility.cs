using UnityEngine;
using System.Collections;

public class DestroyByVisibility : MonoBehaviour {

	public bool hasBeenVisible;

	void Start()
	{
		hasBeenVisible = false;
	}

	void OnBecameVisible () 
	{
		hasBeenVisible = true;
	}

	void OnBecameInvisible()
	{
		if (hasBeenVisible)
			Destroy (gameObject);
	}
}
