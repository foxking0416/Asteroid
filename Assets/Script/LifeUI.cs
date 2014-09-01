using UnityEngine;
using System.Collections;

public class LifeUI : MonoBehaviour {

	Global g;
	GUIText lifeText;

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("GlobalObject");
		g = obj.GetComponent< Global >();
		lifeText = gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		lifeText.text = "Life : " + g.life.ToString();
	}
}
