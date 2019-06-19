using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;


namespace VRestionnaire {



	public class QuestionPanelRadioGridUI:QuestionPanelUI, IQuestionPanelUI {
		public LayoutElement layoutElement;
		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;
		
		public GameObject labelPrefab;
		public GameObject radioItemPrefab;

		List<RadioGroup> questionItems;

		RadioGridQuestion radioGridQuestion;

		public override void SetQuestion(Question q,UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);
			radioGridQuestion = question as RadioGridQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
			gridLayout.constraintCount = radioGridQuestion.labels.Length + 1; // + 1 for the questions
																	 //
			GameObject empty = Instantiate(labelPrefab);
			empty.GetComponent<TMP_Text>().text = "";
			empty.transform.parent = itemsUI;
			empty.transform.position = Vector3.zero;

			for(int i = 0; i < radioGridQuestion.labels.Length; i++) {
				GameObject label = Instantiate(labelPrefab);
				TMP_Text labelText = label.GetComponent<TMP_Text>();
				labelText.text = radioGridQuestion.labels[i];
				labelText.alignment = TextAlignmentOptions.Center;
				label.transform.parent = itemsUI;
				//label.transform.position = Vector3.zero;
				label.transform.localPosition = Vector3.zero;
			}
			
			questionItems = new List<RadioGroup>();

			for(int i = 0; i < radioGridQuestion.q_text.Length; i++) {
				GameObject textObj = Instantiate(labelPrefab);
				LayoutElement layout = textObj.GetComponent<LayoutElement>();
				TMP_Text labelText = textObj.GetComponent<TMP_Text>();

				labelText.text = radioGridQuestion.q_text[i].text;
				Vector2 textSize = labelText.GetPreferredValues(radioGridQuestion.q_text[i].text);
				layout.preferredWidth = textSize.x;
				layout.preferredHeight = textSize.y;

				textObj.transform.parent = itemsUI;
				textObj.transform.localPosition = Vector3.zero;
				RadioGroup radioGroup = new RadioGroup(radioGridQuestion.q_text[i].id, false);
				radioGroup.OnGroupSelected += OnItemSelected;
				for(int j = 0; j < radioGridQuestion.labels.Length; j++) {
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
			
			QuestionItem item = radioGridQuestion.q_text.First((q) => { return q.id == questionId; });
			int idx = Array.IndexOf(radioGridQuestion.q_text, item);
			radioGridQuestion.answers[idx] = itemId;

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
