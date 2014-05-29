using UnityEngine;
using System.Collections;
//defines a specific character
public class PongCharacter : MonoBehaviour {
	
	private string name;
	private float health;
	private float speed;
	private PongSkill skillPassive;
	private PongSkill skillA;
	private PongSkill skillB ;
	private PongSkill skillC;
	
	public PongSkill derp;
	
	public PongCharacter(string n){
		selectCharacter(n);
	}
	
	void selectCharacter(string n){
		name = n;
		derp = new PongSkill("fireblast");
		switch(n){
		case "Vida":
			health = 20;
			speed = 6;
			skillPassive = new PongSkill("incinerate");
			skillA = new PongSkill("fireblast");
			skillB = new PongSkill("ignite");
			skillC = new PongSkill("firewall");
			break;
		case "Sora":
			health = 22;
			speed = 4.5f;
			skillPassive = new PongSkill("rising wind");
			skillA = new PongSkill("lightning strike");
			skillB = new PongSkill("breeze");
			skillC = new PongSkill("whiplash");
			break;
		}
	}
	
	//getter
	public float getHealth(){
		return health;
	}
	public float getSpeed(){
		return speed;
	}
	public string getName(){
		return name;
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
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
