using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
namespace VRestionnaire {
	public class QuestionPanelSubmitUI:QuestionPanelUI, IQuestionPanelUI {

		public TMP_Text thankYouText;
		public TMP_Text submitButtonLabel;
		UnityAction<Question> submitEvent;
		public SubmitQuestion submitQuestion;

		public override void InitWithAnswer()
		{

		}

		public override void SetQuestion(Question q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);
			submitQuestion = q as SubmitQuestion;
			submitButton.onClick.AddListener(Submit);

		}

		public void Submit() {
			if(question != null) {
				question.isAnswered = true;
			}
			if(OnQuestionAnswered != null) {
				OnQuestionAnswered.Invoke(question);
			}
		}

	}

}

