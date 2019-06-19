using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelDropDownUI:QuestionPanelUI<DropDownQuestion>, IQuestionPanelUI {

		[SerializeField] TMP_Dropdown dropdown;

		public override void SetQuestion(DropDownQuestion q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);

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
			OnQuestionAnswered.Invoke(question);
		}

	}
}
