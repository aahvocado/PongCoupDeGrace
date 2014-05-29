using UnityEngine;
using System.Collections;

public class FirewallScript : MonoBehaviour {
	
	public int lifeMax;//how long does this stay on the field
	private int life;

	// Use this for initialization
	void Start () {
		life = lifeMax;
	}
	
	// Update is called once per frame
	void Update () {
		if(life < 0){
			Destroy (this.gameObject);
		}else{
			life--;
		}
	}
	
	
	//getters
	public bool isActive(){
		if(life>0){
			return true;
		}
		return false;
	}
	public int getLife(){
		return life;
	}
}
