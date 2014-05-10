using UnityEngine;
using System.Collections;

public class PongSkill : MonoBehaviour {
	public string name;//string name for this skill
	public int id;//id number for this skill
	public GameObject owner;//player that used this skill
	
	private float baseDamage;//damage on opposition hit?
	private Vector3 baseMovementModifier;//multiply the ball velocity by this much
	
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
			case 1:
				name = "forward smash";
				baseDamage = 10.0f;
				baseMovementModifier = new Vector3(1.5f, 0.5f, 0f);
				break;
		}
	}
	//changes this skill based on the name
	public void changeSkill(string skillName){
		switch(skillName){
			case "forward smash":
				changeSkill (1);
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//reset everything about this skill to it's id
	public void reset(){
		changeSkill(id);
	}
	//getters
	public string getName(){
		return name;
	}
	public int getID(){
		return id;
	}
}
