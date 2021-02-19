using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private Profiles profiles;

    [SerializeField]
    private List<Sliders> volumeSliders = new List<Sliders>();

    // Start is called before the first frame update
    void Awake()
    {
        // If profiles exist SetProfile to be profiles
        if (profiles != null)
            profiles.SetProfile(profiles);
    }

    private void Start()
    {
        // Check if there is a profile and audioMixer 
        if (Settings.profile && Settings.profile.audioMixer != null)
        {
            // Get the audio levels at the start of the game
            Settings.profile.GetAudioLevels();
        }
    }

    public void ApplyChanges()
    {
        // Check if there is a profile and audioMixer 
        if (Settings.profile && Settings.profile.audioMixer != null)
        {
            // Save audio levels applied
            Settings.profile.SaveAudioLevels();
        }
    }

    public void CancelChanges()
    {
        // Check audio levels
        if (Settings.profile && Settings.profile.audioMixer != null)
        {
            // Get audio levels applying them when cancelled
            Settings.profile.GetAudioLevels();
        }

        // Loop though volume sliders and reset the values when cancelled button pressed
        for (int i = 0; i < volumeSliders.Count; i++)
        {
            volumeSliders[i].ResetSliderValue();
        }
    }
}