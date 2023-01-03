using System;

public class KeycardPickable : Pickable
{
    public int code = 0;
    public static event Action pickedKeycard;
    protected override bool OnPickUp(PlayerManager manager)
    {
        manager.keychain.AddCode(code);
        return true;
    }
}
