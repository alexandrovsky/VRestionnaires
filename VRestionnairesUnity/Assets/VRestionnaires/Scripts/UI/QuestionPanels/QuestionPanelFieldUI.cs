using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelFieldUI:QuestionPanelUI, IQuestionPanelUI {

		public TMP_InputField inputField;
		public TMP_Text placeholder;
		[SerializeField] FieldQuestion question;

		public void SetQuestion(Question q)
		{
			question = q as FieldQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			placeholder.text = question.placeholder;

			inputField.onSubmit.AddListener(OnFieldSubmited);
			inputField.onDeselect.AddListener(OnFieldSubmited);
		}


		void OnFieldSubmited(string value)
		{
			question.isAnswered = true;
			question.answer = value;
			print(question.id + " " + question.answer);
		}

	}

}

