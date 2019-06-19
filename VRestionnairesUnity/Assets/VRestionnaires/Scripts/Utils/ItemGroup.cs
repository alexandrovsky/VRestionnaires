using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace VRestionnaire {
	public abstract class ItemGroup {

		public List<Toggle> toggles;
		public string questionId;
		public ToggleGroup toggleGroup;

		bool allowSwitchOff;
		public delegate void OnGroupSelectedDelegate(string qId,int itemId);
		public event OnGroupSelectedDelegate OnGroupSelected;


		protected ItemGroup(string questionId, bool allowSwitchOff)
		{
			this.questionId = questionId;
			this.allowSwitchOff = allowSwitchOff;

			toggles = new List<Toggle>();
		}

		public virtual void AddToggle(Toggle toggle)
		{
			if(toggles.Count == 0) {
				toggleGroup = toggle.gameObject.AddComponent<ToggleGroup>();
				toggleGroup.allowSwitchOff = true; // allowSwitchOff;
			}
			toggle.group = toggleGroup;
			toggleGroup.RegisterToggle(toggle);
			//toggleGroup.NotifyToggleOn(toggle,false);

			toggles.Add(toggle);
		}


		public virtual void Init()
		{
			toggleGroup.SetAllTogglesOff();
			foreach(Toggle toggle in toggles) {
				toggle.onValueChanged.AddListener(OnToggleValueChanged);
			}
		}


		public virtual void OnToggleValueChanged(bool value)
		{
			toggleGroup.allowSwitchOff = this.allowSwitchOff;
 
			List<Toggle> active = toggleGroup.ActiveToggles().ToList();
			int idx = -1;
			if(active.Count == 1) {
				idx = toggles.IndexOf(active[0]);
				Debug.Log("active toggles " + active.Count + " itemId: " + idx);
				OnGroupSelected.Invoke(questionId,idx);
			} else if(active.Count == 0) {
				OnGroupSelected.Invoke(questionId,idx);
			} else {
				Debug.LogError("Error! multiple items are selected in a radio group");
			}
		}

	}
}
