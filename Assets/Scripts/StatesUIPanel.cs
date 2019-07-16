using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatesUIPanel : MonoBehaviour
{
    public List<Button> panelButtons;

    public void SwitchPanelButtonActivation(bool shouldActivate)
    {
        for (int i = 0; i < panelButtons.Count; i++)
        {
            panelButtons[i].interactable = shouldActivate;
        }
    }
}
