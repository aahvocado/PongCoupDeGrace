using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour {
	private string name;//name of this effect
	private int id;
	
	private float maxTimer;//time it takes for this effect to go away
	public float timer;//time 
	
	
	public PlayerEffects(string n){
		changeEffect(n);
	}
	public PlayerEffects(int i){
		changeEffect(i);
	}
	public void refresh(){
		timer = maxTimer;
	}
	public void changeEffect(int idNum){
		id = idNum;
		switch(idNum){
		case 1:
			name = "burning";
			maxTimer = 90;
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
		case "burning":
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
