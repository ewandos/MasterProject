using UnityEngine;
using UnityEngine.UI;


namespace Manager
{
    
    public class SoundManager: MonoBehaviour
    {
        [SerializeField] Slider volumeSlider;


        private void Start()
        {
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
                Load();
            }
            else
            {
                Load();
            }
        }
        
        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
        }

        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    }
}