using UnityEngine;
using System.Collections;

public class PlanetButton : MonoBehaviour {
	public Texture2D normalTexture;
	public Texture2D hoverTexture;
	public Vector2 pos;
	//info to pass over
	public string enemyName;
	
	void OnGUI(){
        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if(GUI.Button(new Rect(pos.x,pos.y,120,120), normalTexture)) {
            Application.LoadLevel("engine_test");
			
			PlayerPrefs.SetString("enemyName", enemyName);
			PlayerPrefs.Save();
        }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

}
