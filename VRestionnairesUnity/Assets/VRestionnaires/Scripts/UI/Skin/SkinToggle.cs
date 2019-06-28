using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VRestionnaire {
	[RequireComponent(typeof(Toggle))]
	public class SkinToggle:MonoBehaviour, ISkinHandler {
		public void ApplySkin(UISkinData skin)
		{
			Toggle toggle = GetComponent<Toggle>();
			toggle.GetComponent<RectTransform>().sizeDelta = skin.toggleSize;
			toggle.colors = skin.buttonColorBlock;
		}
	}
}