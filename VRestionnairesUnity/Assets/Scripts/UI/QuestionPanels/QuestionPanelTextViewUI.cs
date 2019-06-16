using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRestionnaire { 


	public class QuestionPanelTextViewUI : MonoBehaviour, IQuestionPanelUI
	{
		public TMP_Text title;
		public TMP_Text text;

		[SerializeField] TextViewQuestion question;


		public void SetQuestion(Question q)
		{
			question = q as TextViewQuestion;
			title.text = question.title;
			text.text = question.text;

			
		}
	}
}