using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

namespace VRestionnaire {



	public static class VRestionnaireData {

		public static QuestionDatatype DataTypeFromString(string str)
		{
			switch(str) {
			case "boolean":
				return QuestionDatatype.Boolean;
			case "integer":
				return QuestionDatatype.Integer;
			case "float":
				return QuestionDatatype.Float;
			case "string":
				return QuestionDatatype.String;
			default:
				return QuestionDatatype.Invalid;

			}

		}
	}





	public enum QuestionDatatype {
		Invalid,
		Boolean,
		Integer,
		Float,
		String
	}
	public enum QuestionDisplaytype {
		Integer,
		Float,
		Percentage
	}

	public enum QuestionType {
		RadioGrid,RadioList, CheckList,
		Slider,Field, NumField,
		MultField, DropDown, TextView
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
		public string title;
		public string instructions;
		public string code;
		public Question[] questions;

	}

	#region Different Question Classes

	[System.Serializable]
	public class Question {
		public string id;
		public QuestionType questiontype;
		public string instructions;
		public bool required;
		public bool shuffle;
		public QuestionDatatype datatype;

		public Question(JSONObject json) {
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


		public RadioGridQuestion(JSONObject json) : base(json)
		{	
			questiontype = QuestionType.RadioGrid;
			datatype = QuestionDatatype.Boolean;

			JSONArray labelsJson = json["labels"].Array;
			labels = new string[labelsJson.Length];

			for(int i = 0; i < labelsJson.Length; i++) {
				labels[i] = labelsJson[i].Str;
			}

			JSONArray qTextJson = json["q_text"].Array;
			q_text = new QuestionItem[qTextJson.Length];

			for(int i = 0; i < qTextJson.Length; i++) {
				JSONObject obj = qTextJson[i].Obj;
				QuestionItem item = new QuestionItem();
				item.id = obj["id"].Str;
				item.text = obj["text"].Str;
				q_text[i] = item;
			}
		}
	}

	[System.Serializable]
	public class RadioListQuestion:Question {

		public string[] labels;
		public bool horizontal;

		public RadioListQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.RadioList;
			datatype = QuestionDatatype.Boolean;
			horizontal = json["horizontal"].Boolean;
			JSONArray labelsArray = json["labels"].Array;
			labels = new string[labelsArray.Length];
			for(int i = 0; i < labels.Length; i++) {
				labels[i] = labelsArray[i].Str;
			}
		}
	}

	[System.Serializable]
	public class CheckListQuestion:Question {

		public string[] labels;
		bool horizontal;
		public QuestionItem[] questions;

		public CheckListQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.CheckList;
		}
	}

	[System.Serializable]
	public class SliderQuestion:Question {
		public string left;
		public string right;
		public int tick_count;
		public int width = 400;

		public SliderQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.Slider;
		}
	}

	[System.Serializable]
	public class FieldQuestion:Question {
		public string placeholder;


		public FieldQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.Field;
			datatype = QuestionDatatype.String;
		}
	}

	[System.Serializable]
	public class NumFieldQuestion:Question {
		public QuestionDisplaytype displaytype;
		public bool spinbutton;
		public float min;
		public float max;
		public int width = 200;

		public NumFieldQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.NumField;
		}
	}

	[System.Serializable]
	public class MultiFieldQuestion:FieldQuestion {
		public int height;
		public MultiFieldQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.MultField;
		}
	}

	[System.Serializable]
	public class DropDownQuestion:Question {
		public string[] items;
		public int width = 200;
		public int dropwidth;
		public int dropheight;

		public DropDownQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.DropDown;
		}
	}

	[System.Serializable]
	public class TextViewQuestion:Question {
		public string title;
		public string text;
		public TextViewQuestion(JSONObject json) : base(json)
		{
			questiontype = QuestionType.TextView;
			title = json["title"].Str;
			text = json["text"].Str;
		}
	}
	#endregion

}

