using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace VRestionnaire {
	[RequireComponent(typeof(TMP_Dropdown))]
	public class SkinDropdown:MonoBehaviour, ISkinHandler {

		public void ApplySkin(UISkinData skin)
		{
			TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
			dropdown.colors = skin.buttonColorBlock;
		}
	}

}
