using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GUIText debugText;
	public float BoundaryBuffer;
	public float CameraBorder;

	public GameObject axeSwing;
	public Transform axeSpawn;
	public float swingRate = 0.5F;
	
	private float nextSwing = 0.0F;
	// This will be the value to track forward movement and prevent the user from backtracking
	private float yStop = 0.0f;
	// Last yStop value that spawned an enemy
	private float lastYSpawn;

	public GameObject enemy;
	public GameObject obstruction;
	public float enemyFrequency;

	// Modular Environmental Blocks
	public GameObject Convex_Down_Left;
	public GameObject Convex_Down_Right;
	public GameObject Convex_Up_Left;
	public GameObject Convex_Up_Right;
	public GameObject Concave_Down_Left;
	public GameObject Concave_Down_Right;
	public GameObject Concave_Up_Left;
	public GameObject Concave_Up_Right;
	public GameObject Wall_Normal_Down;
	public GameObject Wall_Normal_Left;
	public GameObject Wall_Normal_Right;
	public GameObject Wall_Normal_Up;

	// Procedurally generated level related variables
	private int blockRowCount = 80;
	private int[][] blocks = new int[80][];
	private int currentBlockIndex = 0;

	[System.Serializable]
	public class Boundary
	{
		public float xMin, xMax, zMin, zMax;
	}

	void Start()
	{
		SpawnEnemy ();

//		GenerateLevel ();
		RenderLevel ();

		//Camera.main.orthographicSize = 50f;
	}

