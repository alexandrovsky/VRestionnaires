using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace VRestionnaire { 


	public class QuestionPanelTextViewUI : QuestionPanelUI<TextViewQuestion>, IQuestionPanelUI
	{
		public TMP_Text text;

		public override void SetQuestion(TextViewQuestion q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q, answeredEvent);

			idText.text = question.id;
			text.text = question.text;
			instructionsText.text = question.title;
			question.isAnswered = true;
		}
	}
}