using UnityEngine;
using System.Collections;

public class BallEffects : MonoBehaviour {
	private string name;//name of this effect
	private int id;
	
	private float maxTimer;//time it takes for this effect to go away
	public float timer;//time 
	
	
	public BallEffects(string n){
		changeEffect(n);
	}
	public BallEffects(int i){
		changeEffect(i);
	}
	
	public void changeEffect(int idNum){
		id = idNum;
		switch(idNum){
		case 1:
			name = "on fire";
			maxTimer = 160;
			break;
		case 0://reset
			name = "null";
			maxTimer = 0;
			break;
		}
		timer = maxTimer;
	}
	public void changeEffect(string n){
		switch(n){
		case "on fire":
			changeEffect (1);
			break;
		case "null":
			changeEffect(0);
			break;
		}
	}
	
	//getters
	public string getName(){
		return name;
	}
	public bool isActive(){
		if(timer > 0){
			return true;
		}
		return false;
	}
	public float getTimer(){
		return timer;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//this doesn't actually get called I don't think		
		if(timer > 0){
			timer --;
		}
	}
}
