using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu<SettingsMenu>
{
    // Variables
    [Header("Settings UI")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button closeSettingsMenu;

    #region Volume Vars
    [Header("Volume")]
    [SerializeField] private TextMeshProUGUI masterVolumeText;
    [SerializeField] private Slider masterVolumeSlider;

    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private TextMeshProUGUI sfxVolumeText;
    [SerializeField] private Slider sfxVolumeSlider;

    private float _currentMasterVolume;
    private float _currentMusicVolume;
    private float _currentSFXVolume;
    #endregion

    [Header("Tween Details")]
    [SerializeField] private RectTransform targetPosition;
    [SerializeField] private float moveDuration = 1.0f;

    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Coroutine currentMoveCoroutine;


    protected override void Awake()
    {
        base.Awake();

        rectTransform = settingsMenu.GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;

        closeSettingsMenu.onClick.AddListener(SaveSettings);

        masterVolumeSlider.onValueChanged.AddListener((value) => UpdateVolume(value, AudioMixerKeys.MasterVolumeKey, ref _currentMasterVolume, ref masterVolumeText));
        musicVolumeSlider.onValueChanged.AddListener((value) => UpdateVolume(value, AudioMixerKeys.MusicVolumeKey, ref _currentMusicVolume, ref musicVolumeText));
        sfxVolumeSlider.onValueChanged.AddListener((value) => UpdateVolume(value, AudioMixerKeys.SFXVolumeKey, ref _currentSFXVolume, ref sfxVolumeText));

        LoadSettings();
    }

    #region Save/Load
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(AudioMixerKeys.MasterVolumeKey, _currentMasterVolume);
        PlayerPrefs.SetFloat(AudioMixerKeys.MusicVolumeKey, _currentMusicVolume);
        PlayerPrefs.SetFloat(AudioMixerKeys.SFXVolumeKey, _currentSFXVolume);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        _currentMasterVolume = PlayerPrefs.GetFloat(AudioMixerKeys.MasterVolumeKey);
        _currentMusicVolume = PlayerPrefs.GetFloat(AudioMixerKeys.MusicVolumeKey);
        _currentSFXVolume = PlayerPrefs.GetFloat(AudioMixerKeys.SFXVolumeKey);

        masterVolumeSlider.value = _currentMasterVolume;
        musicVolumeSlider.value = _currentMusicVolume;
        sfxVolumeSlider.value = _currentSFXVolume;

        UpdateVolume(_currentMasterVolume, AudioMixerKeys.MasterVolumeKey, ref _currentMasterVolume, ref masterVolumeText);
        UpdateVolume(_currentMusicVolume, AudioMixerKeys.MusicVolumeKey, ref _currentMusicVolume, ref musicVolumeText);
        UpdateVolume(_currentSFXVolume, AudioMixerKeys.SFXVolumeKey, ref _currentSFXVolume, ref sfxVolumeText);
    }
    #endregion

    public void OpenMenu()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        currentMoveCoroutine = StartCoroutine(MoveCoroutine(startPosition, targetPosition.anchoredPosition));
    }

    public void CloseMenu()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        currentMoveCoroutine = StartCoroutine(MoveCoroutine(targetPosition.anchoredPosition, startPosition));
    }

    private void UpdateVolume(float value, string keyName, ref float currentValue, ref TextMeshProUGUI text)
    {
        currentValue = value;
        text.text = keyName + ": " + (int)(currentValue * 100.0f);

        AudioManager.Instance.SetMixerVolume(keyName, currentValue);
    }



    IEnumerator MoveCoroutine(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null; 
        }

        rectTransform.anchoredPosition = endPos;
    }
}
