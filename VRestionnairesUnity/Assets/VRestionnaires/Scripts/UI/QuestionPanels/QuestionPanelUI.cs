using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {
	public interface IQuestionPanelUI {
		void SetQuestion(Question q);
		//void CheckMandatory();
	}

	public abstract class QuestionPanelUI:MonoBehaviour {
		public LayoutElement headerLayout;
		public LayoutElement bodyLayout;

		public TMP_Text instructionsText;
		public TMP_Text idText;
	}


}


