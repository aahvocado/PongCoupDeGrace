using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour {
	public float defaultSpeed;//movement speed
	private float currSpeed;
	
	private Vector3 velocity;

	public Vector3 defaultSize;//paddle size
	private Vector3 currSize;
	
	public Vector3 spawnPosition;//origin spawn position
	public Vector3 lowerBounds;//lower bounds
	public Vector3 upperBounds;//upper bounds
	
	// Use this for initialization
	void Start () {
		this.transform.position = spawnPosition;
		reset ();
	}
	void reset(){
		currSpeed = defaultSpeed;
		currSize = defaultSize;
		velocity = randomInitialVelocity();
	}
	
	//returns a vector3 with random speed based on speed
	Vector3 randomInitialVelocity(){
		float x = (int)Random.value == 0 ? -currSpeed:currSpeed;
		float y = (int)Random.value == 0 ? -currSpeed*.3f:currSpeed*.3f;
		//test velocity
		x = currSpeed;
		y = currSpeed;
		float z = Random.Range(0,0);
		return new Vector3(x, y, z);
	}
	//check collisions
	void OnCollisionEnter(Collision collision) {
		if(collision.transform.tag == "Paddle"){
			//Paddle paddleScript = collider.GetComponent<Paddle>();
			//print (collider.GetComponent<Collider>().bounds.size);
        	paddleHit(collision.gameObject);
		}
	}
	//ball hit the paddle
	void paddleHit(GameObject p){
		Vector3 pos = this.transform.position;
		Vector3 paddlePos = p.transform.position;
		//calculate bounce factor
		float lengthThird = .3f;
		float velocityVerticalBoost = 1.5f;
		float velocityVerticalShrink = .7f;
		float velocityHorizontalBoost = 1.4f;

		//change reflection depending on where it hit on the paddle
		velocity.y = Mathf.Asin(pos.y - paddlePos.y) * 7;
		if(Mathf.Abs(velocity.y) > 20){
			velocity.y *= velocityVerticalShrink;
			}
			
		reflectX ();
	}
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		this.transform.Translate(velocity*Time.deltaTime);//move ball
		
		//check paddle hit
		
		//check left right bounds
		if(pos.x < lowerBounds.x){
			this.transform.position = new Vector3(lowerBounds.x, pos.y, pos.z);
			reflectX();
		}else if(pos.x > upperBounds.x){
			this.transform.position = new Vector3(upperBounds.x, pos.y, pos.z);
			reflectX();
		}
		//check up down bounds
		if(pos.y < lowerBounds.y){
			this.transform.position = new Vector3(pos.x, lowerBounds.y, pos.z);
			reflectY();
		}else if(pos.y > upperBounds.y){
			this.transform.position = new Vector3(pos.x, upperBounds.y, pos.z);
			reflectY();
		}
	}
	
	//changes in velocity
	void reflectX(){
		velocity = new Vector3(velocity.x*-1, velocity.y, velocity.z);
	}
	void reflectY(){
		velocity = new Vector3(velocity.x, velocity.y*-1, velocity.z);
	}

	//getters
	public float getSpeed(){
		return currSpeed;
	}
	public Vector3 getVelocity(){
		return velocity;
	}
}
