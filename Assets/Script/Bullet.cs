using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 thrust;
	public Quaternion heading;

	// Use this for initialization
	void Start () {
		thrust.x = 400.0f;

		gameObject.rigidbody.drag = 0;
		gameObject.rigidbody.MoveRotation (heading);
		gameObject.rigidbody.AddRelativeForce (thrust);
	}

	void OnCollisionEnter(Collision collision){
		Collider collider = collision.collider;

		if (collider.CompareTag ("Asteroids")) {
			Asteroid asteroid = collider.gameObject.GetComponent<Asteroid> ();

			asteroid.Die ();
			Destroy (gameObject);
		} else {
			Debug.Log ("Collided with " + collider.tag);
		}
	}
}
