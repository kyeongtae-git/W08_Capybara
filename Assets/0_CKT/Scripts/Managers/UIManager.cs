using System;
using UnityEngine;

public class UIManager
{
    public Action<bool> OnUI_EventCanvasEnableEvent;

    public Action<bool> OnUI_SkillSelectionCanvasEnableEvent;
    public Action OnButton_SelectSkillSetEvent;

    public Action<float> OnImage_WillUpdateImageEvent;

    public void Init()
    {
        
    }
}
