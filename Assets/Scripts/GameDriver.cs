using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public enum GameState{
	OverWorld,
	Dialogue
}

public class GameDriver : MonoBehaviour {

	[Header("Game State")]
	public GameState gameState = GameState.OverWorld;
	[Space(8)]

	[Header("Dialogue")]
	public GameObject dialogueBox;
	public Text dialogueText;
	public Text speakerName;
	public Image playerImage;
	public Image npcImage;
	//[Space(8)]

	private NPCScript npcTarget;
	private Dialogue currentDialogue;
	private int currentLine = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public NPCScript NPCTarget{
		get{ 
			return npcTarget; 
		}
		set{
			npcTarget = value; 
		}
	}

	public int DialogueLength{
		get{ return currentDialogue.Length; }
		set { currentDialogue.Length = value; }
	}

	public void StartDialogue(){
		gameState = GameState.Dialogue;
		DeserializeXMLDialogue (npcTarget.dialogueXMLPath);
		if (gameState == GameState.Dialogue) {
			speakerName.text = currentDialogue.Speaker;
			dialogueText.text = currentDialogue.Speech [0];
			currentLine = 0;
			dialogueBox.SetActive (true);
		}
	}

	public void AdvanceDialogue(){
		currentLine++;
		dialogueText.text = currentDialogue.Speech [currentLine];
		currentDialogue.Length--;
	}

	public void EndDialogue(){
		dialogueBox.SetActive (false);
		gameState = GameState.OverWorld;
	}


	public void DeserializeXMLDialogue(string xmlDialoguePath){
		System.IO.FileStream filestream;
		XmlReader reader;
		XmlSerializer serializer = new XmlSerializer (typeof(Dialogue), new XmlRootAttribute("Dialogue"));

		//TextAsset xmlDialogue = (TextAsset)Resources.Load (xmlDialoguePath);


		if (System.IO.File.Exists(UnityEngine.Application.dataPath + xmlDialoguePath)) {
			filestream = new System.IO.FileStream (UnityEngine.Application.dataPath + xmlDialoguePath, System.IO.FileMode.Open);
			reader = new XmlTextReader (filestream);
		} else {
			Debug.Log ("ERROR PARSING XML");
			EndDialogue ();
			return;
		}

		try {
			if (serializer.CanDeserialize (reader)) {
				currentDialogue = serializer.Deserialize (reader) as Dialogue;
			}
			else{
				Debug.Log("Falied");
			}
		} finally {
			reader.Close ();
			filestream.Close ();
			filestream.Dispose ();
			Debug.Log ("FINISHED");
			Debug.Log (currentDialogue.Id);
			Debug.Log (currentDialogue.Speaker);
			Debug.Log (currentDialogue.Length);
			Debug.Log (currentDialogue.Speech[0]);
			Debug.Log (currentDialogue.Speech[1]);
			Debug.Log (currentDialogue.Speech[2]);
		}
	}
}
