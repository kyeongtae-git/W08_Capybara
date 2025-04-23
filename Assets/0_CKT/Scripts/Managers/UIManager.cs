using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Action<bool> OnUI_EventCanvasEnableEvent;

    public Action<bool> OnUI_SkillSelectionCanvasEnableEvent;
    public Action OnButton_SelectSkillSetEvent;

    public Action<List<float>> OnUpdateStatusUIEvent;
    public Action<int> OnUpdateStageUIEvent;

    public Action<float> OnUpdateRockHealthUIEvent;

    public void Init()
    {
        
    }
}
