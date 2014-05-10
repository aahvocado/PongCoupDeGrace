using UnityEngine;
using System.Collections;

public class PaddleAnimationController : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//finish skills
	void doneForwardSmash(){
		anim.SetBool("isForwardSmash", false);
	}
}
