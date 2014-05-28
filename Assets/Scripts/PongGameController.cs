using UnityEngine;
using System.Collections;

public class PongGameController : MonoBehaviour {
	public GameObject ball;//ball
	public bool player_1_AI_Mode;//player 1 computer player?
	private PaddleAI p1AI;
	public GameObject player1;
	private PlayerController p1script;
	public GameObject p1text;
	
	public bool player_2_AI_Mode;//player 2 computer player?
	private PaddleAI p2AI;
	public GameObject player2;
	private PlayerController p2script;
	public GameObject p2text;

	// Use this for initialization
	void Start () {
		p1script = player1.GetComponent<PlayerController>();
		p1AI = new PaddleAI(player1, ball);
		p2script = player2.GetComponent<PlayerController>();
		p2AI = new PaddleAI(player2, ball);
		//
		p1script.setSkillPassive(new PongSkill("incinerate"));
		p1script.setSkillA(new PongSkill("fireblast"));
		p1script.setSkillB(new PongSkill("ignite"));
		p1script.setSkillC(new PongSkill("forward smash"));
		//
		p2script.setSkillPassive(new PongSkill("null"));
		p2script.setSkillA(new PongSkill("fireblast"));
		p2script.setSkillB(new PongSkill("ignite"));
		p2script.setSkillC(new PongSkill("forward smash"));
	}
	
	// Update is called once per frame
	void Update () {
		
		checkBall();
		//
		updateP1 ();
		updateP2 ();
		//update debugging text
		updateDebug(p1text.GetComponent<TextMesh>(), p1script);
		updateDebug(p2text.GetComponent<TextMesh>(), p2script);
	}
	//updates the debug based on player 
	void updateDebug(TextMesh tm, PlayerController pscript){
		string effectsText = " ";
		PlayerEffects pe = new PlayerEffects("null");
		if(pscript.isBurning()){
			pe = pscript.getEffect("burning");
			effectsText = effectsText + pe.getName() + " " + pe.getTimer() + "/" + pe.getTimerMax();
		}
		//add all the text to text mesh
		tm.text = "\nhp: "+ pscript.getCurrHealth() + "/" + pscript.getMaxHealth()+
				  "\naction: "+ pscript.getAction() + 
				  "\neffects: "+ effectsText + 
				  "\npassive: "+ pscript.getSkillPassive().getName()+ " "+ pscript.getSkillPassive().displayCooldown()+
				  "\ng: "+pscript.getSkillA().getName()+ " "+ pscript.getSkillA().displayCooldown()+
				  "\nh: "+pscript.getSkillB().getName()+ " "+ pscript.getSkillB().displayCooldown()+
				  "\nj: "+pscript.getSkillC().getName()+ " "+ pscript.getSkillC().displayCooldown()+
				  "\n ";
	}
	void checkBall(){
		PongBall ballScript = ball.GetComponent<PongBall>();
		if(ball.transform.position.x < ballScript.getLowerBounds().x){//passed player 1's goal
			p1script.takeDamage(3);
			ballScript.reset();
		}else if(ball.transform.position.x > ballScript.getUpperBounds().x){//passed player 1's goal
			p2script.takeDamage(3);
			ballScript.reset();
		}
	}
	//player 1
	void updateP1(){
		if(!player_1_AI_Mode){
			if (Input.GetKeyDown(KeyCode.G)){
				p1script.useSkill (p1script.getSkillA());
			}
			if (Input.GetKeyDown(KeyCode.H)){
				p1script.useSkill (p1script.getSkillB());
			}
			if (Input.GetKeyDown(KeyCode.J)){
				p1script.useSkill (p1script.getSkillC());
			}
			//send vertical movement value to the paddlescript
			p1script.setVMove(Input.GetAxis ("P1 Vertical"));
		}else{
			p1AI.updateAI();
		}
	}
	//player 2
	void updateP2(){
		if(!player_2_AI_Mode){
			if (Input.GetKeyDown(KeyCode.Keypad1)){
				p2script.useSkill (p2script.getSkillA());
			}
			if (Input.GetKeyDown(KeyCode.Keypad2)){
				p2script.useSkill (p2script.getSkillB());
			}
			if (Input.GetKeyDown(KeyCode.Keypad3)){
				p2script.useSkill (p2script.getSkillC());
			}
			//send vertical movement value to the paddlescript
			p2script.setVMove(Input.GetAxis ("P2 Vertical"));
		}else{
			p2AI.updateAI();
		}
	}
}
