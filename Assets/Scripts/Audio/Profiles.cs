using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Volume
{
    public string name;
    public float volume = 1f;
    public float tempVolume = 1f;
}

public class Settings
{
    public static Profiles profile;
}

[CreateAssetMenu(menuName = "Assets/Create Profile")]
public class Profiles : ScriptableObject
{
    public bool saveInPlayerPrefs = true;
    public string prefPrefix = "Settings";

    public AudioMixer audioMixer;
    public Volume[] volumeControl;

    // Updates saved profile
    public void SetProfile(Profiles profile)
    {
        Settings.profile = profile;
    }

    public float GetAudioLevels(string name)
    {
        float volume = 1f;

        // Check if audioMixer exists if not log error warning
        if (!audioMixer)
        {
            Debug.LogWarning("There is no AudioMixer defined in the profiles file");
            return volume;
        }

        for (int i = 0; i < volumeControl.Length; i++)
        {
            if (volumeControl[i].name != name)
            {
                continue;
            }
            else
            {
                // Check if we have player prefs and save the player prefs
                if (saveInPlayerPrefs)
                {
                    // Check does the prefix exists with a particular name in the loop, if so store in the volumeControl[i].volume
                    if (PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                    {
                        volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                    }
                }

                // Reset tempVolume and set the default tempVolume to be the actual volume
                volumeControl[i].tempVolume = volumeControl[i].volume;

                // Set the mixer for the volume
                if (audioMixer)
                {
                    Settings.profile.audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20f);
                }

                // Save volume
                volume = volumeControl[i].volume;

                break;
            }
        }
        return volume;
    }

    public void GetAudioLevels()
    {
        // Check if audioMixer exists if not log error warning
        if (!audioMixer)
        {
            Debug.LogWarning("There is no AudioMixer defined in the profiles file");
            return;
        }

        for (int i = 0; i < volumeControl.Length; i++)
        {
            // Save the playerprefs 
            if (saveInPlayerPrefs)
            {
                // Check does the prefix exists with a particular name in the loop, if so store in the volume control volume
                if (PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                {
                    volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                }
            }

            // Reset the audio volume
            volumeControl[i].tempVolume = volumeControl[i].volume;

            // Set the mixer to match the volume
            audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20f);
        }
    }

    // Function used to set the actual value for the tempVolume
    public void SetAudioLevels(string name, float volume)
    {
        // Check if audioMixer exists if not log error warning
        if (!audioMixer)
        {
            Debug.LogWarning("There is no AudioMixer defined in the profiles file");
            return;
        }

        for (int i = 0; i < volumeControl.Length; i++)
        {
            // If the names don't match keep continuing to check until they do
            if (volumeControl[i].name != name)
            {
                continue;
            }
            else
            {
                // Before breaking out the loop set audioMixer name and volume, and set the tempVolume equal to the volume
                audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20f);
                volumeControl[i].tempVolume = volume;
                break;
            }
        }
    }

    public void SaveAudioLevels()
    {
        // Check if audioMixer exists if not log error warning
        if (!audioMixer)
        {
            Debug.LogWarning("There is no AudioMixer defined in the profiles file");
            return;
        }

        float volume = 0f;

        for (int i = 0; i < volumeControl.Length; i++)
        {
            // Get the tempVolume saved
            volume = volumeControl[i].tempVolume;

            if (saveInPlayerPrefs)
            {
                // Set player prefs volume
                PlayerPrefs.SetFloat(prefPrefix + volumeControl[i].name, volume);
            }

            // Update mixer
            audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volume) * 20f);

            // Pass in the volume from the temp to the main volume
            volumeControl[i].volume = volume;
        }
    }
}