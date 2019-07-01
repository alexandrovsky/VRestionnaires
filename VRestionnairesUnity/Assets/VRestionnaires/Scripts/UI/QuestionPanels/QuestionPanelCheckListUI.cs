using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelCheckListUI:QuestionPanelUI, IQuestionPanelUI {

		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public GameObject labelPrefab;
		public GameObject checkItemPrefab;
		//public GameObject checkItemLabeledPrefab;

		[SerializeField]CheckListQuestion checkQuestion;

		List<Toggle> toggles;
		public int maxQuestionsVertical = 4;
		[Range(0.1f,2.0f)]
		public float preferredWidthScaler = 1.5f;
		[Range(0.1f,2.0f)]
		public float preferredHeightScaler = 1.5f;

		[Range(0.1f,5.0f)]
		public float maxWidth = 4.0f;
		[Range(0.1f,5.0f)]
		public float maxHeight = 2.5f;
		public Vector2 spacing = new Vector2(0.5f,0.5f);

		public float maxTextWidth = 16;
		public float maxTextHeight = 1.5f;

		public override void InitWithAnswer()
		{
			if(checkQuestion != null && checkQuestion.isAnswered) {
				for(int i = 0; i < checkQuestion.answers.Length; i++) {
					checkQuestion.answers[i] = toggles[i].isOn;
				}
			}
		}

		public override void SetQuestion(Question q, UnityAction<Question> answeredEvent, UISkinData skinData)
		{
			base.SetQuestion(q, answeredEvent,skinData);

			instructionsText.text = question.instructions;
			idText.text = question.id;

			toggles = new List<Toggle>();
			checkQuestion = (question as CheckListQuestion);
			gridLayout.startAxis = VariableGridLayoutGroup.Axis.Horizontal;
			if(checkQuestion.horizontal) {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedRowCount;
				gridLayout.constraintCount = 1;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
			} else {
				gridLayout.constraint = VariableGridLayoutGroup.Constraint.FixedColumnCount;
				int factor = checkQuestion.questions.Length / maxQuestionsVertical;
				factor = (factor == 0 ? 1 : factor);
				gridLayout.constraintCount = 2 * factor;
				gridLayout.childAlignment = TextAnchor.MiddleCenter;
				gridLayout.spacing = spacing;
				
				maxWidth = maxTextWidth / factor;
				maxHeight = maxTextHeight / factor;
			}

			for(int i = 0; i < checkQuestion.questions.Length; i++) {

				//GameObject labeledItem = Instantiate(checkItemLabeledPrefab);
				//GameObject checkItem = labeledItem.transform.Find("Checkbox").gameObject;

				GameObject checkItem = Instantiate(checkItemPrefab);
				Toggle toggle = checkItem.GetComponent<Toggle>();
				toggle.isOn = false;
				toggle.onValueChanged.AddListener(HandleToggleValueChanged);

				LayoutElement toggleLayout = toggle.GetComponent<LayoutElement>();
				toggleLayout.enabled = true;
				toggleLayout.minWidth = skinData.toggleSize.x;
				toggleLayout.minHeight = skinData.toggleSize.y;

				toggles.Add(toggle);


				//GameObject label = labeledItem.transform.Find("QuestionLabelInteractive").gameObject;
				GameObject label = Instantiate(labelPrefab);
				TMP_Text text = label.GetComponent<TMP_Text>();
				text.text = checkQuestion.questions[i].text;
				text.margin = new Vector4(-spacing.x/4f,0,0,0);
				
				text.autoSizeTextContainer = true;
				//text.enableAutoSizing = true;
				text.ForceMeshUpdate(true);


				LayoutElement labelLayout = label.GetComponent<LayoutElement>();
				//float w = text.preferredWidth * preferredWidthScaler;
				//float h = text.preferredHeight * preferredHeightScaler;
				////labelLayout.preferredWidth = - 1;
				////labelLayout.preferredHeight = - 1;
				labelLayout.preferredWidth = -1; //Mathf.Clamp(w,0,maxWidth);
				labelLayout.preferredHeight = -1; // Mathf.Clamp(h,0,maxHeight);




				if(label.GetComponent<Button>()) {
					Button btn = label.GetComponent<Button>();
					btn.onClick.AddListener(() => {
						toggle.isOn = !toggle.isOn;
					});
				}

				//labeledItem.transform.parent = itemsUI;
				checkItem.transform.parent = itemsUI;
				label.transform.parent = itemsUI;

				label.transform.localPosition = Vector3.zero;
				label.transform.localRotation = Quaternion.identity;
				label.transform.localScale = label.transform.parent.localScale;


				checkItem.transform.localPosition = Vector3.zero;
				checkItem.transform.localRotation = Quaternion.identity;
				checkItem.transform.localScale = label.transform.parent.localScale;
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(itemsUI);
		}


		void HandleToggleValueChanged(bool arg0)
		{
			question.isAnswered = true;
			for(int i = 0; i < checkQuestion.answers.Length; i++) {
				checkQuestion.answers[i] = toggles[i].isOn;
				print("answered: " + i + " item: " + toggles[i].isOn);
			}
			OnQuestionAnswered.Invoke(question);

		}


	}
}



