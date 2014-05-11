using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {
	public GameObject paddle;//the paddle object itself
	private Animator anim;//animation controller?
	
	public float defaultSpeed;//movement speed
	private float currSpeed;
	private float vMove;//vertical movement should be passed in by PongPlayer

	public Vector3 defaultSize;//paddle size
	private Vector3 currSize;
	
	public Vector3 spawnPosition;//origin spawn position
	public Vector3 lowerBounds;//lower bounds
	public Vector3 upperBounds;//upper bounds
	

	// Use this for initialization
	void Start () {
		paddle.transform.position = spawnPosition;
		anim = paddle.GetComponent<Animator>();
		reset ();
	}
	
	void reset(){
		currSpeed = defaultSpeed;
		currSize = defaultSize;
	}
	//
	public void forwardSmash(){
		anim.SetBool("isForwardSmash", true);
	}
	
	// Update is called once per frame
	void Update () {
		paddle.transform.localScale = currSize;//set size correctly
		Vector3 pos = this.transform.position;//get position
		
		//AnimatorStateInfo asi = anim.GetNextAnimatorStateInfo(0);
		//print (isAnimating()+ " " +(asi.normalizedTime));
		
		//movement
		//float vMove = Input.GetAxis ("Vertical");
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
	//getters
	public float getSpeed(){
		return currSpeed;
	}
	
	public string getCurrentAction(){
		if(anim.GetBool("isForwardSmash")){
			return "forward smash";
		}else{
			return "idle";
		}
	}
	//is this animating non-idle states
	public bool isAnimating(){
		if(anim.GetBool("isForwardSmash")){
			return true;
		}
		return false;

	}
	//setters
	public void setVerticalMove(float v){
		vMove = v;
	}

}
