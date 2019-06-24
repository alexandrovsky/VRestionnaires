﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.FB;
using System.IO;
using Boomlagoon.JSON;
using CGTespy.UI;
using System.Linq;

namespace VRestionnaire {

	//[System.Serializable] public class QuestionTypePrefabsDictionary:SerializableDictionary<QuestionType,GameObject> { }

	[System.Serializable] public class QuestionTypePrefab {
		public QuestionType questionType;
		public GameObject prefab;
	}


	public class VRestionnaireFactory:MonoBehaviour {
		public string dir = "Assets/VRestionnaires/Resources/Questions";
		public string questionnaire_filename = "/example.json";

		static string[] fileExtensions = { "json" };
		
		public RectTransform questionParent;
		public QuestionnairePanelUI questionnairePanel;

		public List<QuestionTypePrefab> questionTypePrefabs;

		public VRestionnaireStudySettings studySettings;

		void Start()
		{
			BuildDefault();
		}

		public void BuildFromStudySettings(string condition, string participantId) {
			string[] filenames = studySettings.FilePathsForCondition(condition);
			OpenAction(filenames);


		}

		public void BuildDefault() {
			string[] filenames = { dir + questionnaire_filename };
			OpenAction(filenames);
		}


		public void OnFileOpenClicked()
		{
			FileBrowser.OpenFilesAsync(OpenAction,"open questionnaire file",dir,false,fileExtensions);
		}

		void OpenAction(string[] filenames)
		{
			questionnairePanel.questionnaires = new List<Questionnaire>();

			for(int i = 0; i < filenames.Length; i++) {
				string filename = filenames[i];
				print("open: " + filename);
				string file = File.ReadAllText(filename);
				JSONObject json = JSONObject.Parse(file);

				questionnairePanel.questionnaires.Add( GenerateQuestionnaire(json) );
				GenerateQuestionnaireUI(questionnairePanel.questionnaires.Last());
			}
			GenerateSubmitQuestionnaireUI();
		}

		bool ContainsQuestionType(QuestionType type)
		{
			for(int i = 0; i < questionTypePrefabs.Count; i++) {
				if(questionTypePrefabs[i].questionType == type) {
					return true;
				}
			}
			return false;
		}

		GameObject ObjectForQuestionType(QuestionType type)
		{
			for(int i = 0; i < questionTypePrefabs.Count; i++) {
				if(questionTypePrefabs[i].questionType == type) {
					return questionTypePrefabs[i].prefab;
				}
			}
			return null;
		}

		QuestionPanelUI GeneratePanelForQuestionType(QuestionType questionType)
		{

			if(ContainsQuestionType(questionType)) {
				GameObject prefab = ObjectForQuestionType(questionType);
				if(prefab != null) {
					GameObject questionPanel = Instantiate(prefab);
					RectTransform questionPanelRT = questionPanel.GetComponent<RectTransform>();
					questionPanelRT.parent = questionParent;
					questionPanelRT.SetAnchor(AnchorPresets.StretchAll);
					questionPanelRT.localPosition = Vector3.zero;
					questionPanelRT.localRotation = Quaternion.identity;
					questionPanelRT.localScale = questionParent.localScale;
					//questionPanel.transform.parent = questionnaireParent; // questionnairePanelUI.questionsPanel;

					return questionPanel.GetComponent<QuestionPanelUI>();
				}
			}
			return null;
		}


		void GenerateSubmitQuestionnaireUI() {

			QuestionPanelUI submitUI = GeneratePanelForQuestionType(QuestionType.Submit);
			submitUI.SetQuestion(submitUI.question,questionnairePanel.OnQuestionnaireSubmitted);
			questionnairePanel.questionPanels.Add(submitUI);
			submitUI.HidePanel();
		}

		void GenerateQuestionnaireUI(Questionnaire questionnaire)
		{
			// TODO: Clear UI
			foreach(Question question in questionnaire.questions) {
				QuestionPanelUI panelUI = GeneratePanelForQuestionType(question.questiontype);
				if(panelUI != null) {
					panelUI.SetQuestion(question, questionnairePanel.OnQuestionAnswered);

					questionnairePanel.questionPanels.Add(panelUI);
					panelUI.HidePanel();
				}
			}
			questionnairePanel.Init();
			


			//Canvas.ForceUpdateCanvases();
			//questionnairePanelUI.contentScrollbarVertical.value = 1;
			//Canvas.ForceUpdateCanvases();

			//questionnairePanelUI.ApplySkin();

		}

		Questionnaire GenerateQuestionnaire(JSONObject json)
		{
			Questionnaire questionnaire = new Questionnaire();

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
			return questionnaire;
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



