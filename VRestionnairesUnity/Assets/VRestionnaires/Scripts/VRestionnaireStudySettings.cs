using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace VRestionnaire {

	[CreateAssetMenu(menuName = "VRestionnaire Study Settings")]
	public class VRestionnaireStudySettings: ScriptableObject {

		[Serializable]
		public class QuestionnaireFromFile {
			public string condition;
			public string[] filePaths;
			public int currentQuestionnaire;
		}


		//[Serializable]
		//public class QuestionnaireFromEditor {
		//	public string condition;
		//	public Questionnaire questionnaire;
		//}

		[Space]
		public string[] conditions;
		//public bool assignRandomCondition;
		//public bool generateParticipantId;

		[Space]
		//public bool loadQuestionnairesFromFiles;
		public QuestionnaireFromFile[] questionnairesFromFiles;
		//public QuestionnaireFromEditor[] questionnairesFromEditor;

		[Space]
		public string navigationButtonNextLabel = "Weiter";
		public string navigationButtonBackLabel = "Zurück";

		[Space]
		public bool showQuestionId = false;

		[Space]
		public string answersOutputFilePath = "Assets/VRestionnaires/Resources/Answers/";



		public string[] FilePathsForCondition(string conditon) {
			for(int i = 0; i < questionnairesFromFiles.Length; i++) {
				if(questionnairesFromFiles[i].condition == conditon) {
					return questionnairesFromFiles[i].filePaths;
				}
			}
			return null;
		}

		private void OnValidate()
		{
			Array.Resize(ref questionnairesFromFiles, conditions.Length);
			for(int i = 0; i < conditions.Length; i++) {
				
				
				if(questionnairesFromFiles[i] == null) {
					QuestionnaireFromFile fromFile = new QuestionnaireFromFile();
					fromFile.condition = conditions[i];
					fromFile.filePaths = new string[] { };
				} else {
					questionnairesFromFiles[i].condition = conditions[i];
				}
			}
		}

	}

}
