using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace VRestionnaire {
	public class QuestionPanelRadioListUI:QuestionPanelUI, IQuestionPanelUI {
		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public GameObject labeledRadioItemPrefab;


		[SerializeField] RadioListQuestion question;
		RadioGroup radioGroup;


		public void SetQuestion(Question q)
		{
			question = q as RadioListQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			radioGroup = new RadioGroup(question.id, false);
			radioGroup.OnGroupSelected += OnGroupSelected;

			if(question.horizontal) {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedRowCount;
				gridLayout.constraintCount = 1;
			} else {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
				gridLayout.constraintCount = 1;
			}

			for(int i = 0; i < question.labels.Length; i++) {
				GameObject radioItem = Instantiate(labeledRadioItemPrefab);
				Toggle toggle = radioItem.GetComponent<Toggle>();
				radioGroup.AddToggle(toggle);
				TMP_Text label = radioItem.transform.Find("Label").GetComponent<TMP_Text>();
				label.text = question.labels[i];
				radioItem.transform.parent = itemsUI;
			}

			radioGroup.Init();
		}

		void OnGroupSelected(string qId,int itemId)
		{
			question.isAnswered = true;
			question.answer = question.labels[itemId];
			print("answered: " + qId + " item: " + question.answer);
		}

	}
}

