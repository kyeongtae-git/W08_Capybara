using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Action<bool> OnUI_EventCanvasEnableEvent;

    public Action<bool> OnUI_SkillSelectionCanvasEnableEvent;
    public Action OnButton_SelectSkillSetEvent;

    public Action<int> OnUpdateStageUIEvent;
    public Action<float> OnUpdateCritRateUIEvent;
    public Action<float> OnUpdateCritDamageUIEvent;
    public Action<float> OnUpdateATKDamageUIEvent;
    public Action<float> OnUpdateATKSpeedUIEvent;
    public Action<float> OnUpdateWillPointUIEvent;
    public Action<float> OnUpdateStaminaPointUIEvent;

    public Action<float> OnUpdateRockHealthUIEvent;

    public void Init()
    {
        
    }
}
