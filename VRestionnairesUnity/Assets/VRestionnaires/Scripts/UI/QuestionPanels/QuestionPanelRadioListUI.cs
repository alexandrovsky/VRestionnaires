using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


namespace VRestionnaire {
	public class QuestionPanelRadioListUI:QuestionPanelUI<RadioListQuestion>, IQuestionPanelUI {
		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public GameObject labelPrefab;
		public GameObject radioItemPrefab;

		
		RadioGroup radioGroup;


		public override void SetQuestion(RadioListQuestion q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);

			instructionsText.text = question.instructions;
			idText.text = question.id;

			radioGroup = new RadioGroup(question.id, false);
			radioGroup.OnGroupSelected += OnGroupSelected;

			if(question.horizontal) {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedRowCount;
				gridLayout.constraintCount = 1;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
			} else {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
				gridLayout.constraintCount = 2;
				gridLayout.childAlignment = TextAnchor.UpperCenter;
			}

			for(int i = 0; i < question.labels.Length; i++) {
				GameObject label = Instantiate(labelPrefab);
				TMP_Text text = label.GetComponent<TMP_Text>();
				text.text = question.labels[i];
				label.transform.parent = itemsUI;
				label.transform.localPosition = Vector3.zero;

				GameObject radioItem = Instantiate(radioItemPrefab);
				Toggle toggle = radioItem.GetComponent<Toggle>();
				radioGroup.AddToggle(toggle);
				
				radioItem.transform.parent = itemsUI;
				radioItem.transform.localPosition = Vector3.zero;
			}

			radioGroup.Init();
		}


		void OnGroupSelected(string qId,int itemId)
		{
			question.isAnswered = true;
			question.answer = question.labels[itemId];
			print("answered: " + qId + " item: " + question.answer);
			OnQuestionAnswered.Invoke(question);
		}

	}
}

