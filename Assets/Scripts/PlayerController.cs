using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject debugText;
	public GameObject ball;
	
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
	private PongSkill skillA;//first skill name
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
		skillPassive = new PongSkill("null");
		skillA = new PongSkill("forward smash");
		skillB = new PongSkill("ignite");
		skillC = new PongSkill("forward smash");
		currentSkill = null;
	}
	
	// Update is called once per frame
	void Update () {
		playerInput();
		action = paddleScript.getCurrentAction();
		
		//check skill
		updateSkills ();
		//debug
		TextMesh tm = debugText.GetComponent<TextMesh>();
		tm.text = "hp: "+ currHealth + "/" + maxHealth+
				  "action: "+ action +
				  "\npassive: "+skillPassive.getName()+ " "+ skillPassive.displayCooldown()+
				  "\ng: "+skillA.getName()+ " "+ skillA.displayCooldown()+
				  "\nh: "+skillB.getName()+ " "+ skillB.displayCooldown()+
				  "\nj: "+skillC.getName()+ " "+ skillC.displayCooldown()+
				  "\n ";
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
	
	void playerInput(){
		//skill useage
	 	if (Input.GetKeyDown(KeyCode.G)){
			useSkill (skillA);
		}
		if (Input.GetKeyDown(KeyCode.H)){
			useSkill (skillB);
		}
		if (Input.GetKeyDown(KeyCode.J)){
			useSkill (skillC);
		}
		
		//send vertical movement value to the paddlescript
		float vMove = Input.GetAxis ("Vertical");
		paddleScript.setVerticalMove(vMove);
	}
	//use a skill
	string useSkill(PongSkill skill){
		if(!isAction() && !skill.isOnCooldown()){
			skill.goOnCooldown();
			switch(skill.getName()){
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
	void useIgnite(){
		ball.GetComponent<PongBall>().addEffect(new BallEffects("ignited"));
	}
	
	//getters
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
	//setters
}