//	This would have been great, but not goint to happen
//	void GenerateLevel ()
//	{
//		// 0 = Wall
//		// 1 = Space
//		// 2 = Column
//		// 3 = Enemy
//		// Our first row is a wall, so all true
//		AddRow (0, 0, 0);
//		AddRow (1, 1, 1);
//		AddRow (1, 1, 0);
//		AddRow (1, 1, 0);
//		AddRow (1, 0, 0);
//		AddRow (1, 1, 1);
//		AddRow (0, 0, 1);
//		AddRow (0, 1, 1);
//	}

	void RenderLevel()
	{
//		int piece;
//		int up;
//		int left;
//		int right;
//		int down;
//		for (int ii = 0; ii < 8; ii++)
//		{
//			// Default to walls
//			piece = 0;
//			up = 0;
//			left = 0;
//			right = 0;
//			down = 0;
//
//
//		}
	
		float xBase = -24.05f;
		float yBase = -2.05f;
		float xIncrement = 5.905f;
		float yIncrement = 5.905f;
	
		//HARD CODE EVERYTHING

//		float y = yBase;
//		BuildRow (xBase, xIncrement, y, Concave_Down_Left, null, Wall_Normal_Down, Wall_Normal_Down, Wall_Normal_Down, Wall_Normal_Down, Wall_Normal_Down, null, Concave_Down_Right);
//		y += yIncrement;
//		y += yIncrement;
//		BuildRow (xBase, xIncrement, y, Wall_Normal_Left, null, null, null, null, null, null, null, Wall_Normal_Right);
//		y += yIncrement;
//		BuildRow (xBase, xIncrement, y, Wall_Normal_Left, null, null, null, null, null, null, null, Wall_Normal_Right);
//		y += yIncrement;
//		BuildRow (xBase, xIncrement, y, Wall_Normal_Left, null, null, null, null, null, null, null, Concave_Up_Right);
//		y += yIncrement;
//		BuildRow (xBase, xIncrement, y, Wall_Normal_Left, null, null, null, null, null, null, null, null);

	}

	//HARD CODE EVERYTHING
	void BuildRow(float xBase, float xIncrement, float y, GameObject go1, GameObject go2, GameObject go3, GameObject go4, GameObject go5, GameObject go6, GameObject go7, GameObject go8, GameObject go9)
	{
		float x = xBase;
		InstantiateBlock (go1, x, y);

		x += xIncrement;
		InstantiateBlock (go2, x, y);

		x += xIncrement;
		InstantiateBlock (go3, x, y);

		x += xIncrement;
		InstantiateBlock (go4, x, y);
		
		x += xIncrement;
		InstantiateBlock (go5, x, y);
		
		x += xIncrement;
		InstantiateBlock (go6, x, y);
		
		x += xIncrement;
		InstantiateBlock (go7, x, y);
		
		x += xIncrement;
		InstantiateBlock (go8, x, y);
		
		x += xIncrement;
		InstantiateBlock (go9, x, y);
	}

	void InstantiateBlock(GameObject go, float x, float y)
	{
		if (go == null)
			return;
		Vector3 spawnPosition = new Vector3 (x, go.transform.position.y, y);
		Instantiate (go, spawnPosition, go.transform.rotation);
	}

	// Look at all the surrounding pieces to determine the correct piece to use
	// Eff this. Way harder than I thought
	GameObject DetermineEnvironmentalPiece(int center, int up, int left, int right, int down)
	{
		GameObject go = null;

		if (center == 0) 
		{
			//this is a wall
		} 
		else 
		{

		}

		return go;
	}

	void AddRow(int left, int middle, int right)
	{
		blocks[currentBlockIndex] = new int[] { 0, left, middle, right, 0 };
		currentBlockIndex++;
	}


	void Update() {
		bool swingPress = Input.GetButton ("Fire1") || Input.GetKey (KeyCode.Space);
		if (swingPress && Time.time > nextSwing) {
			nextSwing = Time.time + swingRate;
			Instantiate(axeSwing, axeSpawn.position, axeSpawn.rotation);

			AudioSource[] audioSources = GetComponents<AudioSource>();
			audioSources[1].Play();
		}
	}

	void FixedUpdate()
	{
		// Movement code. Arrow keys or WASD work
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		// Rotate code based on mouse position

		// This gets the mouses position on the screen, not in the game space
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

		// Translate mouse position to the one we care about
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// using z coordinate for y value, because our camera is looking on from the Y axis
		mousePos = new Vector3(ray.origin.x, ray.origin.z, 0.0f);

		// Get Player coordinates
		// using z coordinate for y value, because our camera is looking on from the Y axis
		Vector3 playerPos = new Vector3 (rigidbody.position.x, rigidbody.position.z, 0.0f);

		//Get relative coordinates
		mousePos.x = mousePos.x - playerPos.x;
		mousePos.y = mousePos.y - playerPos.y;

		// Find our angle
		float yRotate = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
		rigidbody.rotation = Quaternion.Euler (0.0f, yRotate, 0.0f);

//		debugText.text = "Mouse(" + mousePos.x + ", " + mousePos.y + ", " + mousePos.z + ")\n";
//		debugText.text += "Player(" + playerPos.x + ", " + playerPos.y + ", " + playerPos.z + ")\n";
//		debugText.text += "yRotate = " + yRotate + "\n";

		// Set level boundaries, so that player can't go outside the map. Doing this dynamically
		//	so we can just resize the quad and not have to redo code.
		// Find our 'Level' (Quad object which has the dimensions we want)
		GameObject levelObject = GameObject.FindWithTag ("Level");
		if (levelObject != null) 
		{

			Boundary playerBoundary = new Boundary();

			//TODO - This logic only works because the quad is centered on origin (0,0)
			playerBoundary.xMin = levelObject.transform.lossyScale.x/2*-1 + BoundaryBuffer;
			playerBoundary.xMax = levelObject.transform.lossyScale.x/2 - BoundaryBuffer;
			//Player can't move back to areas south beyond the camera.
			playerBoundary.zMin = yStop - Camera.main.orthographicSize + BoundaryBuffer;//levelObject.transform.lossyScale.y/2*-1 + BoundaryBuffer + levelObject.transform.position.z;
			playerBoundary.zMax = levelObject.transform.lossyScale.y/2 - BoundaryBuffer + levelObject.transform.position.z;
//			playerBoundary.zMin = levelObject.transform.lossyScale.y/2*-1 + BoundaryBuffer;
//			playerBoundary.zMax = levelObject.transform.lossyScale.y/2 - BoundaryBuffer;
//
			debugText.text = "Player(" + playerPos.x + ", " + playerPos.y + ", " + playerPos.z + ")\n";
			debugText.text += "Level Size(" + levelObject.transform.lossyScale.x + ", " + levelObject.transform.lossyScale.y + ", " + levelObject.transform.lossyScale.z + ")\n";
			debugText.text += "Player Boundary X(" + playerBoundary.xMin + ", " + playerBoundary.xMax + ")\n";
			debugText.text += "Player Boundary Y(" + playerBoundary.zMin + ", " + playerBoundary.zMax + ")\n";

			rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, playerBoundary.xMin, playerBoundary.xMax), 
					0.0f, 
					Mathf.Clamp(rigidbody.position.z, playerBoundary.zMin, playerBoundary.zMax));

			// Move camera to follow player
			Vector3 newCameraPos = new Vector3 (playerPos.x, Camera.main.transform.position.y, playerPos.y);
			debugText.text += "Camera(" + Camera.main.transform.position.x + ", " + Camera.main.transform.position.y + ", " + Camera.main.transform.position.z + ")\n";
			
			// We want the camera to move, but not near the boundaries of the level (or other obstructions).
			debugText.text += "Screen(" + Screen.width + ", " + Screen.height + ")\n";
			float aspectRatio = (float)Screen.width/(float)Screen.height;
			debugText.text += "Aspect Ratio(" + aspectRatio + ")\n";
			debugText.text += "Camera Size(" + Camera.main.orthographicSize + ")\n";

			Boundary cameraBoundary = new Boundary();
			//			cameraBoundary.xMin = ((levelObject.transform.lossyScale.x/2*-1) + (Camera.main.orthographicSize/2*aspectRatio)) * CameraBorder/aspectRatio;
			//			cameraBoundary.xMax = ((levelObject.transform.lossyScale.x/2) - (Camera.main.orthographicSize/2*aspectRatio)) * CameraBorder/aspectRatio;
			//			cameraBoundary.zMin = ((levelObject.transform.lossyScale.y/2*-1) - (Camera.main.orthographicSize/2)) * CameraBorder;
			//			cameraBoundary.zMax = ((levelObject.transform.lossyScale.y/2) + (Camera.main.orthographicSize/2)) * CameraBorder;
			
			cameraBoundary.xMin = -9f; //((levelObject.transform.lossyScale.x/2*-1) + (Camera.main.orthographicSize/2*aspectRatio)) * CameraBorder/aspectRatio;
			cameraBoundary.xMax = 8.5f; //((levelObject.transform.lossyScale.x/2) - (Camera.main.orthographicSize/2*aspectRatio)) * CameraBorder/aspectRatio;

			// Calmera no longer moves backwards. Player can move back a bit, but they can't go south beyond camera.
			cameraBoundary.zMin = yStop;//((levelObject.transform.lossyScale.y/2*-1) - (Camera.main.orthographicSize/2)) * CameraBorder;
			cameraBoundary.zMax = 1000;//((levelObject.transform.lossyScale.y/2) + (Camera.main.orthographicSize/2)) * CameraBorder;
			
