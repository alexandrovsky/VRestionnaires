using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelFieldUI:QuestionPanelUI, IQuestionPanelUI {

		public TMP_InputField inputField;
		public TMP_Text placeholder;

		FieldQuestion fieldQuestion;

		public override void InitWithAnswer()
		{
			if(fieldQuestion != null && fieldQuestion.isAnswered) {
				inputField.text = fieldQuestion.answer;
			}
		}

		public override void SetQuestion(Question q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);
			fieldQuestion = question as FieldQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			placeholder.text = fieldQuestion.placeholder;
			inputField.onValueChanged.AddListener(OnFieldSubmited);
			//inputField.onEndEdit.AddListener(OnFieldSubmited);
			//inputField.onSubmit.AddListener(OnFieldSubmited);
			//inputField.onDeselect.AddListener(OnFieldSubmited);
		}


		void OnFieldSubmited(string value)
		{
			question.isAnswered = true;
			fieldQuestion.answer = value;
			print(question.id + " " + fieldQuestion.answer);
			OnQuestionAnswered.Invoke(question);
		}

	}

}

