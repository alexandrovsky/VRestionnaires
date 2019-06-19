using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VRestionnaire {
	public class NumberPad:MonoBehaviour {

		public Button deleteButton;
		public Button confirmButton;

		public delegate void OnNumberSelectedEvent(int number);
		public event OnNumberSelectedEvent OnNumberSelected;

		public delegate void OnDeleteEvent();
		public event OnDeleteEvent OnDelete;

		public delegate void OnConfirmEvent();
		public event OnConfirmEvent OnConfirm;

		public void OnNumberButtonClicked(int value)
		{
			print(value + " clicked");
			OnNumberSelected.Invoke(value);
		}

		public void OnDeleteClicked() {
			OnDelete.Invoke();
		}

		public void OnConfirmClicked() {
			OnConfirm.Invoke();
		}

		
	}
}


