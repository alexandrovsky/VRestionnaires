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

		public delegate void OnGroupSelectedDelegate(string qId,int itemId);
		public event OnGroupSelectedDelegate OnGroupSelected;

		public abstract void AddToggle(Toggle toggle);
		public abstract void Init();

		protected void OnToggleValueChanged(bool value)
		{
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
