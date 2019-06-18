using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelMultiFieldUI:QuestionPanelUI, IQuestionPanelUI {
		public TMP_InputField inputField;
		public TMP_Text placeholder;

		[SerializeField] MultiFieldQuestion question;

		public void SetQuestion(Question q)
		{
			question = q as MultiFieldQuestion;
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
		}

	}

}

