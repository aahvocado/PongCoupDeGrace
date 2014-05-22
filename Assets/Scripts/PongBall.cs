using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PongBall : MonoBehaviour {
	public bool paddlePositionMatters;//does the position the ball reflects off the paddle matter? if false, will just reflect x
	//private PlayerController pc;
	private List<BallEffects> effectsList = new List<BallEffects>();
	private GameObject fireEffect;
	
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
		fireEffect = GameObject.Find("OnFire");
		reset ();
	}
	void reset(){
		effectsList = new List<BallEffects>();
		currSpeed = defaultSpeed;
		currSize = defaultSize;
		velocity = randomInitialVelocity();
	}
	
	//public functions
	//add an effect onto the ball
	public void addEffect(BallEffects e){
		if(hasEffectName(e.getName())){
			getEffectName(e.getName()).refresh();
		}else{
			effectsList.Add(e);
		}
	}

	//returns a vector3 with random speed based on speed
	Vector3 randomInitialVelocity(){
		float x = (int)Random.value == 1 ? -currSpeed : currSpeed;
		float y = (int)Random.value == 1 ? -currSpeed*.3f : currSpeed*.3f;
		//test velocity
		x = -currSpeed;
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
		
		PlayerController pc = p.transform.parent.gameObject.GetComponent<PlayerController>();
		PongSkill skill = pc.getCurrentSkill();
		//affected by skills?
		if(pc.isAction()){
			if(skill.getName() == "forward smash"){
				velocity = new Vector3(velocity.x*skill.getMovementModifier().x, velocity.y*skill.getMovementModifier().y, velocity.z*skill.getMovementModifier().z);
			}
		}
		
		//calculate bounce factor
		float lengthThird = .3f;
		float velocityVerticalBoost = 1.2f;
		float velocityVerticalShrink = .7f;
		float velocityHorizontalBoost = 1.4f;
		//change reflection depending on where it hit on the paddle
		if(paddlePositionMatters){
			velocity.y = Mathf.Asin(pos.y - paddlePos.y) * 7;
			if(Mathf.Abs(velocity.y) > 20){
				velocity.y *= velocityVerticalShrink;
				}
		}
			
		//velocity.x *= velocityHorizontalBoost;
		reflectX ();
	}
	
	// Update is called once per frame
	void Update () {
		//
		Vector3 pos = this.transform.position;
		this.transform.Translate(velocity*Time.deltaTime);//move ball		
		
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
		//checks
		checkEffects();//cooldown effects etc
		checkVelocity();
	}
	//check if ball is ignited
	bool checkOnFire(){
		ParticleSystem fire = fireEffect.GetComponent<ParticleSystem>();
		if(getEffectName("ignited").isActive()){
			if(!fire.isPlaying){
				fire.Play();
			}
			return true;
		}else{
			fire.Stop();
		}
		return false;
	}
	//check all effects and subtract timer
	void checkEffects(){
		foreach(BallEffects be in effectsList){
			be.updateEffect();		
			if(!be.isActive()){
				//remove the effect, how to do this without messing up the effect list :(
			}
		}
		checkOnFire();

	}
	public BallEffects getEffectName(string n){
		foreach(BallEffects be in effectsList){
			if(be.getName() == n){
				return be;
			}
		}
		return new BallEffects("null");
	}
	public bool hasEffectName(string n){
		foreach(BallEffects be in effectsList){
			if(be.getName() == n){
				return true;
			}
		}
		return false;
	}
	//make sure velocity isn't too fast or too slow
	void checkVelocity(){
		float xmin = 3.0f;
		float xmax = 16.0f;
		float ymin = 3.0f;
		float ymax = 16.0f;
		int neg;//save value if negative
		if(Mathf.Abs(velocity.x) < xmin){
			neg = (int)Mathf.Ceil(1/velocity.x);
			velocity = new Vector3(xmin*neg, velocity.y, velocity.z);
		}
		if(Mathf.Abs(velocity.x) > xmax){
			neg = (int)Mathf.Ceil(1/velocity.x);
			velocity = new Vector3(xmax*neg, velocity.y, velocity.z);
		}
		if(Mathf.Abs(velocity.y) < ymin){
			neg = (int)Mathf.Ceil(1/velocity.x);
			velocity = new Vector3(velocity.x, ymin*neg, velocity.z);
		}
		if(Mathf.Abs(velocity.y) > ymax){
			neg = (int)Mathf.Ceil(1/velocity.x);
			velocity = new Vector3(velocity.x, ymax*neg, velocity.z);
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
	public List<BallEffects> getEffects(){
		return effectsList;
	}
	public float getSpeed(){
		return currSpeed;
	}
	public Vector3 getVelocity(){
		return velocity;
	}
}
