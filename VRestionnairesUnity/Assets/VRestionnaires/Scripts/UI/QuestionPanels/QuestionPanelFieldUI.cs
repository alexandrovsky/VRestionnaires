using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelFieldUI:QuestionPanelUI<FieldQuestion>, IQuestionPanelUI {

		public TMP_InputField inputField;
		public TMP_Text placeholder;

		public override void SetQuestion(FieldQuestion q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);
			instructionsText.text = question.instructions;
			idText.text = question.id;

			placeholder.text = question.placeholder;
			inputField.onValueChanged.AddListener(OnFieldSubmited);
			//inputField.onEndEdit.AddListener(OnFieldSubmited);
			//inputField.onSubmit.AddListener(OnFieldSubmited);
			//inputField.onDeselect.AddListener(OnFieldSubmited);
		}


		void OnFieldSubmited(string value)
		{
			question.isAnswered = true;
			question.answer = value;
			print(question.id + " " + question.answer);
			OnQuestionAnswered.Invoke(question);
		}

	}

}

