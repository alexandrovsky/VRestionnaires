using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace VRestionnaire {

	public class RadioGroup : ItemGroup {


		public RadioGroup(string questionId)
		{
			this.questionId = questionId;
			toggles = new List<Toggle>();

		}

		public override void AddToggle(Toggle toggle)
		{
			if(toggles.Count == 0) {
				toggleGroup = toggle.gameObject.AddComponent<ToggleGroup>();
				toggleGroup.allowSwitchOff = false;
			}
			toggle.group = toggleGroup;
			toggleGroup.RegisterToggle(toggle);
			//toggleGroup.NotifyToggleOn(toggle,false);

			toggles.Add(toggle);
		}


		public override void Init()
		{
			toggleGroup.SetAllTogglesOff();
			foreach(Toggle toggle in toggles) {
				toggle.onValueChanged.AddListener(OnToggleValueChanged);
			}

		}
	}
}