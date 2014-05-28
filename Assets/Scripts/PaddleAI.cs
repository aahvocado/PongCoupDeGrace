using UnityEngine;
using System.Collections;

public class PaddleAI : MonoBehaviour {
	public GameObject player;
	private PlayerController pc;
	public GameObject ball;
	private PongBall ballscript;
	
	public float vMoveMax = 1;
	public float vMoveSpeed = .2f;
	
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
		
		verticalChaseBall();
		
	}
	//chase the ball
	void verticalChaseBall(){
		Vector3 ballpos = ballscript.getPos();
		Vector3 pos = pc.getPos();
		
		//print (player.tag + " vMove " + ballpos.y);
		
		if(pos.y < ballpos.y){
			print ("moving up");
			pc.setVMove(pc.getVMove()+vMoveSpeed);

		}else if(pos.y > ballpos.y){
			print ("moving down");
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
