using UnityEngine;
using System.Collections;

public class PaddleAI : MonoBehaviour {
	public GameObject player;
	private PlayerController pc;
	public GameObject ball;
	private PongBall ballscript;
	
	public float vMoveMax = 1;
	public float vMoveSpeed = .3f;
	public float visibleRange = 9f;
	
	public PaddleAI(GameObject playerObject, GameObject ballObject){
		player = playerObject;
		pc = player.GetComponent<PlayerController>();
		//
		ball = ballObject;
		ballscript = ball.GetComponent<PongBall>();
		
		//
		//vMoveSpeed = player.tag == "Player2" ? -vMoveSpeed:vMoveSpeed;
	}
	
	//this does stuff
	public void updateAI(){
		Vector3 ballpos = ballscript.getPos();
		Vector3 pos = pc.getPos();
		if(Mathf.Abs (pos.x - ballpos.x) < visibleRange){//within visible x range?
			verticalChaseBall();
		}else{//slow down
			pc.setVMove(pc.getVMove()*.85f);
		}
		
	}
	//chase the ball
	void verticalChaseBall(){
		Vector3 ballpos = ballscript.getPos();
		Vector3 pos = pc.getPos();
				
		if(pos.y < ballpos.y){
			pc.setVMove(pc.getVMove()+vMoveSpeed);
		}else if(pos.y > ballpos.y){
			pc.setVMove(pc.getVMove()-vMoveSpeed);
		}
		checkVMoveSpeed();
	}
	//lazy way to keep vmove in AI max speed
	void checkVMoveSpeed(){
		if(pc.getVMove() > vMoveMax){
			pc.setVMove(vMoveMax);
		}else if(pc.getVMove() < -vMoveMax){
			pc.setVMove(-vMoveMax);
		}
	}
	//setters
	public void setVMoveSpeed(float v){
		vMoveSpeed = v;
	}
	
	//getters
	public float getVMoveSpeed(){
		return vMoveSpeed;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
