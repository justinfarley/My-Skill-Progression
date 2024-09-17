using RedLobsterStudios.Util;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not sure what to do with this yet... need for saving not sure what else
/// </summary>
public class ChoiceManager : MonoSingleton<ChoiceManager>, ISaveable
{
    List<IChoice> choices = new List<IChoice>();
    public Data GetData()
    {
        return new ChoiceData();
    }

    public void LoadData(Data data)
    {
        return;
    }

    public class ChoiceData : Data
    {
    }
}
