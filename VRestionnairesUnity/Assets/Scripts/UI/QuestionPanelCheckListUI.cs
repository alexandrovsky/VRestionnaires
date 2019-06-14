using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelCheckListUI:QuestionPanelUI, IQuestionPanelUI {

		public RectTransform itemsUI;
		public VariableGridLayoutGroup gridLayout;

		public TMP_Text instructionsText;
		public TMP_Text idText;

		public GameObject labeledRadioItemPrefab;


		[SerializeField] RadioListQuestion question;
		RadioGroup radioGroup;


		public void SetQuestion(Question q)
		{

		}
	}
}



