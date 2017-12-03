using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefStringResolver {
    #region Singleton        
    private static PlayerPrefStringResolver _instance;
    public static PlayerPrefStringResolver Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerPrefStringResolver();

            return _instance;
        }
    }
    #endregion

    public string Resolve(string text)
    {
        // todo: use a less hacky way
        var newText = text;
        newText = newText.Replace("${PlayerName}", PlayerPrefs.GetString("PlayerName") ?? "Player");
        newText = newText.Replace("${VillainName}", PlayerPrefs.GetString("VillainName") ?? "Villain");

        return newText;
    }
}
