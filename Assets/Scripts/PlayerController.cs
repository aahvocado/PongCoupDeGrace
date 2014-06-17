using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public GameObject ball;
	public GameObject paddle;
	private float height = 5;
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
	
	public float breezeStrength = .25f;
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
		stayWithinBoundaries();
		action = paddleScript.getCurrentAction();
		//check effects
		updateEffects();
		//rising wind check
		if(getSkillPassive().getName() == "rising wind"){
			paddleScript.bonusSpeed(getSkillPassive().getRisingWind());
		}
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
			currentSkill = new PongSkill("null");
		}
		//cooldown all skills
		skillPassive.updateSkill();
		skillA.updateSkill();
		skillB.updateSkill();
		skillC.updateSkill();
	}
	//
	void updateEffects(){
		//print ("dist "+ (getPos().x - ball.GetComponent<PongBall>().getPos().x));
		if(isBreezing()){
			updateBreezing();
		}
		//update timer of everything everything
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
			//normal
			skill.goOnCooldown();
			switch(skill.getName()){
			//sora
			case "lightning strike":
				useLightningStrike();
				break;
			case "breeze":
				useBreeze();
				break;
			case "whiplash":
				useWhiplash();
				break;
			//vida
			case "firewall":
				useFirewall();
				break;
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
			//rising wind check
			if(getSkillPassive().getName() == "rising wind"){
				getSkillPassive().risindWindDecrement();
			}
			return skill.getName();
		}else{
			return "none";
		}
	}
	//add a player effect
	public void addEffect(string n){
		if(getEffect (n).getName()=="null"){
			effectsList.Add (new PlayerEffects(n));
		}else{
			getEffect (n).refresh();
		}
	}
	//using a specific skill
	void useWhiplash(){
		PongBall ballScript = ball.GetComponent<PongBall>();
		ballScript.reflectX();
		//ballScript.reflectY();
	}
	void useLightningStrike(){
		paddleScript.forwardSmash();
	}
	void useFirewall(){
		Vector3 pos = new Vector3(paddle.transform.position.x, this.transform.position.y, this.transform.position.z);
		GameObject fb = (GameObject)Instantiate(Resources.Load ("Firewall", typeof(GameObject)), pos, Quaternion.identity);
	}
	void useFireblast(){
		Vector3 pos = new Vector3(paddle.transform.position.x, this.transform.position.y, this.transform.position.z);
		GameObject fb = (GameObject)Instantiate(Resources.Load ("Fireblast", typeof(GameObject)), pos, Quaternion.identity);
		FireblastScript fbs = fb.GetComponent<FireblastScript>();
		if(this.transform.position.x > 0){
			fbs.flip();
			//fbs.setSpeed(-fbs.getSpeed ());
		}		

	}
	void updateBreezing(){
		print ("checking breeze");
		//within horizontal and vertical boundaries of this paddle
		//isWithinPaddleHeight(ball.GetComponent<PongBall>().getPos()) && 
		if(Mathf.Abs(getPos().x - ball.GetComponent<PongBall>().getPos().x)< getEffect("breezing").getBreezeDistance()){
			ball.GetComponent<PongBall>().loseVelocityX(breezeStrength*getPaddleDirection());
		}
	}
	void useBreeze(){
		addEffect ("breezing");
	}
	void useIgnite(){
		ball.GetComponent<PongBall>().addEffect("ignited");
	}
	public void checkForBurningDamage(){
		if(isBurning()){//check for burning
			takeDamage(Mathf.Ceil(getCurrHealth()*.07f));
		}else{
			addEffect("burning");//add burn
		}
	}
	//keep the paddle within boundaries
	void stayWithinBoundaries(){
		Vector3 lower = paddleScript.getLower();
		Vector3 upper = paddleScript.getUpper ();
		if(getPos ().y < lower.y){//lower
			this.transform.position = new Vector3(this.transform.position.x, lower.y, this.transform.position.z);
		}
		if(getPos ().y > upper.y){//upper
			this.transform.position = new Vector3(this.transform.position.x, upper.y, this.transform.position.z);
		}
	}

	//getters
	//gets the direction the ball should be going based on this player
	public int getPaddleDirection(){
		return this.transform.root.gameObject.tag == "Player2" ? -1 : 1;
	}
	public bool isWithinPaddleHeight(Vector3 p){
		if(p.y < getPos().y - height && p.y > getPos().y + height){
			return true;
		}
		return false;
	}
	//gets an effect from the list
	public PlayerEffects getEffect(string effectName){
		foreach(PlayerEffects e in effectsList){
			if(e.getName() == effectName){
				return e;
			}
		}
		return new PlayerEffects("null");
	}
	public bool isBreezing(){
		PlayerEffects effect = getEffect ("breezing");
		if(effect.getName()!="null" && effect.isActive()){
			return true;
		}
		return false;
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
	public Vector3 getPos(){
		//note this has to use the child paddle's x and then this gameobject's y
		return new Vector3(paddle.transform.position.x, this.transform.position.y, this.transform.position.z);
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
		//because player 2 is flipped we have to inverse the currently visible vmove
		return this.transform.root.gameObject.tag == "Player2" ? vMove : vMove;
	}
	//setters
	public void setHealth(float hp){
		maxHealth = hp;
		if(currHealth > maxHealth){
			currHealth = maxHealth;
		}
	}
	public void setSpeed(float sp){
		paddleScript.setSpeed (sp);
	}
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
