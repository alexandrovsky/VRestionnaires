using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.FB;
using System.IO;
using Boomlagoon.JSON;
using CGTespy.UI;

namespace VRestionnaire {

	[System.Serializable] public class QuestionTypePrefabsDictionary:SerializableDictionary<QuestionType,GameObject> { }

	public class VRestionnaireFactory:MonoBehaviour {
		public string dir = "/Users/dmitryalexandrovsky/development/bride-of-frankensystem_old/examples/CatapultKings/app/questionnaires";

		static string[] fileExtensions = { "json" };
		//public GameObject questionnairePanelPrefab;
		public RectTransform questionnaireParent;


		public Questionnaire questionnaire;

		public QuestionTypePrefabsDictionary questionTypePrefabsDict;


		void Start()
		{
			BuildDefault();
		}

		public void BuildDefault() {
			string[] filenames = { dir + "/example.json" };
			OpenAction(filenames);
		}


		public void OnFileOpenClicked()
		{
			FileBrowser.OpenFilesAsync(OpenAction,"open questionnaire file",dir,false,fileExtensions);
		}

		void OpenAction(string[] filenames)
		{
			foreach(string filename in filenames) {
				print(filename);
				string file = File.ReadAllText(filename);
				JSONObject json = JSONObject.Parse(file);

				GenerateQuestionnaire(json);
				GenerateQuestionnaireUI();
			}
		}

		void GenerateQuestionnaireUI()
		{
			//RectTransform panel = Instantiate(questionnairePanelPrefab).GetComponent<RectTransform>();
			//QuestionnairePanelUI questionnairePanelUI = panel.GetComponent<QuestionnairePanelUI>();
			//panel.parent = questionnaireParent;
			//panel.ApplyAnchorPreset(TextAnchor.MiddleCenter,true,true);
			//panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,questionnaireParent.Width());
			//panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,questionnaireParent.Height());

			foreach(Question question in questionnaire.questions) {
				if(questionTypePrefabsDict.ContainsKey(question.questiontype)){
					if(questionTypePrefabsDict[question.questiontype] != null) {
						GameObject questionPanel = Instantiate(questionTypePrefabsDict[question.questiontype]);
						RectTransform questionPanelRT = questionPanel.GetComponent<RectTransform>();
						questionPanelRT.parent = questionnaireParent;
						questionPanelRT.SetAnchor(AnchorPresets.StretchAll);
						questionPanelRT.localPosition = Vector3.zero;
						//questionPanel.transform.parent = questionnaireParent; // questionnairePanelUI.questionsPanel;

						IQuestionPanelUI panelUI = questionPanel.GetComponent<IQuestionPanelUI>();
						print(panelUI);

						//if(question is RadioGridQuestion) {
						//} else if(question is RadioListQuestion) {
						//} else if(question is CheckListQuestion) {
						//} else if(question is SliderQuestion) {
						//} else if(question is FieldQuestion) {
						//} else if(question is NumFieldQuestion) {
						//} else if(question is MultiFieldQuestion) {
						//} else if(question is DropDownQuestion) {
						//} else if(question is TextViewQuestion) {
						//}

						panelUI.SetQuestion(question);
					}
				}
			}
			//RectTransform firstChild = questionnairePanelUI.GetComponentsInChildren<QuestionPanelUI>()[0].GetComponent<RectTransform>();
			//questionnairePanelUI.contentScrollRect.content.localPosition =
			//questionnairePanelUI.contentScrollRect.GetSnapToPositionToBringChildIntoView(firstChild);

			//Canvas.ForceUpdateCanvases();
			//questionnairePanelUI.contentScrollbarVertical.value = 1;
			//Canvas.ForceUpdateCanvases();

			//questionnairePanelUI.ApplySkin();

		}

		void GenerateQuestionnaire(JSONObject json)
		{

			questionnaire = new Questionnaire();

			foreach(KeyValuePair<string,JSONValue> pair in json) {
				switch(pair.Key) {
				case "title":
					questionnaire.title = pair.Value.Str;
					break;
				case "instructions":
					questionnaire.instructions = pair.Value.Str;
					break;
				case "code":
					questionnaire.code = pair.Value.Str;
					break;
				case "questions":
					JSONArray questionsJson = pair.Value.Array;
					questionnaire.questions = new Question[questionsJson.Length];
					for(int i = 0; i < questionsJson.Length; i++) {
						questionnaire.questions[i] = GenerateQuestion(questionsJson[i].Obj);
					}
					break;
				}
				Debug.Log("key : value -> " + pair.Key + " : " + pair.Value);
			}
			print(questionnaire);
		}

		Question GenerateQuestion(JSONObject json)
		{
			print("question:" + json.ToString());
			string questiontype = json["questiontype"].Str;

			Question question = null;

			switch(questiontype) {
			case QuestionTypeNames.radiogrid:
				question = new RadioGridQuestion(json);
				break;
			case QuestionTypeNames.radiolist:
				question = new RadioListQuestion(json);
				break;
			case QuestionTypeNames.checklist:
				question = new CheckListQuestion(json);
				break;
			case QuestionTypeNames.slider:
				question = new SliderQuestion(json);
				break;
			case QuestionTypeNames.field:
				question = new FieldQuestion(json);
				break;
			case QuestionTypeNames.num_field:
				question = new NumFieldQuestion(json);
				break;
			case QuestionTypeNames.multi_field:
				question = new MultiFieldQuestion(json);
				break;
			case QuestionTypeNames.drop_down:
				question = new DropDownQuestion(json);
				break;
			case QuestionTypeNames.textview:
				question = new TextViewQuestion(json);
				break;
			default:
				question = new Question(json);
				break;
			}
			return question;
		}


		//public void OnValidate()
		//{
		//	BuildDefault();
		//}

	}
}



