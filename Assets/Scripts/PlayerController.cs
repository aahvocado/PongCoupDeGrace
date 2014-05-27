using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public GameObject ball;
	public GameObject paddle;
	private float vMove;//get passed this from Pong Game Controller
	
	public float maxHealth;
	private float currHealth;
	public float basePower;//damage?
	private float currPower;
	public float baseArmor;//defense?
	private float currArmor;
	
	private string action;//current action
	private ArrayList actionQueue;//action queue that I'm not sure I'm using yet
	//public GameObject paddle;//the paddle this belongs to
	private PaddleController paddleScript;//the script that the paddle utilizes
	private PongSkill skillPassive;//passive skill
	private PongSkill skillA = new PongSkill("null");//first skill name
	private PongSkill skillB;//second skill name
	private PongSkill skillC;//third skill name
	
	private PongSkill currentSkill;
	private List<PlayerEffects> effectsList;
	

	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
		currPower = basePower;
		currArmor = baseArmor;
		//
		effectsList = new List<PlayerEffects>();
		paddleScript = this.GetComponent<PaddleController>();
		currentSkill = null;
	}
	public void takeDamage(float damage){
		currHealth = currHealth - damage;
	}
	// Update is called once per frame
	void Update () {
		//playerInput();
		paddleScript.setVerticalMove(vMove);
		action = paddleScript.getCurrentAction();
		
		updateEffects();
		//check skill
		if(skillA.getName() != "null"){
			updateSkills ();
		}else{
			print ("MISSING SKILLS");
		}
		//debug
	}
	void updateSkills(){
		//check if using a skill
		if(!isAction ()){
			currentSkill = new PongSkill(0);
		}
		//cooldown all skills
		skillPassive.updateSkill();
		skillA.updateSkill();
		skillB.updateSkill();
		skillC.updateSkill();
	}
	//
	void updateEffects(){
		foreach(PlayerEffects e in effectsList){
			e.updateEffects();
			if(!e.isActive()){
				//kind of a bad way to remove right now
				//effectsList.Remove(e);
			}
		}
	}
	//use a skill
	public string useSkill(PongSkill skill){
		if(!isAction() && !skill.isOnCooldown()){
			skill.goOnCooldown();
			switch(skill.getName()){
				case "fireblast":
					useFireblast();
					break;
				case "forward smash":
					paddleScript.forwardSmash();
					break;
				case "ignite":
					useIgnite();
					break;
				
			}
			currentSkill = skill;
			return skill.getName();
		}else{
			return "none";
		}
	}
	//add a player effect
	public void addEffect(PlayerEffects e){
		if(getEffect ("burning").getName()=="null"){
			effectsList.Add (new PlayerEffects("burning"));
		}else{
			getEffect ("burning").refresh();
		}
	}
	//using a specific skill
	void useFireblast(){
		Vector3 pos = new Vector3(paddle.transform.position.x, this.transform.position.y, this.transform.position.z);
		GameObject fb = (GameObject)Instantiate(Resources.Load ("Fireblast", typeof(GameObject)), pos, Quaternion.identity);
		FireblastScript fbs = fb.GetComponent<FireblastScript>();
		if(this.transform.position.x > 0){
			fbs.flip();
			//fbs.setSpeed(-fbs.getSpeed ());
		}		

	}
	void useIgnite(){
		ball.GetComponent<PongBall>().addEffect(new BallEffects("ignited"));
	}
	public void checkForBurningDamage(){
		if(isBurning()){//check for burning
			takeDamage(Mathf.Ceil(getCurrHealth()*.07f));
		}else{
			addEffect(new PlayerEffects("burning"));//add burn
		}
	}
	
	//getters
	//gets an effect from the list
	public PlayerEffects getEffect(string effectName){
		foreach(PlayerEffects e in effectsList){
			if(e.getName() == effectName){
				return e;
			}
		}
		return new PlayerEffects("null");
	}
	public bool isBurning(){
		PlayerEffects effect = getEffect ("burning");
		if(effect.getName()!="null" && effect.isActive()){
			return true;
		}
		return false;
	}
	public float getMaxHealth(){
		return maxHealth;
	}
	public float getCurrHealth(){
		return currHealth;
	}
	//are we currently using a skill or action
	public bool isAction(){
		if(action == "forward smash"){
			return true;
		}
		return false;
	}
	public List<PlayerEffects> getEffectsList(){
		return effectsList;
	}
	public PongSkill getCurrentSkill(){
		return currentSkill;
	}
	public string getAction(){
		return action;
	}
	public PongSkill getSkillPassive(){
		return skillPassive;
	}
	public PongSkill getSkillA(){
		return skillA;
	}
	public PongSkill getSkillB(){
		return skillB;
	}
	public PongSkill getSkillC(){
		return skillC;
	}
	public float getVMove(){
		return vMove;
	}
	//setters
	public void setSkillPassive(PongSkill s){
		skillPassive = s;
	}
	public void setSkillA(PongSkill s){
		skillA = s;
	}
	public void setSkillB(PongSkill s){
		skillB = s;
	}
	public void setSkillC(PongSkill s){
		skillC = s;
	}
	public void setVMove(float v){
		vMove = v;
	}
}
