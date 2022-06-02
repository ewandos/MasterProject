using System.Collections.Generic;
using UnityEngine;

public class Keychain : MonoBehaviour
{
    public List<int> keychain = new List<int>();

    public void AddCode(int code)
    {
        keychain.Add(code);
    }

    public bool HasKeyFor(int code)
    {
        return keychain.Contains(code);
    }
}
