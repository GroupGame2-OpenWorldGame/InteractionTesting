using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1.0f;
	public float rotationFactor = 15.0f;

	private GameDriver gameDriver;
	private bool inDialogueRange = false;
	private bool selectKeyPressed;

	// Use this for initialization
	void Start () {
		gameDriver = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameDriver> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (selectKeyPressed) {
			if (Input.GetAxis ("Select") == 0) {
				selectKeyPressed = false;
			}
		}
		switch (gameDriver.gameState) {
		case GameState.OverWorld:
			if (Input.GetAxis ("Vertical") >= 0.001 || Input.GetAxis ("Vertical") <= -0.001) {
				this.transform.position += this.transform.forward * speed * Input.GetAxis ("Vertical");
			}

			if (Input.GetAxis ("Horizontal") >= 0.001 || Input.GetAxis ("Horizontal") <= -0.001) {
				this.transform.Rotate (0f, rotationFactor * Input.GetAxis ("Horizontal"), 0f);
			}

			if (Input.GetAxis ("Select") != 0 && !selectKeyPressed && inDialogueRange) {
				selectKeyPressed = true;
				gameDriver.StartDialogue();
			}
			break;
		
		case GameState.Dialogue:
			if (!selectKeyPressed) {
				if (Input.GetAxis ("Select") > 0) {
					selectKeyPressed = true;
					gameDriver.AdvanceDialogue ();
				}
			} 
			break;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "NPC") {
			gameDriver.NPCTarget = other.gameObject.GetComponent<NPCScript> ();
			inDialogueRange = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "NPC") {
			if (other.gameObject.GetComponent<NPCScript> () == gameDriver.NPCTarget) {
				inDialogueRange = false;
			}
		}
	}
}
