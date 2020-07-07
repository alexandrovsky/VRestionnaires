# VRestionnaires
A questionnaire tool for VR studies.


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