//			cameraBoundary.xMin = -1;
//			cameraBoundary.xMax = 1;
//			cameraBoundary.zMin = -6;
//			cameraBoundary.zMax = 6;

			debugText.text += "Camera Boundary X(" + cameraBoundary.xMin + ", " + cameraBoundary.xMax + ")\n";
			debugText.text += "Camera Boundary Y(" + cameraBoundary.zMin + ", " + cameraBoundary.zMax + ")\n";
			
			Camera.main.transform.position = new Vector3(Mathf.Clamp(newCameraPos.x, cameraBoundary.xMin, cameraBoundary.xMax), 
			    Camera.main.transform.position.y, 
			    Mathf.Clamp(newCameraPos.z, cameraBoundary.zMin, cameraBoundary.zMax));
			//Camera.main.transform.position = newCameraPos;

			float currentYPos = Camera.main.transform.position.z;
			if (currentYPos > yStop)
			{
				yStop = Camera.main.transform.position.z;
			}
			debugText.text += "yStop(" + yStop + ")\n";
			

			debugText.text += "Velocity" + rigidbody.velocity + "\n";

			// Periodically spawn new enemies
			if ((lastYSpawn + enemyFrequency) < yStop)
				SpawnEnemy();
		}
		else
		{
			Debug.LogError("Cannot find 'Level' object.");
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Obstruction") 
		{
			// Beep if there is no clip currently playing.
			if(!audio.isPlaying)
			{
				audio.Play();
			}
		}
	}

	void SpawnEnemy()
	{
		GameObject levelObject = GameObject.FindWithTag ("Level");
		float xMin = levelObject.transform.lossyScale.x/2*-1 + BoundaryBuffer;
		float xMax = levelObject.transform.lossyScale.x/2 - BoundaryBuffer;

		float z = Camera.main.transform.position.z + (Camera.main.orthographicSize / 2) + 12;
		Vector3 spawnPosition = new Vector3 (Random.Range (xMin, xMax), 0, z);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (enemy, spawnPosition, spawnRotation);

		// Remember where we last spawned an enemy, so that we can calculate when to spawn more
		lastYSpawn = z;
	}
	
}
