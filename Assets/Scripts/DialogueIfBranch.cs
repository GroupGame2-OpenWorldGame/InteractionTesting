using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class DialogueIfBranch : DialogueElement {

	[XmlElement]
	public string ConditionToCheck {
		get;
		set;
	}
		
	public int TrueLineId {
		get;
		private set;
	}

	public int FalseLineId {
		get;
		private set;
	}
}
