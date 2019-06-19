using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionnairePanelUI:MonoBehaviour {

		public UISkinData skinData;

		public TMP_Text title;
		public TMP_Text instructions;

		public RectTransform topPanel;
		public RectTransform questionsPanel;
		public RectTransform bottomPanel;
		public ScrollRect contentScrollRect;
		public Scrollbar contentScrollbarVertical;

		[Tooltip("Go to previous question")]
		public GameObject backButton;
		[Tooltip("Go to next question")]
		public GameObject nextButton;

		[Tooltip("Number pad for Num field input")]
		public NumberPad numberPad;

		public List<RectTransform> questionPanels;
		[SerializeField] int currentQuestionIdx; 


		public void ClearQuetionPanels() {
			for(int i = 0; i < questionPanels.Count; i++) {
				Destroy(questionPanels[i]);
			}
			questionPanels.Clear();
			currentQuestionIdx = 0;
		}

		public void ApplySkin()
		{
			if(skinData == null) {
				return;
			}

			TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
			foreach(TMP_Text text in texts) {
				text.font = skinData.font;
				text.color = skinData.normalTextColor;
			}

			Selectable[] selectables = GetComponentsInChildren<Selectable>();
			foreach(Selectable selectable in selectables) {
				selectable.colors = skinData.colorBlock;
				selectable.transition = Selectable.Transition.ColorTint;
			}
			//Image[] backgrounds = GetComponentsInChildren<Image>();
			//foreach(Image bg in backgrounds) {
			//	if(bg.sprite.name == "Background") {
			//		bg.color = skinData.backgroundColor;
			//	}
			//}

		}
		public void OnNextButtonClicked()
		{
			if(currentQuestionIdx < questionPanels.Count - 1) {
				questionPanels[currentQuestionIdx].gameObject.SetActive(false);
				currentQuestionIdx++;
				questionPanels[currentQuestionIdx].gameObject.SetActive(true);
			}
		}

		public void OnBackButtonClicked()
		{
			if(currentQuestionIdx > 0) {
				questionPanels[currentQuestionIdx].gameObject.SetActive(false);
				currentQuestionIdx--;
				questionPanels[currentQuestionIdx].gameObject.SetActive(true);
			}
		}
	}

}

