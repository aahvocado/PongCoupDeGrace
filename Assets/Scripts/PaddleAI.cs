using UnityEngine;
using System.Collections;

public class PaddleAI : MonoBehaviour {
	public GameObject player;
	private PlayerController pc;
	public GameObject ball;
	private PongBall ballscript;
	
	private float vMoveMax = 1;
	private float vMoveSpeed = .2f;
	
	public PaddleAI(GameObject playerObject, GameObject ballObject){
		player = playerObject;
		pc = player.GetComponent<PlayerController>();
		//
		ball = ballObject;
		ballscript = ball.GetComponent<PongBall>();
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
		
		if(pos.y < ballpos.y){
			if(pc.getVMove()>-vMoveMax){
				pc.setVMove(pc.getVMove()-vMoveSpeed);
			}
		}else if(pos.y > ballpos.y){
			if(pc.getVMove()<vMoveMax){
				pc.setVMove(pc.getVMove()+vMoveSpeed);
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
