using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MicInputScript : MonoBehaviour
{
    public int audioSampleRate = 48000;
    private Animator _animator;

    public float levelMax;

    //private int samples = 8192;
    private AudioSource audioSource;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        //get components you'll need
        audioSource = GetComponent<AudioSource>();

        //initialize input with default mic
        UpdateMicrophone();
    }

    private void Update()
    {

        int micPosition = Microphone.GetPosition("") - (128+1);
        float[] waveData = new float[128];
        audioSource.clip.GetData(waveData, micPosition);
        for(int i = 0; i < 128; i++)
        {
            float wavePeak = waveData[1] * waveData[i];
            if(levelMax < wavePeak)
            {
                _animator.SetFloat("volume", wavePeak * 1000);
            }
        }
    }

    void UpdateMicrophone()
    {
        audioSource.Stop();
        //Start recording to audioclip from the mic
        audioSource.clip = Microphone.Start("", true, 10, audioSampleRate);
        audioSource.loop = false;
        // Mute the sound with an Audio Mixer group becuase we don't want the player to hear it
        Debug.Log(Microphone.IsRecording("").ToString());

        if (Microphone.IsRecording(""))
        { //check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
            while (!(Microphone.GetPosition("") > 0))
            {

            } // Wait until the recording has started. 

            Debug.Log("recording started");

            // Start playing the audio source
            audioSource.Play();
            StartCoroutine(wait());
        }
        else
        {
            //microphone doesn't work for some reason

            Debug.Log("Microphone doesn't work!");
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(10);
        UpdateMicrophone();
    }
}
