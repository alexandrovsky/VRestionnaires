using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionPanelNumFieldUI:QuestionPanelUI<NumFieldQuestion>, IQuestionPanelUI {

		public TMP_InputField inputField;
		public Button incrementButton;
		public Button decrementButton;

		public NumberPad numberPad;


		public override void SetQuestion(NumFieldQuestion q, UnityAction<Question> answeredEvent)
		{
			base.SetQuestion(q,answeredEvent);

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

			incrementButton.gameObject.SetActive(question.spinbutton);
			decrementButton.gameObject.SetActive(question.spinbutton);

			if(question.spinbutton) {
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
			float inputValue = float.Parse(inputField.text.Length == 0? "0" : inputField.text);
			inputValue++;
			inputValue = Mathf.Clamp(inputValue,question.min,question.max);
			inputField.text = inputValue.ToString(); 
		}

		void DecrementValue()
		{
			float inputValue = float.Parse(inputField.text.Length == 0 ? "0" : inputField.text);
			inputValue--;
			inputValue = Mathf.Clamp(inputValue,question.min,question.max);
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
			case QuestionDataType.Float:
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

