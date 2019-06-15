using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRestionnaire {
	public interface IQuestionPanelUI {
		void SetQuestion(Question q);
		//void CheckMandratory();
	}

	public abstract class QuestionPanelUI:MonoBehaviour {
		//public bool isAnswered;
		//public Dictionary<string,int> answers;
	}


}


