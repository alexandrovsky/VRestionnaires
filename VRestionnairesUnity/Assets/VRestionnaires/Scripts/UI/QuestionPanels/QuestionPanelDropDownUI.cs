using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelDropDownUI:QuestionPanelUI, IQuestionPanelUI {

		[SerializeField] TMP_Dropdown dropdown;

		[SerializeField] DropDownQuestion question;



		public void SetQuestion(Question q)
		{
			question = q as DropDownQuestion;

			instructionsText.text = question.instructions;
			idText.text = question.id;

			dropdown.ClearOptions();
			dropdown.AddOptions(question.items);

			dropdown.onValueChanged.AddListener(OnDropDownValueChanged);
		}

		void OnDropDownValueChanged(int itemId)
		{
			question.isAnswered = true;
			question.answer = question.items[itemId];
			print(question.id + " " + question.answer);
		}

	}
}
