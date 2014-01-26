using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float rotationsPerMinute = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = 0f;
		float y = (float)(6.0 * rotationsPerMinute * Time.deltaTime);
		float z = 0f;
		transform.Rotate(x,y,z);
	}
}
