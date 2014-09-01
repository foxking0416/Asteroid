using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	private GameObject gameObjGlobal;
	private Global global;
	public Vector3 forceVector;
	public float rotationSpeed;

	public GameObject Bullet;
	public GameObject gameObjShip;
	private float initialRotationY;
	private Vector3 originInScreenCoords;

	// Use this for initialization
	void Start () {
		//rotation = gameObject.transform.rotation.y;
		gameObjGlobal = GameObject.Find ("GlobalObject");
		global = gameObjGlobal.GetComponent< Global >();

		initialRotationY = gameObject.transform.rotation.eulerAngles.y;
		forceVector.z = 1.0f;
		rotationSpeed = 2.0f;	


	 	originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
	}



	void FixedUpdate(){
		if (Input.GetAxisRaw ("Vertical") > 0) {
			gameObject.rigidbody.AddRelativeForce(forceVector);		
		}
		else if (Input.GetAxisRaw ("Vertical") < 0) {
			gameObject.rigidbody.AddRelativeForce(-1.0f * forceVector);		
		}
		
		if (Input.GetAxisRaw ("Horizontal") > 0) {

			initialRotationY += rotationSpeed;
			Quaternion rot = Quaternion.Euler(new Vector3(0, initialRotationY, 0));
			gameObject.rigidbody.MoveRotation(rot);
		}
		else if (Input.GetAxisRaw ("Horizontal") < 0) {

			initialRotationY -= rotationSpeed;
			Quaternion rot = Quaternion.Euler(new Vector3(0, initialRotationY, 0));
			gameObject.rigidbody.MoveRotation(rot);
		}	

	}


	// Update is called once per frame
	void Update () {


		
		if (Input.GetButtonDown ("Fire1")) {

			int bulletQuantity = global.getBulletQuantity();
			if(bulletQuantity > 0){

				Vector3 spawnPos = gameObject.transform.position;
				spawnPos.x += 1.5f * Mathf.Sin(initialRotationY * Mathf.PI/180);
				spawnPos.z += 1.5f * Mathf.Cos(initialRotationY * Mathf.PI/180);

				GameObject obj = Instantiate(Bullet, spawnPos, Quaternion.identity) as GameObject;
				Bullet b = obj.GetComponent<Bullet>();

				Quaternion rot = Quaternion.Euler(new Vector3(0, initialRotationY - 90, 0));
				b.heading = rot;
				global.setBulletQuantity(--bulletQuantity);
			}
		}



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


	void OnCollisionEnter(Collision collision){
		Collider collider = collision.collider;
		
		if (collider.CompareTag ("Asteroids")) {
			Asteroid asteroid = collider.gameObject.GetComponent<Asteroid> ();
			
			//GameObject gameObjGlobal = GameObject.Find ("GlobalObject");
			//global = gameObjGlobal.GetComponent< Global >();
			global.life--;
			asteroid.Die ();
			Destroy (gameObject);

			global.shipDestroy = true;


		} else {
			Debug.Log ("Collided with " + collider.tag);
		}
	}
}
