using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMicrophone : MonoBehaviour
{
    public AudioSource micAudioSource;

    #region Elements
    static int sampleRate = 0;
    public enum Dsp_Buffer_Sizes { b_128 = 128, b_256 = 256, b_340 = 340, b_480 = 480, b_512 = 512, b_1024 = 1024 };
 
    [Tooltip("Seconds of input that will be buffered. Too short = lost data")]
    public int micRecordLength = 1;
    [Tooltip("Seconds until mic input is played through audio source. Too short = artifacts")]
    public float latencyBuffer = 0.001f;
    [Tooltip("Larger buffer size adds latency. Too short = artifacts")]
    public Dsp_Buffer_Sizes Dsp_Buffer_Size = Dsp_Buffer_Sizes.b_256; //This the the "Best Latency" value
 
    private List<string> ListDevice;
    public int selectedDevice = 0;
    #endregion
 
    /// <summary>Attempts to adjust settings for less latency</summary>
    void SetupAudioSource()
    {
        //bypass to try and speed things up
        micAudioSource.bypassEffects = false;
        micAudioSource.bypassListenerEffects = false;
        micAudioSource.bypassReverbZones = false;
        micAudioSource.priority = 0;
        micAudioSource.pitch = 1;
    }
    /// <summary>Attempts to adjust settings for less latency</summary>
    void SetupGlobalAudio()
    {
        AudioConfiguration config = AudioSettings.GetConfiguration();
        config.dspBufferSize = (int)Dsp_Buffer_Size;
        config.sampleRate = 0;          //11025, 22050, 44100, 48000, 88200, 96000,
        config.numVirtualVoices = 1;    //1, 2, 4, 8, 16, 32, 50, 64, 100, 128, 256, 512,
        config.numRealVoices = 1;       //1, 2, 4, 8, 16, 32, 50, 64, 100, 128, 256, 512,
        config.speakerMode = AudioSpeakerMode.Mono;
        AudioSettings.Reset(config);
    }

    public void ChangeAudioDevice(string newMicName)
    {
        int listIndex = ListDevice.IndexOf(newMicName);
        selectedDevice = listIndex;
    }

    #region DropDown and Button Functions
    private void DropDown_Changed(int index)
    {
        selectedDevice = index;
    }
 
    public void Button_Click()
    {
        if (IsRecording)
        {
            IsRecording = false;
        }
        else
        {
            Record();
        }
    }
    #endregion
 
    /// <summary>Set = Start / stop mic recording. Get = Microphone.IsRecording</summary>
    private bool IsRecording
    {
        get { return Microphone.IsRecording(ListDevice[selectedDevice]); }
        set
        {
            if (value) //true
            {
                if (!Microphone.IsRecording(ListDevice[selectedDevice]))
                {
                    sampleRate = AudioSettings.outputSampleRate;
                    //Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource
                    micAudioSource.clip = Microphone.Start(ListDevice[selectedDevice],true , micRecordLength, sampleRate );// maxFreq);
                    if (!(RecordHeadPosition > 0)) { }          //wait for mic ready
                    micAudioSource.loop = true;                    //continual output
                    micAudioSource.mute = false;                   //Hack for bug
                    micAudioSource.PlayDelayed(latencyBuffer);     //must be delayed or bad results
                }
            }
            else //false
            {
                if (Microphone.IsRecording(ListDevice[selectedDevice]))
                {
                    Microphone.End(ListDevice[selectedDevice]);
                    micAudioSource.clip = null;
                    micAudioSource.loop = false;
                }
            }
        }
    }

    /// <summary>Loads recording device names into DropDown</summary>
    /// <returns>status string</returns>
    private string LoadMicrophoneDevices()
    {
        if (Microphone.devices.Length <= 0)
            return ("Microphone not connected!");

        ListDevice = new List<string>();

        foreach (string device in Microphone.devices)
        {
            ListDevice.Add(device);
        }

        return "Recording devices loaded";
    }


    private int RecordHeadPosition
    {
        get { return Microphone.GetPosition(ListDevice[selectedDevice]); }
    }
 
    private void Record()
    {
        if (!IsRecording)
        {
            IsRecording = true;
        }
    }
 
    private void Awake()
    {
        LoadMicrophoneDevices();
        //SetupGlobalAudio();
        SetupAudioSource();
    }
 
    #region Everything below is for calculating and auto setting latency
    static int Read_Position = 0;
    static int Max_Latency = 0;
    static int old_Write_Position = 0;
    static bool Reload = false;
 
    private void FixedUpdate()
    {
        if (IsRecording)
        {
            float latencySeconds = (float)Max_Latency / (float)AudioSettings.outputSampleRate;
            //TextStatus.text = Max_Latency.ToString() + " Samples / " + AudioSettings.outputSampleRate.ToString() + " samplerate = " + latencySeconds.ToString() + " seconds Latency";
 
            //auto select best latency value
            if (Max_Latency > latencyBuffer * sampleRate)
            {
                latencyBuffer = ((float)Max_Latency / (float)sampleRate) + 0.01f;
                IsRecording = false;
                IsRecording = true;
                Reload = true;
            }
        }
    }
 
    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (Reload)
        {
            Max_Latency = 0;
            Read_Position = 0;
            old_Write_Position = 0;
            Reload = false;
        }
        Read_Position += data.Length;
        int Write_Position = Microphone.GetPosition(ListDevice[selectedDevice]);
       
        if (Write_Position < old_Write_Position)    //Check if write buffer looped
        {
            Read_Position = data.Length;
        }
        //latency in samples
        int Latency = Read_Position - Write_Position;
        if (Latency > Max_Latency)
            Max_Latency = Latency;
 
        old_Write_Position = Write_Position;
    }
    #endregion
}
