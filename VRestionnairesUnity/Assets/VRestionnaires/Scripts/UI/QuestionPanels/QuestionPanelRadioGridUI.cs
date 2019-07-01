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

		List<List<Toggle>> questionItems;

		[SerializeField] RadioGridQuestion radioGridQuestion;


		//[Range(0.1f,5.0f)]
		float maxQuestionLabelWidth = 2.5f;
		//[Range(0.1f,5.0f)]
		float maxQuestionTextHeight = 1.5f;

		//private void Start()
		//{
		//	Generate();
		//}

		public void Generate()
		{
			for(int i = 0; i < itemsUI.childCount; i++) {
				DestroyImmediate(itemsUI.GetChild(i).gameObject);
			}

			SetQuestion(radioGridQuestion,(q) => {

			});
		}

		public override void InitWithAnswer()
		{
			if(radioGridQuestion != null) {
				for(int i= 0; i < questionItems.Count; i++) {
					for(int j = 0; j < questionItems[i].Count; j++) {
						questionItems[i][j].SetIsOnWithoutNotify(false);
					}
					if(radioGridQuestion.answers[i] >= 0) {
						questionItems[i][radioGridQuestion.answers[i]].SetIsOnWithoutNotify(true);
					}
				}
			}
		}

		public override void SetQuestion(Question q,UnityAction<Question> answeredEvent, UISkinData skinData = null)
		{
			base.SetQuestion(q, answeredEvent, skinData);

			radioGridQuestion = question as RadioGridQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;


			questionItems = new List<List<Toggle>>();
			VerticalLayoutGroup panelLayout = itemsUI.GetComponent<VerticalLayoutGroup>();
			panelLayout.spacing = 0.5f;

			for(int i = 0; i < radioGridQuestion.q_text.Length; i++) {
				GameObject textObj = Instantiate(labelPrefab);
				LayoutElement layout = textObj.GetComponent<LayoutElement>();
				layout.enabled = true;
				layout.minHeight = 1;
				layout.minWidth = 1;
				layout.preferredWidth = 2;
				layout.preferredHeight = 2;
				

				TMP_Text questionText = textObj.GetComponent<TMP_Text>();
				
				questionText.text = radioGridQuestion.q_text[i].text;
				questionText.alignment = TextAlignmentOptions.Bottom;
				Vector2 textSize = questionText.GetPreferredValues(radioGridQuestion.q_text[i].text);
				//questionText.margin = new Vector4(0,0,1,-0.75f);

				textObj.transform.parent = itemsUI;
				textObj.transform.localPosition = Vector3.zero;
				textObj.transform.localRotation = Quaternion.identity;
				textObj.transform.localScale = textObj.transform.parent.localScale;
				//RadioGroup radioGroup = new RadioGroup(radioGridQuestion.q_text[i].id);
				List<Toggle> toggles = new List<Toggle>();
				//radioGroup.OnGroupSelected += OnItemSelected;
				GameObject itemsContainer = new GameObject("itemsContainer",typeof(RectTransform));
				itemsContainer.transform.parent = itemsUI;
				itemsContainer.transform.localPosition = Vector3.zero;
				itemsContainer.transform.localRotation = Quaternion.identity;
				itemsContainer.transform.localScale = itemsContainer.transform.parent.localScale;
				HorizontalLayoutGroup horizontalLayoutGroup = itemsContainer.AddComponent<HorizontalLayoutGroup>();
				horizontalLayoutGroup.childControlHeight = true;
				horizontalLayoutGroup.childControlWidth	= true;
				//horizontalLayoutGroup.spacing = -2;


				for(int j = 0; j < radioGridQuestion.labels.Length; j++) {

					GameObject container = new GameObject("container",typeof(RectTransform));
					VerticalLayoutGroup verticalLayoutGroup = container.AddComponent<VerticalLayoutGroup>();
					verticalLayoutGroup.childControlWidth = true;
					verticalLayoutGroup.childControlHeight = true;
					verticalLayoutGroup.spacing = 0.2f;

					container.transform.parent = itemsContainer.transform;
					container.transform.parent = container.transform;
					container.transform.localPosition = Vector3.zero;
					container.transform.localRotation = Quaternion.identity;
					container.transform.localScale = container.transform.parent.localScale;

					GameObject label = Instantiate(labelPrefab);
					TMP_Text labelText = label.GetComponent<TMP_Text>();
					labelText.GetComponent<SkinText>().textFormat = TextFormat.Small;
					labelText.text = radioGridQuestion.labels[j];
					labelText.alignment = TextAlignmentOptions.Center;
					label.transform.parent = container.transform;
					label.transform.localPosition = Vector3.zero;
					label.transform.localRotation = Quaternion.identity;
					label.transform.localScale = Vector3.one;


					LayoutElement labelLayout = labelText.GetComponent<LayoutElement>();
					labelLayout.enabled = true;
					labelLayout.minWidth = skinData.toggleSize.x;
					labelLayout.minHeight = 0.5f;// skinData.toggleSize.y;
					labelLayout.preferredWidth = skinData.toggleSize.x;
					labelLayout.preferredHeight = 1.0f; // skinData.toggleSize.y;
					

					GameObject item = Instantiate(radioItemPrefab);
					Toggle toggle = item.GetComponent<Toggle>();
					//LayoutElement toggleLayout = textObj.GetComponent<LayoutElement>();
					//toggleLayout.enabled = true;
					//toggleLayout.minHeight = 1;
					//toggleLayout.minWidth = 1;
					//toggleLayout.preferredWidth = 1;
					//toggleLayout.preferredHeight = 1;
					//radioGroup.AddToggle(toggle);
					toggle.SetIsOnWithoutNotify(false);
					toggle.onValueChanged.AddListener((val) => {
						OnItemSelected(toggle,radioGridQuestion.id,val);
					});

					toggles.Add(toggle);


					item.transform.parent = container.transform;
					item.transform.localPosition = new Vector3(0,0,-0.01f);
					//item.transform.localPosition = Vector3.zero;
					item.transform.localRotation = Quaternion.identity;
					item.transform.localScale = Vector3.one;
				}
				//radioGroup.Init();
				//questionItems.Add(radioGroup);
				questionItems.Add(toggles);
			}

			//Canvas.ForceUpdateCanvases();
			//LayoutGridElements(); //Invoke("LayoutGridElements",0.16f);
			//Canvas.ForceUpdateCanvases();
		}

		//public override bool CheckMandatory()
		//{
		//	foreach(int a in radioGridQuestion.answers) {
		//		if(a == -1) {
		//			return false;
		//		}
		//	}
		//	return true;
		//}

		void LayoutGridElements() {
			for(int i = 0; i < gridLayout.rows; i++) {
				float rh = gridLayout.GetRowHeight(i);
				print(rh);
				layoutElement.preferredHeight += rh;
			}
		}



		void OnItemSelected(Toggle toggle,string qId,bool value)
		{
			for(int i = 0; i < questionItems.Count; i++) {
				List<Toggle> toggles = questionItems[i];
				int idx = toggles.IndexOf(toggle);
				if(idx >= 0) {
					for(int j = 0; j < toggles.Count; j++) {
						toggles[j].SetIsOnWithoutNotify(false);
					}
					toggle.SetIsOnWithoutNotify(true);
					break;
				}
			}
			int counter = 0;
			for(int i = 0; i < questionItems.Count; i++) {
				for(int j = 0; j < questionItems[i].Count; j++) {
					if(questionItems[i][j].isOn) {
						radioGridQuestion.answers[i] = j;
						counter++;
					}
				}
			}
			if(counter == questionItems.Count) {
				radioGridQuestion.isAnswered = true;
				OnQuestionAnswered.Invoke(question);
			}
		}

	}
}
