using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class DialogueIfBranch : DialogueLine {

	[XmlAttribute]
	public string ConditionInvoke {
		get;
		private set;
	}

	public DialogueLine TrueLine {
		get;
		private set;
	}

	public int TrueLineId {
		get;
		private set;
	}

	public DialogueLine FalseLine {
		get;
		private set;
	}

	public int FalseLineId {
		get;
		private set;
	}

	public override void SetLine(){
		if (TrueLine == null) {
			if (TrueLineId != 0) {
				TrueLine = Head.GetLineById (TrueLineId);
				if (!TrueLine.LinesSet ()) {
					TrueLine.SetLine ();
				}
			}
		} else if (!TrueLine.LinesSet ()){
			TrueLine.SetLine ();
		}

		if (FalseLine == null) {
			if (FalseLineId != 0) {
				FalseLine = Head.GetLineById (FalseLineId);
				if (!FalseLine.LinesSet ()) {
					FalseLine.SetLine ();
				}
			}
		} else if (!FalseLine.LinesSet ()){
			FalseLine.SetLine ();
		}
	}

	public override bool LinesSet(){
		if (TrueLine == null && TrueLineId != 0) {
			return false;
		} else if (FalseLine == null && FalseLineId != 0) {
			return false;
		} 
		return true;
	}

	public override void SetHead(DialogueHead top){
		Head = top;
		if (TrueLine != null) {
			if (TrueLine.Head == null) {
				TrueLine.SetHead (top);
			}
		}
		if (FalseLine != null) {
			if (FalseLine.Head == null) {
				FalseLine.SetHead (top);
			}
		}
	}

	public override DialogueLine FindLineById(int searchId){
		if (!Head.IdsChecked.Contains (Id)) {
			if (Id == searchId) {
				return this;
			} else {
				Head.IdsChecked.Add (Id);
				if (TrueLine != null) {
					DialogueLine returnLine = TrueLine.FindLineById (searchId);
					if (returnLine != null) {
						return returnLine;
					}
				}
				if (FalseLine != null) {
					DialogueLine returnLine = TrueLine.FindLineById (searchId);
					if (returnLine != null) {
						return returnLine;
					}
				}
			}
		}
		return null;
	}
}
