using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour {
	private string name;//name of this effect
	private int id;
	
	private float maxTimer;//time it takes for this effect to go away
	public float timer;//time 
	
	public float breezeDistance = 7;
	public PlayerEffects(string n){
		changeEffect(n);
	}
	public void refresh(){
		timer = maxTimer;
	}
	public void changeEffect(string n){
		name = n;
		switch(n){
		//sora
		case "breezing":
			maxTimer = 50;
			break;
		//vida
		case "burning":
			name = "burning";
			maxTimer = 90;
			break;
		default://reset
			name = "null";
			maxTimer = 0;
			break;
		}
		timer = maxTimer;
	}
	//getters
	public float getBreezeDistance(){
		return breezeDistance;
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
	public void updateEffects () {
		//this doesn't actually get called I don't think		
		if(timer > 0){
			timer --;
		}
	}
	// Update is called once per frame
	void Update () {
		//this doesn't actually get called I don't think		
		if(timer > 0){
			timer --;
		}
	}
}
