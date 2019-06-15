using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRestionnaire {
	public class QuestionPanelNumFieldUI:QuestionPanelUI, IQuestionPanelUI {

		public TMP_Text instructionsText;
		public TMP_Text idText;
		public TMP_InputField inputField;


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

			inputField.onSubmit.AddListener(OnNumFieldSubmitted);
			inputField.onDeselect.AddListener(OnNumFieldSubmitted);
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
					inputField.text = "" + ivalue;
				}
				question.answer = ivalue;
				break;
			case QuestionDatatype.Float:
				float fvalue;
				if(float.TryParse(input,out fvalue)) {
					if(fvalue < question.min) {
						fvalue = (int)question.min;
					} else if(fvalue > question.max) {
						fvalue = (int)question.max;
					}
					validAnswer = true;
					inputField.text = "" + fvalue;
				}
				question.answer = fvalue;
				break;
			}
			question.isAnswered = validAnswer;
			print(question.id + " " + question.answer);
		}

	}

}

