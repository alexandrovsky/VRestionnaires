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


		[Tooltip("go to next page")]
		public Button nextButton;
		[Tooltip("go to previous page")]
		public Button prevButton;


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
	}

}

