using System;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class Coin : MonoBehaviour, ISaveable
{
    [SerializeField]
    private int value;

    public Data GetData()
    {
        return CoinData.Create(value);
    }

    public void LoadData(Data dataContainer)
    {
        print("Loading! " + gameObject.name + ": " + value);
        value = ((CoinData)dataContainer).value;
        print("Loaded! " + gameObject.name + ": " + value);
    }
}
[Serializable]
public class CoinData : Data
{
    public int value;
    public CoinData(int val) 
    {   
        value = val;
    }
    public static CoinData Create(int val) => new CoinData(val);
}
