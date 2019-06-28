using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelDropDownUI:QuestionPanelUI, IQuestionPanelUI {

		[SerializeField] TMP_Dropdown dropdown;
		DropDownQuestion downQuestion;


		public override void InitWithAnswer()
		{
			if(downQuestion != null && downQuestion.isAnswered) {
				dropdown.value = downQuestion.items.IndexOf(downQuestion.answer);
			}
		}

		public override void SetQuestion(Question q,UnityAction<Question> answeredEvent,UISkinData skinData)
		{
			base.SetQuestion(q,answeredEvent, skinData);

			downQuestion = q as DropDownQuestion;

			instructionsText.text = question.instructions;
			idText.text = question.id;

			dropdown.ClearOptions();
			dropdown.AddOptions(downQuestion.items);

			dropdown.onValueChanged.AddListener(OnDropDownValueChanged);
		}


		void OnDropDownValueChanged(int itemId)
		{
			question.isAnswered = true;
			downQuestion.answer = downQuestion.items[itemId];
			print(question.id + " " + downQuestion.answer);
			OnQuestionAnswered.Invoke(question);
		}

	}
}
