using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//public GameObject paddle;//the paddle this belongs to
	private PaddleController paddleScript;//the script that the paddle utilizes
	public string skillA;//first skill name
	public string skillB;//second skill name
	public string skillC;//third skill name
	

	// Use this for initialization
	void Start () {
		paddleScript = this.GetComponent<PaddleController>();
	}
	
	// Update is called once per frame
	void Update () {
		playerInput();
	}
	
	void playerInput(){
		//skill A
	 	if (Input.GetKeyDown(KeyCode.G)){
			paddleScript.forwardSmash();
		}
		//send vertical movement value to the paddlescript
		float vMove = Input.GetAxis ("Vertical");
		paddleScript.setVerticalMove(vMove);
	}
}
