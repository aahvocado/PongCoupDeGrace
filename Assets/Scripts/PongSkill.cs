using UnityEngine;
using System.Collections;

public class PongSkill : MonoBehaviour {
	
	public string name;//string name for this skill
	public int id;//id number for this skill
	private string description;//
	public GameObject owner;//player that used this skill
	
	private string specialEffectName;
	private float baseDamage;//damage on opposition hit?
	private Vector3 baseMovementModifier;//multiply the ball velocity by this much
	
	private float baseCooldown;//base cooldown
	private float currCooldown;//current max cooldown
	private float cooldown;//the cooldown timer
	
	private float risingWindMax = 4;//value for rising wind
	private float risingWindIncrement = .04f;//
	private float risingWindValue = 0;
	

	//private Vector3 currMovementModifier;
	public PongSkill(string skillName){//changes skill to name
		changeSkill(skillName);
	}

	// Use this for initialization
	void Start () {
		baseDamage = 0.0f;
		baseMovementModifier = new Vector3(0,0,0);
	}
	//changes this skill based on the name
	public void changeSkill(string skillName){
		name = skillName;
		switch(skillName){
		case "rising wind":
			risingWindValue = 0;
			description = "You gain wind over time and move faster with more wind but using an ability will make you lose " + risingWindMax/2 + " (50% of max) wind.";
			break;
		case "lightning strike":
			baseCooldown = 140;
			description = "";
			break;
		case "breeze":
			baseCooldown = 140;
			description = "";
			break;
		case "whiplash":
			baseCooldown = 140;
			description = "";
			break;
		//Vida skills
		case "incinerate":
			baseCooldown = 0;
			description = "Your abilities burn enemies. If they are already burned they take an additional 7% (rounded up) damage.";
			break;
		case "fireblast":
			baseCooldown = 45;
			description = "Shoot a fireball that does " + baseDamage + "damage and sets the enemy on fire."; 
			break;
		case "ignite":
			baseCooldown = 150;
			description = "Ignites the ball, burning any player that touches it.";
			break;
		case "firewall":
			baseCooldown = 200;
			description = "Creates a wall of fire at your current location. If the ball hits the firewall then it gets set on fire.";
			break;
		//generic
		case "forward smash":
			baseCooldown = 150.0f;
			baseDamage = 10.0f;
			baseMovementModifier = new Vector3(3f, 0.5f, 1f);
			break;
		default://reset
			name = "null";
			baseDamage = 0.0f;
			baseMovementModifier = new Vector3(0,0,0);
			break;
		}
		currCooldown = baseCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void updateSkill(){
		//rising wind
		switch(name){
		
		case "rising wind":
			if(risingWindValue < risingWindMax){
				risingWindValue = risingWindValue + risingWindIncrement;
			}else{
				risingWindValue = risingWindMax;
			}
			if(risingWindValue < 0){
				risingWindValue = 0;
			}
			baseCooldown = risingWindValue;
			break;
		}
		//normal
		if(currCooldown < baseCooldown){
			currCooldown ++;
		}
	}
	public void risindWindDecrement(){
		risingWindValue = risingWindValue - risingWindMax/2;
	}
	//reset everything about this skill to it's id
	public void goOnCooldown(){
		currCooldown = 0;
	}
	public void reset(){
		changeSkill(name);
	}
	public void setLowCooldown(float t){
		baseCooldown = t;
	}
	//getters
	public float getRisingWind(){
		return risingWindValue;
	}
	public bool isOnCooldown(){
		if(currCooldown < baseCooldown){
			return true;
		}
		return false;
	}
	public float getCooldown(){
		return currCooldown;
	}
	public string displayCooldown(){
		return currCooldown +"/"+baseCooldown;
	}
	public Vector3 getMovementModifier(){
		return baseMovementModifier;
	}
	public string getName(){
		return name;
	}
	public int getID(){
		return id;
	}
}
