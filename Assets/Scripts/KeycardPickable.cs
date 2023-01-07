using System;

public class KeycardPickable : Pickable
{
    public int code = 0;
    protected override bool OnPickUp(PlayerManager manager)
    {
        manager.keychain.AddCode(code);
        GameState.PlayerPickedUpKeycardEvent?.Invoke(true);
        GameState.PlayerPickedUpItem?.Invoke(true);
        return true;
    }
}
