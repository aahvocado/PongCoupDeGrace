using UnityEngine;
using System.Collections;

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
	

	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
		currPower = basePower;
		currArmor = baseArmor;
		//
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
	
	//getters
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
