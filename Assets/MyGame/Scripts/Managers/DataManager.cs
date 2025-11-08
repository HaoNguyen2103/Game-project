using UnityEngine;

public static class DataManager
{
    public static int DataCoin
    {
        get => PlayerPrefs.GetInt(Datakey.DataCoin, 0);
        set => PlayerPrefs.SetInt(Datakey.DataCoin, value);
    }

    public static float DataMusic
    {
        get => PlayerPrefs.GetFloat(Datakey.DataMusic, 1f);
        set => PlayerPrefs.SetFloat(Datakey.DataMusic, value);
    }
    public static float DataSfx
    {
        get => PlayerPrefs.GetFloat(Datakey.DataSfx, 1f);
        set => PlayerPrefs.SetFloat(Datakey.DataSfx, value);
    }
}