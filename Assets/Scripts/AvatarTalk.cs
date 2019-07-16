using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarTalk : MonoBehaviour
{
    public Animator animator;

    public float talkVolume = 1f;
    public float tempSineFloat;

    public float talkSpeed = 1f;

    public AudioSource microphonePlayback;
    public AudioClip microphoneInput;
    public float micSensitivity;
    public bool flapped;

    public bool micEnabled = false;
    public string micName;

    public float[] waveData;
    private int samples = 999;
    public float micVolumeEaseDownSpeed = 1f;

    private void Start()
    {
        StartCoroutine("MicVolumeEaseDown");
    }

    private IEnumerator MicVolumeEaseDown()
    {
        float oldMicVolume = 0f;
        float newMicVolume = 0f;
        float setMicVolume = 0f;

        while (true)
        {
            if (micEnabled)
            {
                newMicVolume = LevelMax();
                if (newMicVolume > talkVolume)
                {
                    setMicVolume = newMicVolume;
                }
                oldMicVolume = newMicVolume;
            }

            if(setMicVolume > 0)
            {
                setMicVolume -= Time.deltaTime * micVolumeEaseDownSpeed;
            }

            talkVolume = setMicVolume;

            yield return null;
        }
    }

    private void Update()
    {
        tempSineFloat = Mathf.PingPong(Time.time * talkSpeed, 1);

        animator.SetFloat("talkVolume", talkVolume);
        animator.SetFloat("tempLipSync", tempSineFloat);
    }

    float LevelMax()
    {
        if (!micEnabled)
        {
            return 0f;
        }
        else
        {
            float levelMax = 0;
            waveData = new float[samples];
            int micPosition = Microphone.GetPosition(micName) - (samples + 1);
            if (micPosition < 0)
            {
                return 0;
            }
            microphoneInput.GetData(waveData, micPosition);
            for (int i = 0; i < samples; ++i)
            {
                float wavePeak = waveData[i] * waveData[i];
                if (levelMax < wavePeak)
                {
                    levelMax = wavePeak;
                }
            }
            return Mathf.Clamp(levelMax * micSensitivity, 0f, 1f);
        }
    }

    public void SetMic(string newMicName)
    {
        micName = newMicName;
    }

    public void StartMicrophone()
    {
        microphoneInput = Microphone.Start(micName, true, samples, 44100);
        microphonePlayback.clip = microphoneInput;
        while(!(Microphone.GetPosition(micName) > 0)) { }
        microphonePlayback.Play();

        micEnabled = Microphone.IsRecording(micName);
    }

    public void StopMicrophone()
    {
        Microphone.End(micName);
    }
}
