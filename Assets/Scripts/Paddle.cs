using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	public float defaultSpeed;//movement speed
	private float currSpeed;

	public Vector3 defaultSize;//paddle size
	private Vector3 currSize;
	
	public Vector3 spawnPosition;//origin spawn position
	public Vector3 lowerBounds;//lower bounds
	public Vector3 upperBounds;//upper bounds

	// Use this for initialization
	void Start () {
		this.transform.position = spawnPosition;
		reset ();
	}
	
	void reset(){
		currSpeed = defaultSpeed;
		currSize = defaultSize;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = currSize;//set size correctly
		Vector3 pos = this.transform.position;//get position

		//movement
		float vMove = Input.GetAxis ("Vertical");
		this.transform.Translate(Vector3.up*currSpeed*Time.deltaTime*vMove);

	}
	
	//returns true lower than lowerbounds or higher than upperbounds
	bool checkOutOfBounds(){
		Vector3 pos = this.transform.position;
		if(pos.x < lowerBounds.x || pos.y < lowerBounds.y || pos.z < lowerBounds.z){
			return true;
		}else if(pos.x > upperBounds.x || pos.y > upperBounds.y || pos.z > upperBounds.z){
			return true;
		}
		return false;
	}
}
