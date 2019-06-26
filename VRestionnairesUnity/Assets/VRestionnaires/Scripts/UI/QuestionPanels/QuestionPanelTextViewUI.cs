using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire {


	public class QuestionPanelTextViewUI : QuestionPanelUI, IQuestionPanelUI
	{
		public TMP_Text text;
		TextViewQuestion textViewQuestion;

		public override void InitWithAnswer()
		{

		}

		public override void SetQuestion(Question q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);

			textViewQuestion = question as TextViewQuestion;

			idText.text = question.id;
			text.text = textViewQuestion.text;
			instructionsText.text = textViewQuestion.title;
			question.isAnswered = true;
		}
	}
}