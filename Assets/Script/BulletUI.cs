using UnityEngine;
using System.Collections;

public class BulletUI : MonoBehaviour {

	Global g;
	GUIText bulletText;
	
	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("GlobalObject");
		g = obj.GetComponent< Global >();
		bulletText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		bulletText.text = "Bullet : " + g.getBulletQuantity().ToString();
	}
}
