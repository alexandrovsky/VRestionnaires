using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRestionnaire {

	public class VRestionnaireUI:MonoBehaviour {

		public UISkinData skinData;

		protected virtual void OnSkinUI() { 
		}

		protected virtual void Awake()
		{
			OnSkinUI();
		}

	}
}
