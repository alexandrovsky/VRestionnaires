﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelSliderUI:QuestionPanelUI, IQuestionPanelUI {

		[SerializeField] Slider slider;
		[SerializeField] Button incrementValueButton;
		[SerializeField] Button decrementValueButton;
		[SerializeField] TMP_Text minLabel;
		[SerializeField] TMP_Text maxLabel;

		SliderQuestion sliderQuestion;

		public override void SetQuestion(Question q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);
			sliderQuestion = question as SliderQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			minLabel.text = sliderQuestion.left;
			maxLabel.text = sliderQuestion.right;

			slider.minValue = 0;
			slider.maxValue = sliderQuestion.tick_count;
			slider.wholeNumbers = question.datatype == QuestionDataType.Integer;

			slider.onValueChanged.AddListener(OnSliderValueChanged);
			incrementValueButton.onClick.AddListener(IncrementValue);
			decrementValueButton.onClick.AddListener(DecrementValue);
		}


		void OnSliderValueChanged(float value)
		{
			question.isAnswered = true;
			sliderQuestion.answer = value;
			print(question.id + " " + sliderQuestion.answer);
			OnQuestionAnswered.Invoke(question);
		}


		void IncrementValue()
		{
			slider.value++;
			slider.value = Mathf.Clamp(slider.value, 0,sliderQuestion.tick_count);
			
		}

		void DecrementValue()
		{
			slider.value--;
			slider.value = Mathf.Clamp(slider.value, 0,sliderQuestion.tick_count);
		}

	}
}


