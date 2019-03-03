using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer mainMixer;

 
    public void ChangeMixerFromValue(float value)
    {
        float pitchValue = (100 + ((1 - value) * 50))/100;
        mainMixer.SetFloat("MasterPitch", pitchValue);

        float bassVolume = -300 * value + 240;
        if(bassVolume >= 20)
        {
            bassVolume = 20;
        }
        mainMixer.SetFloat("BassVolume", bassVolume);
        float drum3Volume = -150 * value + 90 - 10;
        if (drum3Volume >= 0)
        {
            drum3Volume = 0;
        }
        mainMixer.SetFloat("Drum3Volume", drum3Volume);
        float drum4Volume = -100 * value + 40 - 10;
        if (drum4Volume >= 0)
        {
            drum4Volume = 0;
        }
        mainMixer.SetFloat("Drum4Volume", drum4Volume);
        float drum2Volume = -80 * value + 20 - 10;
        if (drum2Volume >= 0)
        {
            drum2Volume = 0;
        }
        mainMixer.SetFloat("Drum2Volume", drum2Volume);
    }
}
