using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneUI : MonoBehaviour
{
    public AvatarTalk avatarTalk;
    public Dropdown chooseMicDropdown;

    private void Start()
    {
        chooseMicDropdown.ClearOptions();
        List<string> micOptions = new List<string>();
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            micOptions.Add(Microphone.devices[i]);
        }

        chooseMicDropdown.AddOptions(micOptions);

        avatarTalk.SetMic(chooseMicDropdown.options[0].text);

        chooseMicDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(chooseMicDropdown);
        });
    }

    void DropdownValueChanged(Dropdown change)
    {
        avatarTalk.SetMic(change.options[change.value].text);
    }


}
