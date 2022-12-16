using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Gun gun;
    public TextMeshProUGUI ammoUI;
    public TextMeshProUGUI carrierAmmoUI;

    private void Start()
    {
        gun.updatedAmmo += (i => ammoUI.text = i.ToString());
        gun.updatedCarriedAmmo += (i => carrierAmmoUI.text = i.ToString());
    }
}
