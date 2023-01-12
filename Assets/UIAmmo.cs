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

        gun.updatedAmmo += (i => ammoUI.text = i.ToString());
        gun.updatedCarriedAmmo += (i => carrierAmmoUI.text = i.ToString());
    }
}
