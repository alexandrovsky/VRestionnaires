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

		//void OnPanelBecameVisible();

		//void OnPanelBecameInVissible();


	}

	public abstract class QuestionPanelUI:MonoBehaviour{

		public UnityAction<Question> OnQuestionAnswered;


		public LayoutElement headerLayout;
		public LayoutElement bodyLayout;

		public TMP_Text instructionsText;
		public TMP_Text idText;

		[HideInInspector]public Question question;


		public void SetQuestionIDVisibility(bool visible)
		{
			idText.gameObject.SetActive(visible);
		}

		public virtual void SetQuestion(Question q, UnityAction<Question> answeredEvent)
		{
			OnQuestionAnswered += answeredEvent;
			question = q;
		}

		public abstract void InitWithAnswer();

		public virtual bool CheckMandatory()
		{
			if(question != null && question.required) {
				return question.isAnswered;
			}else {
				return true;
			}
		}

		public virtual void ShowPanel() {
			gameObject.SetActive(true);
			InitWithAnswer();
		}
		public virtual void HidePanel()
		{
			gameObject.SetActive(false);
		}


	}


}


