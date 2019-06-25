using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;
using System;
using System.Linq;

namespace VRestionnaire {

	public static class VRestionnaireData {

		public static QuestionDataType DataTypeFromString(string str)
		{
			switch(str) {
			case "boolean":
				return QuestionDataType.Boolean;
			case "integer":
				return QuestionDataType.Integer;
			case "float":
				return QuestionDataType.Float;
			case "string":
				return QuestionDataType.String;
			default:
				return QuestionDataType.Invalid;

			}

		}
	}


	public enum QuestionDataType {
		Invalid,
		Boolean,
		Integer,
		Float,
		String
	}
	public enum QuestionDisplayType {
		Integer,
		Float,
		Percentage
	}

	public enum QuestionType {
		RadioGrid,RadioList, CheckList,
		Slider,Field, NumField,
		MultField, DropDown, TextView,
		Submit
	}

	public static class QuestionTypeNames {
		public const string radiogrid = "radiogrid";
		public const string radiolist = "radiolist";
		public const string checklist = "checklist";
		public const string slider = "slider";
		public const string field = "field";
		public const string num_field = "num_field";
		public const string multi_field = "multi_field";
		public const string drop_down = "drop_down";
		public const string textview = "textview";
	}

	[System.Serializable]
	public class QuestionItem {
		public string id;
		public string text;
		public bool reversed;


	}

	[System.Serializable]
	public class Questionnaire {
		public string condition;
		public string participantId;
		public string title;
		public string instructions;
		public string code;
		public Question[] questions;
		public long startUtcTime;
		public long endUtcTime;

		public Questionnaire()
		{
			startUtcTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}

	}

	#region Different Question Classes

	[System.Serializable]
	public abstract class Question {
		public string id;
		public QuestionType questiontype;
		public string instructions;
		public bool required;
		public bool shuffle;
		public QuestionDataType datatype;
		bool _isAnswered;
		public bool isAnswered {
			get {
				return _isAnswered; }
			set {
				_isAnswered = value;
				answerCounter++;
				answerUtcTime = DateTime.UtcNow;
			}
		}

		public abstract string Export();

		public int answerCounter; // how often was the an answer picked
		public DateTime answerUtcTime;

		public Question(JSONObject json) {
			if(json == null) {
				return; // --->
			} 

			if(json.ContainsKey("id")) {
				id = json["id"].Str;
			}

			if(json.ContainsKey("required")) {
				required = json["required"].Boolean;
			}

			if(json.ContainsKey("shuffle")) {
				shuffle = json["shuffle"].Boolean;
			}

			if(json.ContainsKey("datatype")) {
				datatype = VRestionnaireData.DataTypeFromString(json["datatype"].Str);
			}

			if(json.ContainsKey("instructions")) {
				instructions = json["instructions"].Str;
			}
		}
	}



	[System.Serializable]
	public class RadioGridQuestion:Question {
		public string[] labels;

		public QuestionItem[] q_text;
		public int[] answers;



		public RadioGridQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.RadioGrid;
			datatype = QuestionDataType.Boolean;

			JSONArray labelsJson = json["labels"].Array;
			labels = new string[labelsJson.Length];


			for(int i = 0; i < labelsJson.Length; i++) {
				labels[i] = labelsJson[i].Str;
			}

			JSONArray qTextJson = json["q_text"].Array;
			answers = new int[qTextJson.Length];

			q_text = new QuestionItem[qTextJson.Length];

			for(int i = 0; i < qTextJson.Length; i++) {
				JSONObject obj = qTextJson[i].Obj;
				QuestionItem item = new QuestionItem();
				item.id = obj["id"].Str;
				item.text = obj["text"].Str;
				q_text[i] = item;
				answers[i] = -1;
			}
		}

