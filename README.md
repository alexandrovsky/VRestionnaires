# VRestionnaires
A questionnaire tool for VR studies.
[[pics/VRestionnairesTool.jpg]]

## Usage
 - Drag the `VRESTIONNAIRES` or `VRESTIONNAIRES_WORLD` Prefab into your scene.
 - Select the `VRestionnaireFactory` component and adjust the parameters.
  - `Dir` root directory of the used questionnaires
  - `Questionnaire_filename`if no other settings set this is the default json file to generate the UI from
  - `QuestionParent` the parent canvas of all question items. By default its pointing to the canvas of the prefab.
  - `Question Type Prefabs` Each question type can be modified. Look at the individual question prefabs how they are impelmented.
 - `Study Settings` A `VRestionnaireStudySettings` scriptable object for the settings of the study flow
 
 ### VRestionnaireStudySettings
 Here you can set the names of the conditions. Then, for each condition a list of questionnaires can be set.
 Further, the settings allow for customized button labels, display options and the output path for the responses.





If you decide to implement VRestionnaires in user studies, plese refere to our publications


*Susanne Putze, Dmitry Alexandrovsky, Felix Putze, Sebastian Höffner, Jan David Smeddinck, and Rainer Malaka. 2020. Breaking The Experience: Effects of Questionnaires in VR User Studies. In Proceedings of the 2020 CHI Conference on Human Factors in Computing Systems (CHI ’20). Association for Computing Machinery, New York, NY, USA, 1–15. DOI:https://doi.org/10.1145/3313831.3376144*

*Dmitry Alexandrovsky, Susanne Putze, Michael Bonfert, Sebastian Höffner, Pitt Michelmann, Dirk Wenig, Rainer Malaka, and Jan David Smeddinck. 2020. Examining Design Choices of Questionnaires in VR User Studies. In Proceedings of the 2020 CHI Conference on Human Factors in Computing Systems (CHI ’20). Association for Computing Machinery, New York, NY, USA, 1–21. DOI:https://doi.org/10.1145/3313831.3376260*
