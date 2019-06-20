using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

namespace VRestionnaire {
	public interface IQuestionPanelUI {
		//void SetQuestion(Question q);
		//bool CheckMandatory();
	}

	public abstract class QuestionPanelUI:MonoBehaviour{

		public UnityAction<Question> OnQuestionAnswered;


		public LayoutElement headerLayout;
		public LayoutElement bodyLayout;

		public TMP_Text instructionsText;
		public TMP_Text idText;

		public Question question;


		public virtual void SetQuestion(Question q, UnityAction<Question> answeredEvent)
		{
			OnQuestionAnswered += answeredEvent;
			question = q;
		}

		public virtual bool CheckMandatory()
		{
			if(question.required) {
				return question.isAnswered;
			}else {
				return true;
			}
		}
	}


}


