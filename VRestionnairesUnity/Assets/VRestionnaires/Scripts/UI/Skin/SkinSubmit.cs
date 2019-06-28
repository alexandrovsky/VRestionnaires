using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VRestionnaire {
	[RequireComponent(typeof(Button))]
	public class SkinSubmit:MonoBehaviour, ISkinHandler {
		public void ApplySkin(UISkinData skin)
		{
			Button btn = GetComponent<Button>();
			btn.colors = skin.submitColorBlock;
		}
	}
}


