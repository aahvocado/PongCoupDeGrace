using UnityEngine;
using System.Collections;

public class FireblastScript : MonoBehaviour {
	public float speed;
	private Vector3 velocity;
	private bool isFlipped;
	
	private bool collided;
	private int life;
	private int maxLife;
	// Use this for initialization
	void Start () {
		velocity = new Vector3(speed, 0, 0);
		isFlipped = false;
		collided = false;
		life = 0;
		maxLife = 150;
	}
	
	// Update is called once per frame
	void Update () {
		velocity = new Vector3(speed, 0, 0);
		print (velocity);

		this.transform.Translate(velocity*Time.deltaTime);

		checkBounds();
		life++;
		if(life > maxLife){
			Destroy (transform.root.gameObject);
		}
	}
	//check collisions
	void OnCollisionEnter(Collision collision) {
		if(life > 30 && !collided){//ignore collisions when initially spawned
			if(collision.transform.tag == "Paddle"){
				collided = true;
	        	GameObject player = collision.gameObject.transform.parent.gameObject;
				PlayerController pc = player.GetComponent<PlayerController>();
				pc.takeDamage(2);
			}
		}
	}
	//destroy ball if out of bounds	
	void checkBounds(){
		float maxBoundsX = 40;
		if(this.transform.position.x < -maxBoundsX || this.transform.position.x > maxBoundsX){
			Destroy(transform.root.gameObject);
		}
	}
	//
	public void setSpeed(float s){
		speed = s;
	}
	public void flip(){
		speed = -speed;
	}
	//
	public float getSpeed(){
		return speed;
	}
}