		public override string Export()
		{
			string output = "[" + String.Join(",",answers.Select(p => p.ToString()).ToArray()) + "]";
			return output;
		}
	}

	[System.Serializable]
	public class RadioListQuestion:Question {

		public string[] labels;
		public bool horizontal;
		public int answer = -1;
		public RadioListQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.RadioList;
			datatype = QuestionDataType.Boolean;
			horizontal = json.ContainsKey("horizontal") ? json["horizontal"].Boolean : false;
			JSONArray labelsArray = json["labels"].Array;
			labels = new string[labelsArray.Length];
			for(int i = 0; i < labels.Length; i++) {
				labels[i] = labelsArray[i].Str;
			}
		}
		public override string Export()
		{
			return isAnswered ? labels[answer] : "-1";
		}
	}

	[System.Serializable]
	public class CheckListQuestion:Question {

		public bool horizontal;
		public QuestionItem[] questions;
		public bool[] answers;

		public CheckListQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.CheckList;
			horizontal = json.ContainsKey("horizontal") ? json["horizontal"].Boolean : false;
			JSONArray qJSON = json["questions"].Array;
			questions = new QuestionItem[qJSON.Length];
			answers = new bool[qJSON.Length];
			for(int i = 0; i < questions.Length; i++) {
				QuestionItem item = new QuestionItem {
					id = qJSON[i].Obj["id"].Str,
					text = qJSON[i].Obj["text"].Str
				};
				questions[i] = item;
			}
		}
		public override string Export()
		{
			List<string> responses = new List<string>();
			for(int i = 0; i < questions.Length; i++) {
				responses.Add(answers[i] ? questions[i].text : "");
			}

			string output = "[" + String.Join(",",responses.Select(s => s).ToArray()) + "]";
			return output;
		}
	}

	[System.Serializable]
	public class SliderQuestion:Question {
		public string left;
		public string right;
		public int tick_count;
		public int width = 400;
		public float answer;

		public SliderQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.Slider;
			datatype = QuestionDataType.Integer;

			if(json.ContainsKey("datatype")) {
				datatype = json["datatype"].Str == "integer" ? QuestionDataType.Integer : QuestionDataType.Float;
			}

			left = json["left"].Str;
			right = json["right"].Str;

			tick_count = (int) json["tick_count"].Number;
		}

		public override string Export()
		{
			if(datatype == QuestionDataType.Float) {
				return answer.ToString();
			} 
			return ((int)answer).ToString();
		}
	}

	[System.Serializable]
	public class FieldQuestion:Question {
		public string placeholder;
		public string answer;

		public FieldQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.Field;
			datatype = QuestionDataType.String;
			placeholder = json["placeholder"].Str;
		}

		public override string Export()
		{
			return answer;
		}
	}

	[System.Serializable]
	public class NumFieldQuestion:Question {
		public QuestionDisplayType displaytype;
		public bool spinbutton;
		public float min;
		public float max;
		public int width = 200;

		public float answer;

		public NumFieldQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.NumField;
			if(json.ContainsKey("spinbutton")) {
				spinbutton = json["spinbutton"].Boolean;
			}

			switch(json["datatype"].Str) {
			case "integer":
				datatype = QuestionDataType.Integer;
				break;
			case "float":
				datatype = QuestionDataType.Float;
				break;
			}
			if(json.ContainsKey("displaytype")) {
				switch(json["displaytype"].Str) {
				case "integer":
					displaytype = QuestionDisplayType.Integer;
					break;
				case "float":
					displaytype = QuestionDisplayType.Float;
					break;
				case "percentage":
					displaytype = QuestionDisplayType.Percentage;
					break;
				}
			}
			if(json.ContainsKey("min")) {
				min = (float) json["min"].Number;
			} else {
				min = float.MinValue;
			}
			if(json.ContainsKey("max")) {
				max = (float)json["max"].Number;
			} else {
				max = float.MaxValue;
			}
		}

		public override string Export()
		{
			if(datatype == QuestionDataType.Float) {
				return answer.ToString();
			}
			return ((int)answer).ToString();
		}
	}

	[System.Serializable]
	public class MultiFieldQuestion:Question {
		public string placeholder;
		public string answer;
		public int height;
		public MultiFieldQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.MultField;
			height = (int)json["height"].Number;
			datatype = QuestionDataType.String;
			placeholder = json["placeholder"].Str;

		}

		public override string Export()
		{
			return answer;
		}
	}

	[System.Serializable]
	public class DropDownQuestion:Question {
		public List<string> items;
		public int width = 200;
		public int dropwidth;
		public int dropheight;
		public string answer;

		public DropDownQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.DropDown;
			JSONArray itemsJson = json["items"].Array;
			items = new List<string>();
			for(int i = 0; i < itemsJson.Length; i++) {
				items.Add(itemsJson[i].Str);
			}
		}

		public override string Export()
		{
			return answer;
		}
	}

	[System.Serializable]
	public class TextViewQuestion:Question {
		public string title;
		public string text;
		public TextViewQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.TextView;
			if(json.ContainsKey("title")) {
				title = json["title"].Str;
			}
			if(json.ContainsKey("text")) {
				text = json["text"].Str;
			}
		}

		public override string Export()
		{
			return "";
		}
	}


	[System.Serializable]
	public class SubmitQuestion:Question {
		public string title;
		public string text;
		public SubmitQuestion() : base(null)
		{
			questiontype = QuestionType.TextView;
		}

		public override string Export()
		{
			return "";
		}
	}

	#endregion

}

