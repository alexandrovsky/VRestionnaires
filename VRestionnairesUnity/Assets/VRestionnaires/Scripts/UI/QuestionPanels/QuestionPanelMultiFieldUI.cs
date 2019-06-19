using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelMultiFieldUI:QuestionPanelUI, IQuestionPanelUI {
		public TMP_InputField inputField;
		public TMP_Text placeholder;


		public override void SetQuestion(Question q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);
			MultiFieldQuestion multiFieldQuestion = question as MultiFieldQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			placeholder.text = multiFieldQuestion.placeholder;

			inputField.onSubmit.AddListener(OnMultiFieldSubmit);
			inputField.onDeselect.AddListener(OnMultiFieldSubmit);
		}


		void OnMultiFieldSubmit(string input)
		{
			MultiFieldQuestion multiFieldQuestion = question as MultiFieldQuestion;
			question.isAnswered = true;
			multiFieldQuestion.answer = input;
			print(question.id + " " + multiFieldQuestion.answer);
			OnQuestionAnswered.Invoke(question);
		}

	}

}

