using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject debugText;
	//public GameObject paddle;//the paddle this belongs to
	private PaddleController paddleScript;//the script that the paddle utilizes
	public PongSkill skillA;//first skill name
	public PongSkill skillB;//second skill name
	public PongSkill skillC;//third skill name
	

	// Use this for initialization
	void Start () {
		paddleScript = this.GetComponent<PaddleController>();
		skillA = new PongSkill("forward smash");
		skillB = new PongSkill("forward smash");
		skillC = new PongSkill("forward smash");
	}
	
	// Update is called once per frame
	void Update () {
		playerInput();
		//
		TextMesh tm = debugText.GetComponent<TextMesh>();
		tm.text = "g: "+skillA.getName()+
				  "\nh: "+skillB.getName()+
				  "\nj: "+skillC.getName();
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
		switch(skill.getID()){
			case 1:
				paddleScript.forwardSmash();
				break;
			
		}
		return skill.getName();
	}
}
