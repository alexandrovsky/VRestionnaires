using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using TMPro;


namespace VRestionnaire {
	public class QuestionPanelRadioListUI:QuestionPanelUI, IQuestionPanelUI {
		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public GameObject labelPrefab;
		public GameObject radioItemPrefab;

		List<Toggle> toggles;
		
		[SerializeField] RadioListQuestion radioListQuestion;


		public override void InitWithAnswer()
		{

			if(radioListQuestion != null && radioListQuestion.isAnswered) {
				for(int i = 0; i < toggles.Count; i++) {
					toggles[i].SetIsOnWithoutNotify(false);
				}
				toggles[radioListQuestion.answer].SetIsOnWithoutNotify(true);
			}
		}

		public override void HidePanel()
		{
			base.HidePanel();
		}

		public override void ShowPanel()
		{
			base.ShowPanel();
		}

		public override void SetQuestion(Question q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);
			radioListQuestion = question as RadioListQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			
			toggles = new List<Toggle>();
			

			if(radioListQuestion.horizontal) {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedRowCount;
				gridLayout.constraintCount = 1;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
			} else {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
				gridLayout.constraintCount = 2;
				gridLayout.childAlignment = TextAnchor.UpperCenter;
			}

			for(int i = 0; i < radioListQuestion.labels.Length; i++) {
				GameObject label = Instantiate(labelPrefab);
				TMP_Text text = label.GetComponent<TMP_Text>();
				text.text = radioListQuestion.labels[i];
				label.transform.parent = itemsUI;
				label.transform.localPosition = Vector3.zero;
				label.transform.localRotation = Quaternion.identity;
				label.transform.localScale = label.transform.parent.localScale;

				GameObject radioItem = Instantiate(radioItemPrefab);
				Toggle toggle = radioItem.GetComponent<Toggle>();

				
				toggle.SetIsOnWithoutNotify(false);
				toggles.Add(toggle);
				toggle.onValueChanged.AddListener((val) => {
					OnItemSelected(toggle,radioListQuestion.id, val);
				});

				radioItem.transform.parent = itemsUI;
				radioItem.transform.localPosition = Vector3.zero;
				radioItem.transform.localRotation = Quaternion.identity;
				radioItem.transform.localScale = radioItem.transform.parent.localScale;

				if(label.GetComponent<Button>()) {
					Button btn = label.GetComponent<Button>();
					btn.onClick.AddListener(() => {
						toggle.isOn = !toggle.isOn;
					});
				}
			}
		}

		void OnItemSelected(Toggle toggle, string qId, bool value)
		{
			int idx = toggles.IndexOf(toggle);
			foreach(Toggle t in toggles) {
				t.SetIsOnWithoutNotify(false);
			}
			toggle.SetIsOnWithoutNotify(true);
			radioListQuestion.answer = idx;
			radioListQuestion.isAnswered = true;

			print("answered: " + qId + " item: " + idx);
			OnQuestionAnswered.Invoke(question);
		}

	}
}

