using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelCheckListUI:QuestionPanelUI, IQuestionPanelUI {

		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public TMP_Text instructionsText;
		public TMP_Text idText;

		public GameObject labeledCheckItemPrefab;


		[SerializeField] CheckListQuestion question;


		List<Toggle> toggles;

		public void SetQuestion(Question q)
		{
			question = q as CheckListQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			toggles = new List<Toggle>();

			answers = new Dictionary<string,int>();

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
				answers.Add(question.questions[i].id,int.MaxValue);
			}
		}

		void HandleToggleValueChanged(bool arg0)
		{
			isAnswered = true;
			for(int i = 0; i < answers.Count; i++) {
				answers[question.questions[i].id] = toggles[i].isOn ? 1 : 0;
				print("answered: " + i + " item: " + toggles[i].isOn);
			}
		}
	}
}



