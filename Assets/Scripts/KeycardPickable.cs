public class KeycardPickable : Pickable
{
    public int code = 0;

    protected override void OnPickUp(PlayerManager manager)
    {
        manager.keychain.AddCode(code);
    }
}
