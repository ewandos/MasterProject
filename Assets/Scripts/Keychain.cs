using System;
using System.Collections.Generic;
using UnityEngine;

public class Keychain : MonoBehaviour
{
    public GameSettings gameSettings;
    public bool hasMasterKey = false;
    public List<int> keychain = new List<int>();

    public void Start()
    {
        hasMasterKey = gameSettings.playerHasMasterKey;
    }

    public void AddCode(int code)
    {
        keychain.Add(code);
    }

    public bool HasKeyFor(int code)
    {
        return keychain.Contains(code);
    }

    public void RemoveCode(int code)
    {
        keychain.Remove(code);
    }

    public int GetKeycardCount()
    {
        return keychain.Count;
    }

    public void RemoveNumberOfCodes(int count)
    {
        keychain.RemoveRange(0, count);
    }
}
