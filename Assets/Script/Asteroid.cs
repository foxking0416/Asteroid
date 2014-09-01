using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public GameObject deathExplosion;
	public GameObject smallerAsteroid;
	public int pointValue;
	public AudioClip deathKnell;
	private float size;
	private Vector3 originInScreenCoords;
	
	// Use this for initialization
	void Start () {

		float velocity = Random.Range (0.0f, 50.0f);
		float eulerAngleY = Random.Range (0.0f, 360.0f);

		Quaternion rot = Quaternion.Euler(new Vector3(0, eulerAngleY, 0));

		Vector3 thrust = new Vector3();
		thrust.x = velocity;

		gameObject.rigidbody.drag = 0;
		gameObject.rigidbody.MoveRotation (rot);
		gameObject.rigidbody.AddRelativeForce (thrust);

		gameObject.transform.localScale = new Vector3(size, size, size);
		originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {

		float screenWidth = Camera.main.GetScreenWidth ();
		float screenHeight = Camera.main.GetScreenHeight ();
		Vector3 boundaryStart = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, originInScreenCoords.z));
		Vector3 boundaryEnd = Camera.main.ScreenToWorldPoint (new Vector3 (screenWidth, screenHeight, originInScreenCoords.z));
		
		if (gameObject.transform.position.x < boundaryStart.x) {
			gameObject.transform.position = new Vector3( boundaryEnd.x, gameObject.transform.position.y, gameObject.transform.position.z);
		}
		if (gameObject.transform.position.x > boundaryEnd.x) {
			gameObject.transform.position = new Vector3( boundaryStart.x, gameObject.transform.position.y, gameObject.transform.position.z);
		}
		if (gameObject.transform.position.z < boundaryStart.z) {
			gameObject.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, boundaryEnd.z);
		}
		if (gameObject.transform.position.z > boundaryEnd.z) {
			gameObject.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, boundaryStart.z);
		}
		
	}

	public void Die() {
		AudioSource.PlayClipAtPoint (deathKnell, gameObject.transform.position);

		Vector3 oldPosition = gameObject.transform.position;
		Vector3 newPosition1 = oldPosition + new Vector3 (1, 0, 0);
		Vector3 newPosition2 = oldPosition + new Vector3 (0, 0, 1);
		/*
		if (size > 0.5) {
			GameObject objSmallerAsteroid1 = Instantiate (smallerAsteroid, oldPosition, Quaternion.AngleAxis (-90, Vector3.right)) as GameObject;
			GameObject objSmallerAsteroid2 = Instantiate (smallerAsteroid, newPosition1, Quaternion.AngleAxis (-90, Vector3.right)) as GameObject;
			GameObject objSmallerAsteroid3 = Instantiate (smallerAsteroid, newPosition2, Quaternion.AngleAxis (-90, Vector3.right)) as GameObject;

			Asteroid sAsteroid1 = objSmallerAsteroid1.GetComponent<Asteroid> ();
			Asteroid sAsteroid2 = objSmallerAsteroid2.GetComponent<Asteroid> ();
			Asteroid sAsteroid3 = objSmallerAsteroid3.GetComponent<Asteroid> ();
			sAsteroid1.setSize(size * 0.333f);
			sAsteroid2.setSize(size * 0.333f);
			sAsteroid3.setSize(size * 0.333f);
			sAsteroid1.pointValue = 30;
			sAsteroid2.pointValue = 30;
			sAsteroid3.pointValue = 30;
		}*/
		
		Instantiate (deathExplosion, gameObject.transform.position, Quaternion.AngleAxis (-90, Vector3.right));

		GameObject gameObjGlobal = GameObject.Find("GlobalObject");
		Global global = gameObjGlobal.GetComponent<Global> ();
		global.setScore(global.getScore() + pointValue);

		Destroy (gameObject);
	}

	public float getSize(){
		return size;
	}

	public void setSize(float s){
		size = s;
	}

}
