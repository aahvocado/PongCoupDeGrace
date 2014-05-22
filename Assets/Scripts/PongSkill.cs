using UnityEngine;
using System.Collections;

public class PongSkill : MonoBehaviour {
	public string name;//string name for this skill
	public int id;//id number for this skill
	public GameObject owner;//player that used this skill
	
	private string specialEffectName;
	private float baseDamage;//damage on opposition hit?
	private Vector3 baseMovementModifier;//multiply the ball velocity by this much
	
	private float baseCooldown;//base cooldown
	private float currCooldown;//current max cooldown
	private float cooldown;//the cooldown timer
	
	//private Vector3 currMovementModifier;
	
	public PongSkill(int idNum){//changes skill to id
		changeSkill(idNum);
	}
	public PongSkill(string skillName){//changes skill to name
		changeSkill(skillName);
	}
	
	// Use this for initialization
	void Start () {
		baseDamage = 0.0f;
		baseMovementModifier = new Vector3(0,0,0);
	}
	//changes this skill based on the id number
	public void changeSkill(int idNum){
		id = idNum;
		switch(idNum){
		case 4:
			name = "fireblast";
			baseDamage = 5.0f;
			break;
		case 3:
			name = "ignite";
			baseCooldown = 450.0f;
			break;
		case 2:
			name = "firewall";
			break;
			
		case 1:
			name = "forward smash";
			baseCooldown = 150.0f;
			baseDamage = 10.0f;
			baseMovementModifier = new Vector3(3f, 0.5f, 1f);
			break;
		case 0://reset
			name = "null";
			baseDamage = 0.0f;
			baseMovementModifier = new Vector3(0,0,0);
			break;
		}
		currCooldown = baseCooldown;
	}
	//changes this skill based on the name
	public void changeSkill(string skillName){
		switch(skillName){
		case "fireblast":
			changeSkill(4);
			break;
		case "ignite":
			changeSkill(3);
			break;
		case "firewall":
			changeSkill(2);
			break;
		case "forward smash":
			changeSkill (1);
			break;
		case "null":
			changeSkill(0);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void updateSkill(){
		if(currCooldown < baseCooldown){
			currCooldown ++;
		}
	}
	//reset everything about this skill to it's id
	public void goOnCooldown(){
		currCooldown = 0;
	}
	public void reset(){
		changeSkill(id);
	}
	//getters
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
