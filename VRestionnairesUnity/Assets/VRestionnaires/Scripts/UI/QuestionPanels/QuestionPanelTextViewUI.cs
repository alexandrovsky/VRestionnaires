using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRestionnaire { 


	public class QuestionPanelTextViewUI : QuestionPanelUI, IQuestionPanelUI
	{
		public TMP_Text text;
		[SerializeField] TextViewQuestion question;
		
		public void SetQuestion(Question q)
		{
			question = q as TextViewQuestion;
			idText.text = question.id;
			text.text = question.text;
			instructionsText.text = question.title;
		}
	}
}