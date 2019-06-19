using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelMultiFieldUI:QuestionPanelUI<MultiFieldQuestion>, IQuestionPanelUI {
		public TMP_InputField inputField;
		public TMP_Text placeholder;


		public override void SetQuestion(MultiFieldQuestion q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);

			instructionsText.text = question.instructions;
			idText.text = question.id;

			placeholder.text = question.placeholder;

			inputField.onSubmit.AddListener(OnMultiFieldSubmit);
			inputField.onDeselect.AddListener(OnMultiFieldSubmit);
		}


		void OnMultiFieldSubmit(string input)
		{
			question.isAnswered = true;
			question.answer = input;
			print(question.id + " " + question.answer);
			OnQuestionAnswered.Invoke(question);
		}

	}

}

