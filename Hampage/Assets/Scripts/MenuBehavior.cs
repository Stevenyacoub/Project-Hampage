using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuBehavior : MonoBehaviour
{
      [Header("Video Settings")]
      [SerializeField] private Slider brightnessSlider = null;
      [SerializeField] private TMP_Text brightnessValue = null;
      [SerializeField] private float defaultBrightness = 1;

      public TMP_Dropdown resolutionDropDown;
      private Resolution[] resolutions;
      
      private bool _isFullScreen;
      private float _brightnessLevel;

      public void Start(){
            resolutions = Screen.resolutions;
            resolutionDropDown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for(int i = 0; i < resolutions.Length; i++)
            {
                  string option = resolutions[i].width + "x" + resolutions[i].height;
                  options.Add(option);

                  if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                  {
                        currentResolutionIndex = i;
                  }
            }

            resolutionDropDown.AddOptions(options);
            resolutionDropDown.value = currentResolutionIndex;
            resolutionDropDown.RefreshShownValue(); 
      }

      public void SetResolution(int resolutionIndex){
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
      }

      public void SetBrightness(float brightness)
      {
            _brightnessLevel= brightness;
            brightnessValue.text = brightness.ToString("0.0");
      }

      public void SetFullScreen(bool isFullScreen){
            _isFullScreen = isFullScreen;
      }

      public void VideoApply(){
            PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

            PlayerPrefs.SetInt("masterFullscreen",( _isFullScreen ? 1 : 0));
            Screen.fullScreen = _isFullScreen;

            StartCoroutine(ConfirmationBox());
      }

      [Header("Audio Settings")]
      [SerializeField] private TMP_Text volumeValue = null;
      [SerializeField] private Slider masterVolumeSlider= null;

      [SerializeField] private GameObject confirmationPrompt = null;

      [Header("Levels To Load")]
      public string _newGameLevel;
      private string levelToLoad;
  
      public void StartGameButton(){
            SceneManager.LoadScene(_newGameLevel);
      }

      public void QuitButton(){
            Application.Quit();
      }
      
      public void SetVolume(float volume){
            AudioListener.volume = volume;
            volumeValue.text = volume.ToString("0.0");
      }

      public void VolumeApply(){
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            StartCoroutine(ConfirmationBox());
      }

      public IEnumerator ConfirmationBox(){
            confirmationPrompt.SetActive(true);
            yield return new WaitForSeconds(2);
            confirmationPrompt.SetActive(false);
      }
}
