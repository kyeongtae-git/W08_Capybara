using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Action<bool> OnUI_EventCanvasEnableEvent;

    public Action<bool> OnUI_SkillSelectionCanvasEnableEvent;
    public Action OnButton_SelectSkillSetEvent;

    public Action<List<float>> OnUpdateUIEvent;
    public Action<int> OnUpdateStageEvent;

    public void Init()
    {
        
    }
}
