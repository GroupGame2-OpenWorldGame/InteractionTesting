using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class DialogueLine {

	[XmlAttribute]
	public int Id {
		get ;
		private set ;
	}

	public string Speaker {
		get;
		private set;
	}

	public string Text {
		get;
		private set;
	}

	public DialogueLine NextLine {
		get;
		private set;
	}

	//Set Next Line Here
	public int NextLineId {
		get;
		private set ;
		
	}
		
	public DialogueHead Head {
		get;
		protected set;
	}

	public string TriggerWhenHit {
		get;
		private set;
	}

	public bool TriggerPassed {
		get;
		set;
	}

	public virtual void SetLine(){
		if (NextLine == null) {
			if (NextLineId != 0) {
				NextLine = Head.GetLineById (NextLineId);
				if (!NextLine.LinesSet ()) {
					NextLine.SetLine ();
				}
			}
		} else if (!NextLine.LinesSet ()) {
			NextLine.SetLine ();
		}
	}

	public virtual bool LinesSet(){
		if (NextLine == null && NextLineId != 0) {
			return false;
		}
		return true;
	}

	public virtual void SetHead(DialogueHead top){
		Head = top;
		if (NextLine != null) {
			if (NextLine.Head == null) {
				NextLine.SetHead (top);
			}
		}
	}

	public virtual DialogueLine FindLineById(int searchId){
		if (!Head.IdsChecked.Contains (Id)) {
			if (Id == searchId) {
				return this;
			} else {
				Head.IdsChecked.Add (Id);
				if (NextLine != null) {
					return NextLine.FindLineById (searchId);
				}
			}
		}
		return null;
	}
}
