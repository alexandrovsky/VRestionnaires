using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VRestionnaire {
	public class QuestionPanelSubmitUI:QuestionPanelUI, IQuestionPanelUI {

		public Button submitButton;
		UnityAction<Question> submitEvent;

		public override void InitWithAnswer()
		{
			
		}

		public override void SetQuestion(Question q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);
			submitButton.onClick.AddListener(Submit);

		}

		public void Submit() {
			question.isAnswered = true;
			OnQuestionAnswered.Invoke(question);
		}

	}

}

