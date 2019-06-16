using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;


namespace VRestionnaire {



	public class QuestionPanelRadioGridUI:QuestionPanelUI, IQuestionPanelUI {
		public LayoutElement layoutElement;
		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public TMP_Text instructionsText;
		public TMP_Text idText;


		public GameObject labelPrefab;
		public GameObject radioItemPrefab;

		[SerializeField] RadioGridQuestion question;
		List<RadioGroup> questionItems;



		void IQuestionPanelUI.SetQuestion(Question q)
		{
			question = q as RadioGridQuestion;

			instructionsText.text = question.instructions;
			idText.text = question.id;

			gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
			gridLayout.constraintCount = question.labels.Length + 1; // + 1 for the questions
																	 //
			GameObject empty = Instantiate(labelPrefab);
			empty.GetComponent<TMP_Text>().text = "";
			empty.transform.parent = itemsUI;
			for(int i = 0; i < question.labels.Length; i++) {
				GameObject label = Instantiate(labelPrefab);
				TMP_Text labelText = label.GetComponent<TMP_Text>();
				labelText.text = question.labels[i];
				labelText.alignment = TextAlignmentOptions.Center;
				label.transform.parent = itemsUI;
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
				RadioGroup radioGroup = new RadioGroup(question.q_text[i].id, false);
				radioGroup.OnGroupSelected += OnItemSelected;
				for(int j = 0; j < question.labels.Length; j++) {
					GameObject item = Instantiate(radioItemPrefab);
					Toggle toggle = item.GetComponent<Toggle>();
					radioGroup.AddToggle(toggle);
					item.transform.parent = itemsUI;
				}
				radioGroup.Init();
				questionItems.Add(radioGroup);
			}

			Canvas.ForceUpdateCanvases();
			LayoutGridElements(); //Invoke("LayoutGridElements",0.16f);
			Canvas.ForceUpdateCanvases();



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
			question.isAnswered = true;
			QuestionItem item = question.q_text.First((q) => { return q.id == questionId; });
			int idx = Array.IndexOf(question.q_text, item);
			question.answers[idx] = itemId;
		}
	}
}
