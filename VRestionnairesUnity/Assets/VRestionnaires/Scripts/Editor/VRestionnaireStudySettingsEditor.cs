using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//namespace VRestionnaire
//{
//	[CustomEditor(typeof(VRestionnaireStudySettings))]
//	public class VRestionnaireStudySettingsEditor : Editor
//	{
//		public override void OnInspectorGUI()
//		{
//			serializedObject.Update();


//			VRestionnaireStudySettings settings = (VRestionnaireStudySettings)target;
//			var conditions = serializedObject.FindProperty("conditions");
//			EditorGUILayout.PropertyField(conditions, new GUIContent("Conditions"),true);



//			settings.assignRandomCondition = EditorGUILayout.Toggle("Assign Random Condition", settings.assignRandomCondition);
//			settings.generateParticipantId = EditorGUILayout.Toggle("Generate Participant ID",settings.generateParticipantId);


//			EditorGUILayout.Space();
//			settings.loadQuestionnairesFromFiles = EditorGUILayout.Toggle("Load Questionnaire from Files",settings.loadQuestionnairesFromFiles);
//			if(settings.loadQuestionnairesFromFiles) {
//				if(settings.questionnairesFromFiles == null || settings.questionnairesFromFiles.Length != settings.conditions.Length) {
//					settings.questionnairesFromFiles = new VRestionnaireStudySettings.QuestionnaireFromFile[settings.conditions.Length];
//				}

//				for(int i = 0; i < settings.conditions.Length; i++) {
//					GUILayout.BeginHorizontal();
//					settings.questionnairesFromFiles[i].condition = settings.conditions[i];
//					GUILayout.Label(settings.questionnairesFromFiles[i].condition);
//					settings.questionnairesFromFiles[i].filePath = GUILayout.TextField(settings.questionnairesFromFiles[i].filePath);
//					GUILayout.EndHorizontal();
//				}
//			} else {

//			}


//			serializedObject.ApplyModifiedProperties();

//			base.OnInspectorGUI();

//		}
//	}
//}


