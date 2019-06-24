using System.Collections.Generic;
using UnityEngine.UI;

public static class ToggleGroupExtensions {

	private static System.Reflection.FieldInfo _toggleListMember;

	/// <summary>
	/// Gets the list of toggles. Do NOT add to the list, only read from it.
	/// </summary>
	/// <param name="grp"></param>
	/// <returns></returns>
	public static IList<Toggle> GetToggles(this ToggleGroup grp)
	{
		if(_toggleListMember == null) {
			_toggleListMember = typeof(ToggleGroup).GetField("m_Toggles",System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			if(_toggleListMember == null)
				throw new System.Exception("UnityEngine.UI.ToggleGroup source code must have changed in latest version and is no longer compatible with this version of code.");
		}
		return _toggleListMember.GetValue(grp) as IList<Toggle>;
	}

	public static int Count(this ToggleGroup grp)
	{
		return GetToggles(grp).Count;
	}

	public static Toggle Get(this ToggleGroup grp,int index)
	{
		return GetToggles(grp)[index];
	}

}
