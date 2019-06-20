using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelNumFieldUI:QuestionPanelUI, IQuestionPanelUI {

		public TMP_InputField inputField;
		public Button incrementButton;
		public Button decrementButton;

		public NumberPad numberPad;
		NumFieldQuestion numFieldQuestion;

		public override void SetQuestion(Question q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);
			numFieldQuestion = question as NumFieldQuestion;
			instructionsText.text = question.instructions;
			idText.text = question.id;

			switch(question.datatype) {
			case QuestionDataType.Integer:
				inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
				break;
			case QuestionDataType.Float:
				inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
				break;
			}

			incrementButton.gameObject.SetActive(numFieldQuestion.spinbutton);
			decrementButton.gameObject.SetActive(numFieldQuestion.spinbutton);

			if(numFieldQuestion.spinbutton) {
				incrementButton.onClick.AddListener(IncrementValue);
				decrementButton.onClick.AddListener(DecrementValue);
			}

			inputField.onSelect.AddListener(OnFieldSelected);

			inputField.onValueChanged.AddListener(OnNumFieldSubmitted);
			inputField.onSubmit.AddListener(OnNumFieldSubmitted);
			inputField.onDeselect.AddListener(OnNumFieldSubmitted);

			numberPad = FindObjectOfType<QuestionnairePanelUI>().numberPad;
			if(numberPad) {
				numberPad.OnNumberSelected += NumberPad_OnNumberSelected;
				numberPad.OnConfirm += NumberPad_OnConfirm;
				numberPad.OnDelete += NumberPad_OnDelete;
			}
		}

		private void NumberPad_OnDelete()
		{
			inputField.Select();
			inputField.text = "";
		}

		private void NumberPad_OnConfirm()
		{
			OnNumFieldSubmitted(inputField.text);
			numberPad.gameObject.SetActive(false);
		}

		private void NumberPad_OnNumberSelected(int number)
		{
			inputField.text += number.ToString();
		}

		void IncrementValue() {
			NumFieldQuestion numFieldQuestion = question as NumFieldQuestion;
			float inputValue = float.Parse(inputField.text.Length == 0? "0" : inputField.text);
			inputValue++;
			inputValue = Mathf.Clamp(inputValue,numFieldQuestion.min, numFieldQuestion.max);
			inputField.text = inputValue.ToString();
		}

		void DecrementValue()
		{
			NumFieldQuestion numFieldQuestion = question as NumFieldQuestion;
			float inputValue = float.Parse(inputField.text.Length == 0 ? "0" : inputField.text);
			inputValue--;
			inputValue = Mathf.Clamp(inputValue,numFieldQuestion.min,numFieldQuestion.max);
			inputField.text = inputValue.ToString();
		}

		void OnFieldSelected(string str)
		{
			if(numberPad) {
				numberPad.gameObject.SetActive(true);
			}
		}
		void OnNumFieldSubmitted(string input)
		{
			bool validAnswer = false;
			switch(question.datatype) {
			case QuestionDataType.Integer:
				int ivalue;
				if(int.TryParse(input, out ivalue)) {
					if(ivalue < numFieldQuestion.min) {
						ivalue = (int)numFieldQuestion.min;
					}else if(ivalue > numFieldQuestion.max) {
						ivalue = (int)numFieldQuestion.max;
					}
					validAnswer = true;
					inputField.onValueChanged.RemoveListener(OnNumFieldSubmitted);
					inputField.text = ivalue.ToString();
					inputField.onValueChanged.AddListener(OnNumFieldSubmitted);
				}
				numFieldQuestion.answer = ivalue;
				break;
			case QuestionDataType.Float:
				float fvalue;
				if(float.TryParse(input,out fvalue)) {
					if(fvalue < numFieldQuestion.min) {
						fvalue = numFieldQuestion.min;
					} else if(fvalue > numFieldQuestion.max) {
						fvalue = numFieldQuestion.max;
					}
					validAnswer = true;
					inputField.onValueChanged.RemoveListener(OnNumFieldSubmitted);
					inputField.text = fvalue.ToString();
					inputField.onValueChanged.AddListener(OnNumFieldSubmitted);
				}
				numFieldQuestion.answer = fvalue;
				break;
			}
			question.isAnswered = validAnswer;
			print(question.id + " " + numFieldQuestion.answer);
		}

	}

}

