using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class MenuBehavior : MonoBehaviour
{
      /*Audio Setting Variables
      Audio Settings focuses on the configuration, saving, and loading of audio settings focusing on the Master Audio, Music Audio, and SFX (Special Effects) Audio.
      In the Settings UI the player will be able to change the volume of each audio type, denoted by a slider and 0-100 number */
      [Header("Audio Settings")] 
      // Since Music and Sfx are children of the Master Audiomixer they are categorized into AudioMixerGroups. AudioMixer and AudioMixerGroup are libraries in Unity
      [SerializeField] private AudioMixerGroup musicGroup;
      [SerializeField] private AudioMixerGroup sfxGroup;
      // Floats dedicated to holding any changes made to the volume
      public static float musicVolume;
      public static float sfxVolume;
      public static float masterVolume;
      // Represents the 0-100 number notation of the volume
      [SerializeField] private TMP_Text masterVolumeTxt = null;
      [SerializeField] private TMP_Text musicVolumeTxt = null;
      [SerializeField] private TMP_Text sfxVolumeTxt = null;
      // Represents the volume slider 
      [SerializeField] private Slider masterVolumeSlider;
      [SerializeField] private Slider musicVolumeSlider;
      [SerializeField] private Slider sfxVolumeSlider;

      /*Video Setting Variables
      Video Settings focuses on the configuration of video settings, specifically resolution and fullscreen/window options. The player is able to choose a resolution from a dropdown that is generated based 
      on the players screen size. There will be a toggle allowing them to choose wether to be in fullscreen display or not.*/
      [Header("Video Settings")]
      // Represents the dropdown for resolution display
      public TMP_Dropdown resolutionDropDown;
      // Represents the resolutions themseves. Resolution is a Unity class
      private Resolution[] res;
      // Bool to represent the toggle between fuullscreen and windowed
      private bool isFullScreen;

     
      /*Awake is a unity method that runs when the game is booted up. We will use Awake since configuring settings should automatically happen upon starting the game. There are two main settings being
      configured in the Awake, Audio Settings and Video Resolution Settings.
      */
      public void Awake(){
            /*Audio Settings
            Once we have saved player settings, whenever we want to open the game again we have to load those player settings into the correct audio configuration
            Since we have Master, Music, and SFX Volumes we will have to get the data for each from the saved playerprefs, use our set method to set the volumes to the correct 
            configuration, and update the slider on the UI to show that it is displaying correctly
            */
            // Master Volume Load and Configure
            masterVolume = PlayerPrefs.GetFloat("masterVolume");
            SetMasterVolume(masterVolume);
            masterVolumeSlider.value = masterVolume;
            // Music Volume Load and Configure
            musicVolume = PlayerPrefs.GetFloat("Music");
            SetMusicVolume(musicVolume);
            musicVolumeSlider.value = musicVolume;
            // SFX Volume Load and Configure
            sfxVolume = PlayerPrefs.GetFloat("SFX");
            SetSFXVolume(sfxVolume);
            sfxVolumeSlider.value = sfxVolume;

            /* Resolution Settings
            Unlike Audio settings, resolution settings will maintain without needing to be saved from player prefs. However, we do need adjust the resolution options depending on the 
            screen size of the player. This is best done in Awake so that the player does not experience any wait.
            */
            GetResolutions();
      }

      /*Start Game
      The first option on the main menu which allows the player to start the game. Unity displays different levels and screens throughout the game as Scenes and uses the SceneManager Class to 
      invoke functions*/
      public void StartGameButton(){
            // Loading the first game level 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
      /*Quit Game
      Using quit allows players to exit the application and will also save all changes to playerprefs ( the class used to store setting changes) ensuring that any configurations 
      are saved before quitting.
      */
      public void QuitButton(){
            PlayerPrefs.Save();
            Application.Quit();
      }
      /*Set Music Volume
      Takes in a float volume which is indicated by using the slider in the UI. This value is configured using Log base 10 and *20 to convert it to a decible,
      before being passed to the Audio Mixer Group based on music. Additionally, the musicVolumeTxt which represents the text notation  0-100, is updated to match the value.
      */
      public void SetMusicVolume(float volume){
            // musicVolume variable is updated
            musicVolume = volume;
            // musicVolume notation is updated
            musicVolumeTxt.text = ((int)(volume*100)).ToString();
            // Audio Mixer of the music is updated
            musicGroup.audioMixer.SetFloat("Music", (Mathf.Log10(musicVolume)*20));
      }
      /*Set SFX Volume
      Takes in a float volume which is indicated by using the slider in the UI. This value is configured using Log base 10 and *20 to convert it to a decible,
      before being passed to the Audio Mixer Group based on SFX. Additionally, the sfxVolumeTxt which represents the text notation  0-100, is updated to match the value.
      */
      public void SetSFXVolume(float volume){
            // sfxVolume variable is updated
            sfxVolume = volume;
            // sfxVolume notation is updated
            sfxVolumeTxt.text = ((int)(volume*100)).ToString();
            // Audio Mixer of the music is updated
            sfxGroup.audioMixer.SetFloat("SFX", (Mathf.Log10(sfxVolume)*20));
      }
      /*Set Master Volume
      Takes in a float volume which is indicated by using the slider in the UI. Master Volume is configured differently than the individual volumes. Rather than be represented by one audio group it 
      represents all audio collectively. Here I use an AudioListener which is a Unity object that lives in a scene and contains all audio being played. Changing the volume on master decreases
      the volume of all audio. Additionally, the masterVolumeTxt which represents the text notation  0-100, is updated to match the value.
      */
      public void SetMasterVolume(float volume){
            // setting the audiolistener to the new volume
            AudioListener.volume = volume;
            // updates the notation on the ui
            masterVolumeTxt.text = ((int)(volume*100)).ToString();
      }
      /*Volume Apply
      Saves the changes to all volume settings once the player clicks apply button on the UI. Uses PlayerPrefs which is a class in Unity that stores items persistently. PlayerPrefs store similar to a 
      dictionary where it will have String Key and either string,float,or int as the value.
      */
      public void VolumeApply(){
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            PlayerPrefs.SetFloat("Music", musicVolume);
            PlayerPrefs.SetFloat("SFX", sfxVolume);
      }

      /*Get Resolutions
      Uses the Players Screen size, from Screen class in Unity, to find the compatible resolutions where Resolutions are also a class in Unity. Then we want to display those resolutions as dropdown options in the 
      main menu UI.
      */
      public void GetResolutions(){
            // We want to get all the screen resolutions avaliable on the players screen. To do so, we use Screen.resolutions and store them in an array of resolutions
            res = Screen.resolutions;
            // We also want the resolutions to be displayed in the dropdown menu as strings,
            // but before we can fill the dropdown we should clear any contents in case there are items that are not supported resolutions
            resolutionDropDown.ClearOptions();
            List<string> dropDownOptions = new List<string>();
            /* For each resolution given by the screen, we want to create a string to be displayed in the dropdown.
            We take the resolution.width and resolution.height and use the Resolution.ToString() to convert them. Each resolution will be displayed as
            width x height on the dropdown */
            for(int i = 0; i < res.Length; i++)
            {
                  dropDownOptions.Add((res[i].width).ToString() +"x"+ (res[i].height).ToString());
            }
            // Now that we have all the resolution strings we can add them as options to our dropdown
            resolutionDropDown.AddOptions(dropDownOptions);
            // Refreshing the dropdown so that it displays all the added values
            resolutionDropDown.RefreshShownValue(); 
      }

      /*Set Resolution
      The player can choose a resolution from the dropdown and the game will change to correct resolution setting. Takes a resolution index as a parameter which is the index in the dropdown where
      that resolution exists. For example, the lowest resolution setting will be the first dropdown option at index 0. Uses Screen.SetResolution method to configure the changes. 
      */
      public void SetResolution(int resIndex){
            Resolution resolution = res[resIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
      }
      /*Set Fullscreen
      The player can choose whether they want to be in fullscreen or windowed mode uses a toggle on the main menu. The toggle is passed as a boolean and is assigned to the _isFullScreen variable.
      Then uses Screen.fullScreen method to apply whether the game is in fullscreen or not. 
      */
      public void SetFullScreen(bool fullscreenToggle){
            isFullScreen = fullscreenToggle;
            Screen.fullScreen = isFullScreen;
      }
      
     

}
