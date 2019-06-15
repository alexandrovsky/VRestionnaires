using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelSliderUI:QuestionPanelUI, IQuestionPanelUI {

		[SerializeField] Slider slider;
		[SerializeField] Button incrementValueButton;
		[SerializeField] Button decrementValueButton;
		[SerializeField] TMP_Text minLabel;
		[SerializeField] TMP_Text maxLabel;

		[SerializeField] SliderQuestion question;

		public void SetQuestion(Question q)
		{
			question = q as SliderQuestion;
			minLabel.text = question.left;
			maxLabel.text = question.right;

			slider.minValue = 0;
			slider.maxValue = question.tick_count;
			slider.wholeNumbers = question.datatype == QuestionDatatype.Integer;

			answers = new Dictionary<string,int>();
			answers.Add(question.id,int.MaxValue);
			slider.onValueChanged.AddListener(OnSliderValueChanged);
		}

		void OnSliderValueChanged(float value)
		{
			answers[question.id] = (int)value;
			print(question.id + " " + answers[question.id]);
		}

	}
}


