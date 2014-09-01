using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour {

	Global global;
	GUIText scoreText;

	// Use this for initialization
	void Start () {
		GameObject gameObjGlobal = GameObject.Find ("GlobalObject");
		global = gameObjGlobal.GetComponent< Global >();
		scoreText = gameObject.GetComponent<GUIText>();



	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score : " + global.getScore().ToString();

	}
}
