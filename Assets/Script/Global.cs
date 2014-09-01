using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	public GameObject objToSpawn;
	private float timer1;
	private float timer2;
	public float spawnPeriod;
	public int numberSpawnedEachPeriod;
	public Vector3 originInScreenCoords;
	private int score;
	public int life;
	public int totalAsteroid;
	public int currentTotalAsteroid;
	public int level;
	public bool shipDestroy;
	private int bulletQuantity;
	private int maxBulletQuantity;
	private float bulletRecoverPeriod;
	public GameObject gameObjShip;
	private float invinciblePeriod;

	// Use this for initialization
	void Start () {
		maxBulletQuantity = 20;
		bulletQuantity = 20;
		level = 1;
		currentTotalAsteroid = 0;
		totalAsteroid = 10;
		score = 0;
		timer1 = 0;
		timer2 = 0;
		life = 300;
		spawnPeriod = 2.0f;
		bulletRecoverPeriod = 2.0f;
		numberSpawnedEachPeriod = 3;

		originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		timer1 += Time.deltaTime;
		timer2 += Time.deltaTime;

		if (timer1 > spawnPeriod && currentTotalAsteroid < totalAsteroid) {
			timer1 = 0;
			float screenWidth = Camera.main.GetScreenWidth ();
			float screenHeight = Camera.main.GetScreenHeight ();


			for (int i = 0; i < numberSpawnedEachPeriod; i++) {

				//GameObject gameObjShip = GameObject.Find("Fighter");
				//Ship ship = gameObjShip.GetComponent<Ship> ();


				float horizontalPos = Random.Range (0.0f, screenWidth);
				float verticalPos = Random.Range (0.0f, screenHeight);
				Vector3 testVector = Camera.main.ScreenToWorldPoint (new Vector3 (horizontalPos, verticalPos, originInScreenCoords.z));
				GameObject objAsteroid = Instantiate (objToSpawn, 
				             Camera.main.ScreenToWorldPoint (new Vector3 (horizontalPos, verticalPos, originInScreenCoords.z)),

			            	 Quaternion.identity) as GameObject;
				Asteroid asteroid = objAsteroid.GetComponent<Asteroid>();
				asteroid.setSize(1.0f);
				asteroid.pointValue = 10;

				currentTotalAsteroid++;
			}
		}

		if (timer2 > bulletRecoverPeriod && bulletQuantity < maxBulletQuantity) {
			timer2 = 0;
			bulletQuantity++;
		}
		if (shipDestroy == true) {
			Instantiate (gameObjShip, new Vector3(0,0,0), Quaternion.AngleAxis (90, Vector3.up));
			shipDestroy = false;
			bulletQuantity = maxBulletQuantity;
		}
	}

	public void setLife(int liveCount){
		life = liveCount;
	}

	public int getLife(){
		return life;
	}

	public void setScore(int s){
		score = s;
	}

	public int getScore(){
		return score;
	}

	public void setBulletQuantity(int b){
		bulletQuantity = b;
	}

	public int getBulletQuantity() {
		return bulletQuantity;
	} 
}
