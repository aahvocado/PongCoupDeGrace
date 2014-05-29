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
	public void refresh(){
		timer = maxTimer;
	}
	public void reset(){
		changeEffect(name);
	}
	//timer
	public void updateEffect(){
		if(timer > 0){
			timer --;
		}
	}
	//set to effect
	public void changeEffect(string n){
		name = n;
		switch(n){
		case "lightning straight":
			maxTimer = 30;
			//lightning strike just makes this ball go straight and then bounce off
			break;
		case "ignited":
			maxTimer = 60f;
			break;
		default://reset
			name = "null";
			maxTimer = 0;
			break;
		}
		timer = maxTimer;
	}
	//setters
	public void setLowCooldown(float t){
		maxTimer = t;
		//maxTimer = maxTimer *.1f;
	}
	//getters
	//isCheckable basically says if it's past 10 frames
	public bool isCheckable(){
		if(timer < maxTimer - 10){
			return true;
		}
		return false;
	}
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
	public float getTimerMax(){
		return maxTimer;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//this doesn't actually get called I don't think		
		
	}
	
}
