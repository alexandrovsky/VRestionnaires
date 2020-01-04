using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace VRestionnaire {
	////[CustomEditor(typeof(VRestionnaireStudySettings))]
	//public class VRestionnaireStudySettingsEditor:Editor {
	//	public override void OnInspectorGUI()
	//	{
	//		serializedObject.Update();
	//		VRestionnaireStudySettings settings = (VRestionnaireStudySettings)target;


	//		EditorGUIUtility.LookLikeInspector();
	//		SerializedProperty conditions = serializedObject.FindProperty("conditions");
	//		SerializedProperty questionnairesFromFiles = serializedObject.FindProperty("questionnairesFromFiles");
	//		EditorGUI.BeginChangeCheck();
	//		EditorGUILayout.PropertyField(conditions, true);
	//		//EditorGUILayout.PropertyField(questionnairesFromFiles, conditions.arraySize > 0);
	//		if(conditions.arraySize > 0) {
	//			for(int i = 0; i < questionnairesFromFiles.arraySize; i++) {
	//				SerializedProperty qff = questionnairesFromFiles.GetArrayElementAtIndex(i);
	//				EditorGUILayout.PropertyField(qff, true);
	//				if(GUILayout.Button("Load questionnaires")) {
	//					string path = EditorUtility.OpenFolderPanel("Load questionnaires","/tmp/questionnaires","");
	//					if(path.Length > 0) {
	//						string[] files = Directory.GetFiles(path,"*.json");
	//						settings.questionnairesFromFiles[i].filePaths = files;
	//					}
	//				}
	//			}
	//		}

	//		if(EditorGUI.EndChangeCheck()) {
	//			serializedObject.ApplyModifiedProperties();
	//		}
				
	//		EditorGUIUtility.LookLikeControls();
	//		//base.OnInspectorGUI();
	//	}
	//}
}


