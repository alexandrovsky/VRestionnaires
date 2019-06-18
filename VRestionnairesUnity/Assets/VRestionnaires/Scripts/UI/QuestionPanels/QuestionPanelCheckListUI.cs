using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelCheckListUI:QuestionPanelUI, IQuestionPanelUI {

		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public GameObject labeledCheckItemPrefab;


		[SerializeField] CheckListQuestion question;


		List<Toggle> toggles;

		public void SetQuestion(Question q)
		{
			question = q as CheckListQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			toggles = new List<Toggle>();

			if(question.horizontal) {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedRowCount;
				gridLayout.constraintCount = 1;
			} else {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
				gridLayout.constraintCount = 1;
			}

			for(int i = 0; i < question.questions.Length; i++) {
				GameObject checkItem = Instantiate(labeledCheckItemPrefab);
				Toggle toggle = checkItem.GetComponent<Toggle>();
				toggle.onValueChanged.AddListener(HandleToggleValueChanged);
				toggles.Add(toggle);

				TMP_Text label = checkItem.transform.Find("Label").GetComponent<TMP_Text>();
				label.text = question.questions[i].text;
				checkItem.transform.parent = itemsUI;
			}
		}

		void HandleToggleValueChanged(bool arg0)
		{
			question.isAnswered = true;
			for(int i = 0; i < question.answers.Length; i++) {
				question.answers[i] = toggles[i].isOn;
				print("answered: " + i + " item: " + toggles[i].isOn);
			}
		}
	}
}



