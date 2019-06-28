using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace VRestionnaire {

	public class QuestionnairePanelUI:MonoBehaviour {

		public delegate void OnQuestionnaireSubmittedEvent(Questionnaire questionnaire);
		public event OnQuestionnaireSubmittedEvent OnQuestionnaireSubmittedCallback;

		public delegate void OnQuestionnaireStartedEvent(Questionnaire questionnaire);
		public event OnQuestionnaireStartedEvent OnQuestionnaireStartedCallback;

		public delegate void OnQuestionnaireFinishedEvent(Questionnaire questionnaire);
		public event OnQuestionnaireFinishedEvent OnQuestionnaireFinishedCallback;


		public int currentQuestionnaireIdx;
		public List<Questionnaire> questionnaires;

		public UISkinData skinData;
		public VRestionnaireStudySettings studySettings;
		public TMP_Text title;
		public TMP_Text instructions;

		public RectTransform topPanel;
		public RectTransform questionsPanel;
		public RectTransform bottomPanel;
		public ScrollRect contentScrollRect;
		public Scrollbar contentScrollbarVertical;


		public string output;

		[Tooltip("Go to previous question")]
		public GameObject backButton;
		public TMP_Text backButtonLabel;
		[Tooltip("Go to next question")]
		public GameObject nextButton;
		public TMP_Text nextButtonLabel;

		public List<QuestionPanelUI> questionPanels;
		[SerializeField] int currentQuestionIdx;


		public void ClearQuestionPanels() {
			for(int i = 0; i < questionPanels.Count; i++) {
				Destroy(questionPanels[i].gameObject);
			}
			questionPanels.Clear();
			currentQuestionIdx = 0;
		}

		private void OnEnable()
		{
			OnQuestionnaireFinishedCallback += QuestionnairePanelUI_OnQuestionnaireFinishedCallback;
		}

		private void OnDisable()
		{
			OnQuestionnaireFinishedCallback -= QuestionnairePanelUI_OnQuestionnaireFinishedCallback;
		}

		private void QuestionnairePanelUI_OnQuestionnaireFinishedCallback(Questionnaire questionnaire)
		{
			questionnaire.endUtcTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			output = VRestionnairePersistence.CreateCSVStringFromQuestionnaire(questionnaire);
			string filename = VRestionnairePersistence.GenerateFilename(questionnaire);
			VRestionnairePersistence.WriteFile(studySettings.answersOutputFilePath,filename,output);

			// append
			VRestionnairePersistence.CreteCSVFromQuestionnaire(studySettings.answersOutputFilePath,questionnaire);
			print(output);
		}

		public void Init()
		{
			//backButton.GetComponent<Button>().interactable = false;
			//nextButton.GetComponent<Button>().interactable = true;
			title.text = questionnaires[currentQuestionnaireIdx].title;
			questionnaires[currentQuestionnaireIdx].startUtcTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			instructions.text = questionnaires[currentQuestionnaireIdx].instructions;
			questionPanels[currentQuestionIdx].ShowPanel(); //.gameObject.SetActive(true);
			nextButtonLabel.text = studySettings.navigationButtonNextLabel;
			backButtonLabel.text = studySettings.navigationButtonBackLabel;

			topPanel.gameObject.SetActive(studySettings.showQuestionnaireHeader);

			CheckNavigationButtons();

			if(OnQuestionnaireStartedCallback != null) {
				OnQuestionnaireStartedCallback.Invoke(questionnaires[currentQuestionnaireIdx]);
			}
		}

		public void ApplySkin()
		{
			if(skinData == null) {
				return;
			}


			ISkinHandler[] handlers = GetComponentsInChildren<ISkinHandler>(true);
			foreach(ISkinHandler handler in handlers) {
				handler.ApplySkin(skinData);
			}

		}
		public void OnNextButtonClicked()
		{
			if(currentQuestionIdx < questionPanels.Count-1) {
				questionPanels[currentQuestionIdx].HidePanel();

				currentQuestionIdx++;
				UpdateQuestionnaireIdxForQuestionIdx(currentQuestionIdx);

				questionPanels[currentQuestionIdx].ShowPanel();
			}
			CheckNavigationButtons();
		}

		public void OnBackButtonClicked()
		{
			if(currentQuestionIdx > 0) {
				questionPanels[currentQuestionIdx].HidePanel();

				currentQuestionIdx--;
				UpdateQuestionnaireIdxForQuestionIdx(currentQuestionIdx);

				questionPanels[currentQuestionIdx].ShowPanel();
			}
			CheckNavigationButtons();
		}

		public void UpdateQuestionnaireIdxForQuestionIdx(int questionIdx)
		{
			int qstnrIdx = 0;
			int acc = 0;
			for(int i = 0; i < questionnaires.Count; i++) {
				acc += questionnaires[i].questions.Length;
				if(questionIdx >= acc) {
					qstnrIdx++;
					if(OnQuestionnaireStartedCallback != null) {
						
					}
					
				} else {
					break;
				}
			}

			if(currentQuestionnaireIdx != qstnrIdx) {
				if(OnQuestionnaireFinishedCallback != null) {
					OnQuestionnaireFinishedCallback.Invoke(questionnaires[currentQuestionnaireIdx]);
				}
				qstnrIdx = Mathf.Clamp(qstnrIdx,0,questionnaires.Count-1);
				currentQuestionnaireIdx = qstnrIdx;
				Init();
			}
		}



		void CheckNavigationButtons()
		{

			QuestionPanelUI panelUI = questionPanels[currentQuestionIdx];
			nextButton.GetComponent<Button>().interactable = panelUI.CheckMandatory();
			backButton.GetComponent<Button>().interactable = true;

			// next button
			if(currentQuestionIdx < questionPanels.Count-1) {
				nextButton.GetComponent<Button>().interactable = panelUI.CheckMandatory();
			} else {
				nextButton.GetComponent<Button>().interactable = false;
			}

			//back button
			if(currentQuestionIdx > 0) {
				backButton.GetComponent<Button>().interactable = true;
			} else {
				backButton.GetComponent<Button>().interactable = false;
			}
		}



		public void OnQuestionAnswered(Question question)
		{
			print(">>>>>>> answered question: " + question.id);
			CheckNavigationButtons();
		}

		public void OnQuestionnaireSubmitted(Question question)
		{
			print(">>>>>>> qstnr submitted: " + question.id);
			CheckNavigationButtons();
			questionnaires[currentQuestionnaireIdx].endUtcTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			if(OnQuestionnaireSubmittedCallback != null) {
				OnQuestionnaireSubmittedCallback.Invoke(this.questionnaires[currentQuestionnaireIdx]);
			}
		}
	}
}