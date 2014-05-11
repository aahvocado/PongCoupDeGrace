using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject debugText;
	
	private string action;//current action
	private ArrayList actionQueue;//action queue that I'm not sure I'm using yet
	//public GameObject paddle;//the paddle this belongs to
	private PaddleController paddleScript;//the script that the paddle utilizes
	public PongSkill skillA;//first skill name
	public PongSkill skillB;//second skill name
	public PongSkill skillC;//third skill name
	
	private PongSkill currentSkill;
	

	// Use this for initialization
	void Start () {
		paddleScript = this.GetComponent<PaddleController>();
		skillA = new PongSkill("forward smash");
		skillB = new PongSkill("forward smash");
		skillC = new PongSkill("forward smash");
		currentSkill = null;
	}
	
	// Update is called once per frame
	void Update () {
		playerInput();
		action = paddleScript.getCurrentAction();
		
		//clear skill
		if(!isAction ()){
			currentSkill = new PongSkill(0);
		}
		//debug
		TextMesh tm = debugText.GetComponent<TextMesh>();
		tm.text = "action: "+ action +
				  "\ng: "+skillA.getName()+
				  "\nh: "+skillB.getName()+
				  "\nj: "+skillC.getName()+
				  "\n ";
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
		if(!isAction()){
			switch(skill.getID()){
				case 1:
					paddleScript.forwardSmash();
					break;
				
			}
			currentSkill = skill;
			return skill.getName();
		}else{
			return "none";
		}
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
