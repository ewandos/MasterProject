using TMPro;
using UnityEngine;

public class GlowstickUI : MonoBehaviour
{
    public TextMeshProUGUI glowstickText;

    public void Initialize(GlowstickController glowstickController)
    {
        glowstickText.text = glowstickController.glowstickCount.ToString();
        glowstickController.updatedGlowstickCount += i => glowstickText.text = i.ToString();
    }
}
