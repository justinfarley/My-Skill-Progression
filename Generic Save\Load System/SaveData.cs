using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SaveData
{
	public Dictionary<string, Data> idToSaveableItems { get; set; } = new Dictionary<string, Data>();

	public SaveData(bool SAVING = false)
	{
        idToSaveableItems.Clear();

		if (!SAVING) return;

        UnityEngine.Object.FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None)
		.Where(x => ((GameObject)x).GetComponent<ISaveable>() != null)
		.ToList()
		.ForEach(saveable =>
		{   
			Debug.Log(((GameObject)saveable).name);
			string id = ((GameObject)saveable).GetComponent<UniqueID>().ID;

			if(!idToSaveableItems.ContainsKey(id))
				idToSaveableItems.Add(id, ((GameObject)saveable).GetComponent<ISaveable>().GetData());
		});
	}
}
