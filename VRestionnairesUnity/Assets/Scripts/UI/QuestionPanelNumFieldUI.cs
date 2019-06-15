using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelNumFieldUI:QuestionPanelUI, IQuestionPanelUI {

		public TMP_Text instructionsText;
		public TMP_Text idText;
		public TMP_InputField inputField;
		public Button incrementButton;
		public Button decrementButton;

		float inputValue;

		[SerializeField] NumFieldQuestion question;
		public void SetQuestion(Question q)
		{
			question = q as NumFieldQuestion;

			instructionsText.text = question.instructions;
			idText.text = question.id;

			switch(question.datatype) {
			case QuestionDatatype.Integer:
				inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
				break;
			case QuestionDatatype.Float:
				inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
				break;
			}

			incrementButton.gameObject.SetActive(question.spinbutton);
			decrementButton.gameObject.SetActive(question.spinbutton);

			if(question.spinbutton) {
				incrementButton.onClick.AddListener(IncrementValue);
				decrementButton.onClick.AddListener(DecrementValue);
			}

			inputField.onValueChanged.AddListener(OnNumFieldSubmitted);
			inputField.onSubmit.AddListener(OnNumFieldSubmitted);
			inputField.onDeselect.AddListener(OnNumFieldSubmitted);

		}

		void IncrementValue() {
			inputValue++;
			inputValue = Mathf.Clamp(inputValue,question.min,question.max);
			inputField.text = inputValue.ToString(); 
		}

		void DecrementValue()
		{
			inputValue--;
			inputValue = Mathf.Clamp(inputValue,question.min,question.max);
			inputField.text = inputValue.ToString();
		}

		void OnNumFieldSubmitted(string input)
		{
			bool validAnswer = false;
			switch(question.datatype) {
			case QuestionDatatype.Integer:
				int ivalue;
				if(int.TryParse(input, out ivalue)) {
					if(ivalue < question.min) {
						ivalue = (int)question.min;
					}else if(ivalue > question.max) {
						ivalue = (int)question.max;
					}
					validAnswer = true;
					inputField.onValueChanged.RemoveListener(OnNumFieldSubmitted);
					inputField.text = ivalue.ToString();
					inputField.onValueChanged.AddListener(OnNumFieldSubmitted);
				}
				question.answer = ivalue;
				break;
			case QuestionDatatype.Float:
				float fvalue;
				if(float.TryParse(input,out fvalue)) {
					if(fvalue < question.min) {
						fvalue = question.min;
					} else if(fvalue > question.max) {
						fvalue = question.max;
					}
					validAnswer = true;
					inputField.onValueChanged.RemoveListener(OnNumFieldSubmitted);
					inputField.text = fvalue.ToString();
					inputField.onValueChanged.AddListener(OnNumFieldSubmitted);
				}
				question.answer = fvalue;
				break;
			}
			question.isAnswered = validAnswer;
			print(question.id + " " + question.answer);
		}

	}

}

