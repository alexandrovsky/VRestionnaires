using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelDropDownUI:QuestionPanelUI, IQuestionPanelUI {

		public TMP_Text instructionsText;
		public TMP_Text idText;

		[SerializeField] TMP_Dropdown dropdown;

		[SerializeField] DropDownQuestion question;



		public void SetQuestion(Question q)
		{
			question = q as DropDownQuestion;

			instructionsText.text = question.instructions;
			idText.text = question.id;

			answers = new Dictionary<string,int>();

			answers.Add(question.id,int.MaxValue);

			dropdown.ClearOptions();
			dropdown.AddOptions(question.items);

			dropdown.onValueChanged.AddListener(OnDropDownValueChanged);
		}

		void OnDropDownValueChanged(int itemId)
		{
			isAnswered = true;
			answers[question.id] = itemId;
			print(question.id + " " + answers[question.id]);
		}

	}
}
