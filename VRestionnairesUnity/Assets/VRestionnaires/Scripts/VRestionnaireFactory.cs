using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.FB;
using System.IO;
using Boomlagoon.JSON;
using CGTespy.UI;
using System;

namespace VRestionnaire {

	//[System.Serializable] public class QuestionTypePrefabsDictionary:SerializableDictionary<QuestionType,GameObject> { }

	[System.Serializable] public class QuestionTypePrefab {
		public QuestionType questionType;
		public GameObject prefab;
	}


	public class VRestionnaireFactory:MonoBehaviour {
		public string dir = "/Users/dmitryalexandrovsky/development/bride-of-frankensystem_old/examples/CatapultKings/app/questionnaires";

		static string[] fileExtensions = { "json" };
		//public GameObject questionnairePanelPrefab;
		public RectTransform questionParent;
		public QuestionnairePanelUI questionnairePanel;

		public Questionnaire questionnaire;

		public List<QuestionTypePrefab> questionTypePrefabs;
		

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

		void GenerateQuestionnaireUI()
		{	
			foreach(Question question in questionnaire.questions) {
				if(ContainsQuestionType(question.questiontype)){
					GameObject prefab = ObjectForQuestionType(question.questiontype);
					if(prefab != null) {
						GameObject questionPanel = Instantiate(prefab);
						RectTransform questionPanelRT = questionPanel.GetComponent<RectTransform>();
						questionPanelRT.parent = questionParent;
						questionPanelRT.SetAnchor(AnchorPresets.StretchAll);
						questionPanelRT.localPosition = Vector3.zero;
						//questionPanel.transform.parent = questionnaireParent; // questionnairePanelUI.questionsPanel;
						

						if(question is RadioGridQuestion) {
							QuestionPanelUI<RadioGridQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<RadioGridQuestion>>();
							panelUI.SetQuestion(question as RadioGridQuestion, questionnairePanel.OnQuestionAnswered);
							
						} else if(question is RadioListQuestion) {
							QuestionPanelUI<RadioListQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<RadioListQuestion>>();
							panelUI.SetQuestion(question as RadioListQuestion,questionnairePanel.OnQuestionAnswered);
						} else if(question is CheckListQuestion) {
							QuestionPanelUI<CheckListQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<CheckListQuestion>>();
							panelUI.SetQuestion(question as CheckListQuestion,questionnairePanel.OnQuestionAnswered);
						} else if(question is SliderQuestion) {
							QuestionPanelUI<SliderQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<SliderQuestion>>();
							panelUI.SetQuestion(question as SliderQuestion,questionnairePanel.OnQuestionAnswered);
						} else if(question is FieldQuestion) {
							QuestionPanelUI<FieldQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<FieldQuestion>>();
							panelUI.SetQuestion(question as FieldQuestion,questionnairePanel.OnQuestionAnswered);
						} else if(question is NumFieldQuestion) {
							QuestionPanelUI<NumFieldQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<NumFieldQuestion>>();
							panelUI.SetQuestion(question as NumFieldQuestion,questionnairePanel.OnQuestionAnswered);
						} else if(question is MultiFieldQuestion) {
							QuestionPanelUI<MultiFieldQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<MultiFieldQuestion>>();
							panelUI.SetQuestion(question as MultiFieldQuestion,questionnairePanel.OnQuestionAnswered);
						} else if(question is DropDownQuestion) {
							QuestionPanelUI<DropDownQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<DropDownQuestion>>();
							panelUI.SetQuestion(question as DropDownQuestion,questionnairePanel.OnQuestionAnswered);
						} else if(question is TextViewQuestion) {
							QuestionPanelUI<TextViewQuestion> panelUI = questionPanel.GetComponent<QuestionPanelUI<TextViewQuestion>>();
							panelUI.SetQuestion(question as TextViewQuestion,questionnairePanel.OnQuestionAnswered);
						}

						questionnairePanel.questionPanels.Add(questionPanelRT);
						questionPanelRT.gameObject.SetActive(false);
					}
				}
			}
			questionnairePanel.Init();

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



