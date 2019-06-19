using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelCheckListUI:QuestionPanelUI<CheckListQuestion>, IQuestionPanelUI {

		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public GameObject labelPrefab;
		public GameObject checkItemPrefab;

		//[SerializeField] CheckListQuestion question;
		
		List<Toggle> toggles;

		public override void SetQuestion(CheckListQuestion q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);
			
			instructionsText.text = question.instructions;
			idText.text = question.id;

			toggles = new List<Toggle>();

			if(question.horizontal) {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedRowCount;
				gridLayout.constraintCount = 1;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
			} else {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
				gridLayout.constraintCount = 2;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
			}

			for(int i = 0; i < question.questions.Length; i++) {
				GameObject label = Instantiate(labelPrefab);
				TMP_Text text = label.GetComponent<TMP_Text>();
				text.text = question.questions[i].text;
				label.transform.parent = itemsUI;
				label.transform.localPosition = Vector3.zero;

				GameObject checkItem = Instantiate(checkItemPrefab);
				Toggle toggle = checkItem.GetComponent<Toggle>();
				toggle.isOn = false;
				toggle.onValueChanged.AddListener(HandleToggleValueChanged);
				toggles.Add(toggle);
				checkItem.transform.parent = itemsUI;
				checkItem.transform.localPosition = Vector3.zero;
			}
		}


		void HandleToggleValueChanged(bool arg0)
		{
			question.isAnswered = true;
			for(int i = 0; i < question.answers.Length; i++) {
				question.answers[i] = toggles[i].isOn;
				print("answered: " + i + " item: " + toggles[i].isOn);
			}
			OnQuestionAnswered.Invoke(question);
			
		}

		
	}
}



