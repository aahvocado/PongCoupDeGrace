using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PongBall : MonoBehaviour {
	public bool freezeBall;//freeze movement for debugging

	public GameObject p1;
	private PlayerController p1script;
	public GameObject p2;
	private PlayerController p2script;
	//
	public bool paddlePositionMatters;//does the position the ball reflects off the paddle matter? if false, will just reflect x
	public bool damageOnCollision;
	//private PlayerController pc;
	private List<BallEffects> effectsList = new List<BallEffects>();
	private GameObject fireEffect;
	
	public float defaultSpeed;//movement speed
	private float currSpeed;
	
	public Vector3 velocity;

	public Vector3 defaultSize;//paddle size
	private Vector3 currSize;
	
	public Vector3 spawnPosition;//origin spawn position
	public Vector3 lowerBounds;//lower bounds
	public Vector3 upperBounds;//upper bounds
	
	public float speedContactMultiplier = 1.05f;
	
	private float lightningStraightSpeed = 10.2f;//speed for when lightning strike is used
	// Use this for initialization
	void Start () {
		fireEffect = GameObject.Find("OnFire");
		reset ();
		//
		p1script = p1.GetComponent<PlayerController>();
		p2script = p2.GetComponent<PlayerController>();
	}
	
	public void reset(){
		this.transform.position = spawnPosition;
		effectsList = new List<BallEffects>();
		currSpeed = defaultSpeed;
		currSize = defaultSize;
		velocity = randomInitialVelocity();
	}
	
	//public functions
	//add an effect onto the ball
	public void addEffect(string n){
		if(hasEffectName(n)){
			getEffectName(n).refresh();
		}else{
			effectsList.Add(new BallEffects(n));
		}
	}

	//returns a vector3 with random speed based on speed
	Vector3 randomInitialVelocity(){
		float spawnx = Random.Range(-1,1);
		float spawny = Random.Range(-1,1);
		float x = spawnx < 0.0 ? -currSpeed : currSpeed;
		float y = spawny < 0.0 ? -(currSpeed*.7f) : currSpeed*.7f;
		//print ("random "+ testx + " / " + testy);
		
		//force test velocity
		//x = -currSpeed;
		//y = currSpeed;
		float z = Random.Range(0,0);
		return new Vector3(x, y, z);
	}
	//check collisions
	void OnCollisionEnter(Collision collision) {
		if(collision.transform.tag == "Paddle"){
			//Paddle paddleScript = collider.GetComponent<Paddle>();
			//print (collider.GetComponent<Collider>().bounds.size);
        	paddleHit(collision.gameObject);
		}else if(collision.transform.tag == "Wall"){
			wallHit(collision.gameObject);
		}
	}
	//ball hit the paddle
	void paddleHit(GameObject p){
		Vector3 pos = this.transform.position;
		Vector3 paddlePos = p.transform.position;
		
		PlayerController pc = p.transform.parent.gameObject.GetComponent<PlayerController>();
		PongSkill skill = pc.getCurrentSkill();
		//ball affected by paddle skills?
		paddleEffectsCheck(pc);
		//paddle affected by ball effects?
		ballEffectsCheck(pc);
		//damage player on collision
		if(damageOnCollision) damageToPlayer(pc, 1);
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
			
		//increase velocity
		changeVelocity(speedContactMultiplier);
		//flip the ball
		reflectX ();
	}
	void wallHit(GameObject p){
		Vector3 pos = this.transform.position;
		Vector3 paddlePos = p.transform.position;
		//check wall effects
		if(p.name == "Firewall(Clone)"){
			addEffect("ignited");
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
		reflectX();
	}
	//skills affecting this ball
	void paddleEffectsCheck(PlayerController pc){
		
		//check player's current skills
		PongSkill skill = pc.getCurrentSkill();
		string skillName = skill.getName();
		if(pc.isAction()){
			if(skillName == "forward smash"){
				velocity = new Vector3(velocity.x*skill.getMovementModifier().x, velocity.y*skill.getMovementModifier().y, velocity.z*skill.getMovementModifier().z);
			}
			if(skillName == "lightning strike"){
				addEffect("lightning straight");
			}
		}
	}
	//effects that happen 
	void ballEffectsCheck(PlayerController pc){
		//we hit something take off lightning strike
		if(hasEffectName("lightning straight")){
			if(getEffectName("lightning straight").isCheckable()){
				effectsList.Remove(getEffectName("lightning straight"));
			}
		}
		//ignited, hurt player
		if(hasEffectName("ignited")){
			pc.takeDamage(1);//normal 1 bonus damage from an ignited ball
			pc.checkForBurningDamage();
		}
	}
	//deal damage to paddle
	float damageToPlayer(PlayerController pc, float damage){
		pc.checkForBurningDamage();
		pc.takeDamage(damage);
		return damage;
	}
	
	// Update is called once per frame
	void Update () {
		//
		Vector3 pos = this.transform.position;
		if(hasEffectName("lightning straight")){//check for lightning strike
			float lspeed = lightningStraightSpeed*getHorizontalDirection()+currSpeed*getHorizontalDirection();
			lspeed = lspeed > 32 ? 32:lspeed;//don't go too high
			Vector3 lightningVelocity = new Vector3(lspeed,0,0);
			this.transform.Translate(lightningVelocity*Time.deltaTime);//move ball
		}else if(freezeBall){
			//did we toggle freeze ball in place
		}else{
			this.transform.Translate(velocity*Time.deltaTime);//move ball		
		}
		
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
	//change velocity by a factor of v
	public void changeVelocity(float v){
		velocity = velocity * v;
	}
	public void loseVelocityX(float v){
		velocity = new Vector3(velocity.x + v, velocity.y, velocity.z);
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
	public void reflectX(){
		velocity = new Vector3(velocity.x*-1, velocity.y, velocity.z);
	}
	public void reflectY(){
		velocity = new Vector3(velocity.x, velocity.y*-1, velocity.z);
	}
	//getters
	public int getHorizontalDirection(){
		if(velocity.x < 0){
			return -1;
		}else{
			return 1;
		}
	}
	public Vector3 getPos(){
		return this.transform.position;
	}
	public List<BallEffects> getEffects(){
		return effectsList;
	}
	public float getSpeed(){
		return currSpeed;
	}
	public Vector3 getVelocity(){
		return velocity;
	}
	public Vector3 getLowerBounds(){
		return lowerBounds;
	}
	public Vector3 getUpperBounds(){
		return upperBounds;
	}
}
