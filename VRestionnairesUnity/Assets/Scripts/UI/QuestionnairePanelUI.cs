using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionnairePanelUI:MonoBehaviour {
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
	}

}

