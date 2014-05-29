using UnityEngine;
using System.Collections;

public class PongGameController : MonoBehaviour {
	public bool setLowCooldowns;//make everything super low cooldown

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
		PongCharacter p1char = new PongCharacter("Vida");

		p2script = player2.GetComponent<PlayerController>();
		p2AI = new PaddleAI(player2, ball);
		PongCharacter p2char = new PongCharacter("Sora");
		
		//
		p1script.setSkillPassive(p1char.getSkillPassive ());
		p1script.setSkillA(p1char.getSkillA());
		p1script.setSkillB(p1char.getSkillB());
		p1script.setSkillC(p1char.getSkillC());
		p1script.setHealth(p1char.getHealth());
		p1script.setSpeed(p1char.getSpeed());
		//
		p2script.setSkillPassive(p2char.getSkillPassive());
		p2script.setSkillA(p2char.getSkillA());
		p2script.setSkillB(p2char.getSkillB());
		p2script.setSkillC(p2char.getSkillC());
		p2script.setHealth(p2char.getHealth());
		p2script.setSpeed(p2char.getSpeed());
	}
	
	// Update is called once per frame
	void Update () {
		//ball
		checkBall();
		//players
		updateP1 ();
		updateP2 ();
		//debug
		if(setLowCooldowns){
			p1script.getSkillPassive().setLowCooldown(30);
			p1script.getSkillA().setLowCooldown(30);
			p1script.getSkillB().setLowCooldown(30);
			p1script.getSkillC().setLowCooldown(30);
			
			p2script.getSkillPassive().setLowCooldown(30);
			p2script.getSkillA().setLowCooldown(30);
			p2script.getSkillB().setLowCooldown(30);
			p2script.getSkillC().setLowCooldown(30);
		}
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
