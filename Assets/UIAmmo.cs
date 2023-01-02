using TMPro;
using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    public TextMeshProUGUI ammoUI;
    public TextMeshProUGUI carrierAmmoUI;
    
    public void Initialize(Gun gun)
    {
        ammoUI.text = gun.amunition.ToString();
        carrierAmmoUI.text = gun.amunitionCarried.ToString();

        if(GameSettings.playerStartsWithGun)
            Show();
        else
            Hide();

        gun.toggledGun += b =>
        {
            if (b) Show();
            else Hide();
        };
        
        gun.updatedAmmo += (i => ammoUI.text = i.ToString());
        gun.updatedCarriedAmmo += (i => carrierAmmoUI.text = i.ToString());
    }

    public void Show()
    {
        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void Hide()
    {
        GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
