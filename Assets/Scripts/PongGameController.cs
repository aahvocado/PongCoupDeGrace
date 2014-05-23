using UnityEngine;
using System.Collections;

public class PongGameController : MonoBehaviour {
	public GameObject ball;
	public GameObject player1;
	public bool p1AI;//player 1 computer player?
	public GameObject player2;
	public bool p2AI;//player 2 computer player?


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkBall();
		//
		updateP1 ();
		updateP2 ();
	}
	void checkBall(){
		PlayerController p1script = player1.GetComponent<PlayerController>();
		PlayerController p2script = player2.GetComponent<PlayerController>();
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
		PlayerController p1script = player1.GetComponent<PlayerController>();
		if(!p1AI){
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
		}
	}
	//player 2
	void updateP2(){
		PlayerController p2script = player2.GetComponent<PlayerController>();
		if(!p2AI){
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
		}
	}
}
