using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class DialogueHead {

	[XmlAttribute]
	public int NPCName {
		get ;
		private set ;
	}

	public List<int> IdsChecked;

	public DialogueLine FirstLine {
		get;
		private set;
	}

	public void SetHeadToLines(){
		FirstLine.SetHead (this);
	}

	public DialogueLine GetLineById(int id){
		DialogueLine foundLine = FirstLine.FindLineById (id);
		/*
		if (foundDialogueLine == null) {
			Debug.Log ("No line found with ID of " + id);
		}
		*/
		IdsChecked.Clear ();
		return foundLine;
	}

	public DialogueHead(){
		IdsChecked = new List<int> ();
	}
}

