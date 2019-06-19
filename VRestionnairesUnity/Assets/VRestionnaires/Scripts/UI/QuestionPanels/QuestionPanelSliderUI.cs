using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelSliderUI:QuestionPanelUI<SliderQuestion>, IQuestionPanelUI {

		[SerializeField] Slider slider;
		[SerializeField] Button incrementValueButton;
		[SerializeField] Button decrementValueButton;
		[SerializeField] TMP_Text minLabel;
		[SerializeField] TMP_Text maxLabel;


		public override void SetQuestion(SliderQuestion q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);

			instructionsText.text = question.instructions;
			idText.text = question.id;

			minLabel.text = question.left;
			maxLabel.text = question.right;

			slider.minValue = 0;
			slider.maxValue = question.tick_count;
			slider.wholeNumbers = question.datatype == QuestionDataType.Integer;

			slider.onValueChanged.AddListener(OnSliderValueChanged);
			incrementValueButton.onClick.AddListener(IncrementValue);
			decrementValueButton.onClick.AddListener(DecrementValue);
		}


		void OnSliderValueChanged(float value)
		{
			question.isAnswered = true;
			question.answer = value;
			print(question.id + " " + question.answer);
			OnQuestionAnswered.Invoke(question);
		}


		void IncrementValue()
		{
			slider.value++;
			slider.value = Mathf.Clamp(slider.value, 0, question.tick_count);
			
		}

		void DecrementValue()
		{
			slider.value--;
			slider.value = Mathf.Clamp(slider.value, 0,question.tick_count);
		}

	}
}


