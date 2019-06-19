using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;


namespace VRestionnaire {



	public class QuestionPanelRadioGridUI:QuestionPanelUI<RadioGridQuestion>, IQuestionPanelUI {
		public LayoutElement layoutElement;
		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;
		
		public GameObject labelPrefab;
		public GameObject radioItemPrefab;

		List<RadioGroup> questionItems;



		public override void SetQuestion(RadioGridQuestion q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);

			instructionsText.text = question.instructions;
			idText.text = question.id;

			gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
			gridLayout.constraintCount = question.labels.Length + 1; // + 1 for the questions
																	 //
			GameObject empty = Instantiate(labelPrefab);
			empty.GetComponent<TMP_Text>().text = "";
			empty.transform.parent = itemsUI;
			empty.transform.position = Vector3.zero;

			for(int i = 0; i < question.labels.Length; i++) {
				GameObject label = Instantiate(labelPrefab);
				TMP_Text labelText = label.GetComponent<TMP_Text>();
				labelText.text = question.labels[i];
				labelText.alignment = TextAlignmentOptions.Center;
				label.transform.parent = itemsUI;
				//label.transform.position = Vector3.zero;
				label.transform.localPosition = Vector3.zero;
			}
			
			questionItems = new List<RadioGroup>();

			for(int i = 0; i < question.q_text.Length; i++) {
				GameObject textObj = Instantiate(labelPrefab);
				LayoutElement layout = textObj.GetComponent<LayoutElement>();
				TMP_Text labelText = textObj.GetComponent<TMP_Text>();

				labelText.text = question.q_text[i].text;
				Vector2 textSize = labelText.GetPreferredValues(question.q_text[i].text);
				layout.preferredWidth = textSize.x;
				layout.preferredHeight = textSize.y;

				textObj.transform.parent = itemsUI;
				textObj.transform.localPosition = Vector3.zero;
				RadioGroup radioGroup = new RadioGroup(question.q_text[i].id, false);
				radioGroup.OnGroupSelected += OnItemSelected;
				for(int j = 0; j < question.labels.Length; j++) {
					GameObject item = Instantiate(radioItemPrefab);
					Toggle toggle = item.GetComponent<Toggle>();
					radioGroup.AddToggle(toggle);
					item.transform.parent = itemsUI;
					item.transform.localPosition = Vector3.zero;
				}
				radioGroup.Init();
				questionItems.Add(radioGroup);
			}

			//Canvas.ForceUpdateCanvases();
			//LayoutGridElements(); //Invoke("LayoutGridElements",0.16f);
			//Canvas.ForceUpdateCanvases();
		}

		public override bool CheckMandatory()
		{
			
			foreach(RadioGroup rg in questionItems) {
				if(rg.toggleGroup.ActiveToggles().ToList().Count() != 1) {
					return false;
				}
			}
			return true;
		}

		void LayoutGridElements() {
			for(int i = 0; i < gridLayout.rows; i++) {
				float rh = gridLayout.GetRowHeight(i);
				print(rh);
				layoutElement.preferredHeight += rh;
			}
		}


		void OnItemSelected(string questionId, int itemId)
		{
			
			QuestionItem item = question.q_text.First((q) => { return q.id == questionId; });
			int idx = Array.IndexOf(question.q_text, item);
			question.answers[idx] = itemId;

			int answersCount = 0;
			foreach(RadioGroup rg in questionItems) {
				answersCount += rg.toggleGroup.ActiveToggles().ToList().Count();
			}
			if(answersCount == questionItems.Count) {
				question.isAnswered = true;
				OnQuestionAnswered.Invoke(question);
			}


		}
	}
}
