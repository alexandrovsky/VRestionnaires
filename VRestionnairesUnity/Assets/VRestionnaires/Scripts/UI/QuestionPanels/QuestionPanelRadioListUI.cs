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
		//public VariableGridLayoutGroup gridLayout;

		public GameObject labelPrefab;
		public GameObject radioItemPrefab;

		List<Toggle> toggles;

		[SerializeField] RadioListQuestion radioListQuestion;


		//public int maxQuestionsVertical = 4;
		//[Range(0.1f,2.0f)]
		//public float preferredWidthScaler = 1.5f;
		//[Range(0.1f,2.0f)]
		//public float preferredHeightScaler = 1.5f;

		//[Range(0.1f,5.0f)]
		//public float maxWidth = 4.0f;
		//[Range(0.1f,5.0f)]
		//public float maxHeight = 2.5f;
		//public Vector2 spacing = new Vector2(0.5f,0.5f);


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
			LayoutGroup layoutGroup;

			if(radioListQuestion.horizontal) {
				layoutGroup = itemsUI.gameObject.AddComponent<HorizontalLayoutGroup>();
				layoutGroup.childAlignment = TextAnchor.MiddleCenter;
				layoutGroup.padding.left = 1;
				layoutGroup.padding.right = 1;
				layoutGroup.padding.top = 1;
				layoutGroup.padding.bottom = 1;
			} else {
				layoutGroup = itemsUI.gameObject.AddComponent<VerticalLayoutGroup>();
				layoutGroup.childAlignment = TextAnchor.MiddleCenter;
				layoutGroup.padding.left = 1;
				layoutGroup.padding.right = 1;
				layoutGroup.padding.top = 1;
				layoutGroup.padding.bottom = 1;
			}

			
			for(int i = 0; i < radioListQuestion.labels.Length; i++) {
				GameObject container = new GameObject("container",typeof(RectTransform));
				container.AddComponent<HorizontalLayoutGroup>();
				GameObject label = Instantiate(labelPrefab);
				TMP_Text text = label.GetComponent<TMP_Text>();
				text.text = radioListQuestion.labels[i];
				text.autoSizeTextContainer = true;
				text.enableAutoSizing = true;
				text.ForceMeshUpdate(true);
				


				text.margin = new Vector4(text.fontSize,0,0,0);

				GameObject radioItem = Instantiate(radioItemPrefab);
				Toggle toggle = radioItem.GetComponent<Toggle>();


				toggle.SetIsOnWithoutNotify(false);
				toggles.Add(toggle);
				toggle.onValueChanged.AddListener((val) => {
					OnItemSelected(toggle,radioListQuestion.id, val);
				});
				container.transform.parent = itemsUI;
				radioItem.transform.parent = container.transform;
				label.transform.parent = container.transform;


				label.transform.localPosition = Vector3.zero;
				label.transform.localRotation = Quaternion.identity;
				label.transform.localScale = label.transform.parent.localScale;

				


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

