using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof (Slider))]
public class Sliders : MonoBehaviour
{
    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    [Header("Volume Name")]
    [Tooltip("This is the name of the exposed parameter")]
    [SerializeField]
    private string volumeName = "Enter Volume Name Here";

    [Header("Volume Label")]
    [SerializeField]
    private TextMeshProUGUI volumeLabel;

    private void Start()
    {
        // Reset slider value at the start, this is done so that it saves any changes made to the sliders at run time meaning
        // that next time the project is played the volume values applied to the sliders previously will be automatically applied.
        ResetSliderValue();

        slider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(slider.value);
        });
    }

    public void UpdateValueOnChange(float value)
    {
        if (volumeLabel != null)
            volumeLabel.text = Mathf.Round(value * 100.0f).ToString() + "%";

        // Check if the Settings.profile exists if so update values
        if (Settings.profile)
        {
            // Call audio levels and update 
            Settings.profile.SetAudioLevels(volumeName, value);
        }
    }

    public void ResetSliderValue()
    {
        // Check if the Settings.profile exists, if so reset the volume for the slider and the UpdateValueOnChange function
        if (Settings.profile)
        {
            float volume = Settings.profile.GetAudioLevels(volumeName);

            UpdateValueOnChange(volume);
            slider.value = volume;
        }
    }
}
