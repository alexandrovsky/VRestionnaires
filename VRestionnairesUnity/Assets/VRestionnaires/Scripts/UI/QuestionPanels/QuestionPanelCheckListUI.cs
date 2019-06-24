using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelCheckListUI:QuestionPanelUI, IQuestionPanelUI {

		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public GameObject labelPrefab;
		public GameObject checkItemPrefab;

		CheckListQuestion checkQuestion;

		List<Toggle> toggles;


		public override void InitWithAnswer()
		{
			if(checkQuestion != null && checkQuestion.isAnswered) {
				for(int i = 0; i < checkQuestion.answers.Length; i++) {
					checkQuestion.answers[i] = toggles[i].isOn;
				}
			}
		}

		public override void SetQuestion(Question q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);

			instructionsText.text = question.instructions;
			idText.text = question.id;

			toggles = new List<Toggle>();
			checkQuestion = (question as CheckListQuestion);
			if(checkQuestion.horizontal) {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedRowCount;
				gridLayout.constraintCount = 1;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
			} else {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
				gridLayout.constraintCount = 2;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
			}

			for(int i = 0; i < checkQuestion.questions.Length; i++) {
				GameObject label = Instantiate(labelPrefab);
				TMP_Text text = label.GetComponent<TMP_Text>();
				text.text = checkQuestion.questions[i].text;
				label.transform.parent = itemsUI;
				label.transform.localPosition = Vector3.zero;
				label.transform.localRotation = Quaternion.identity;
				label.transform.localScale = label.transform.parent.localScale;
				

				GameObject checkItem = Instantiate(checkItemPrefab);
				Toggle toggle = checkItem.GetComponent<Toggle>();
				toggle.isOn = false;
				toggle.onValueChanged.AddListener(HandleToggleValueChanged);
				toggles.Add(toggle);
				checkItem.transform.parent = itemsUI;
				checkItem.transform.localPosition = Vector3.zero;
				checkItem.transform.localRotation = Quaternion.identity;
				checkItem.transform.localScale = label.transform.parent.localScale;

				if(label.GetComponent<Button>()) {
					Button btn = label.GetComponent<Button>();
					btn.onClick.AddListener(() => {
						toggle.isOn = !toggle.isOn;
					});
				}

			}
		}


		void HandleToggleValueChanged(bool arg0)
		{
			question.isAnswered = true;
			for(int i = 0; i < checkQuestion.answers.Length; i++) {
				checkQuestion.answers[i] = toggles[i].isOn;
				print("answered: " + i + " item: " + toggles[i].isOn);
			}
			OnQuestionAnswered.Invoke(question);

		}


	}
}



