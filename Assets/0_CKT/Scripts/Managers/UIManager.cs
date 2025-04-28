using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Action<bool> OnUI_EventCanvasEnableEvent;

    public Action<bool, bool> OnUI_SkillSelectionCanvasEnableEvent;
    public Action<int> OnButton_SelectSkillSetEvent;

    public Action<int> OnUpdateStageUIEvent;
    public Action<float> OnUpdateCritRateUIEvent;
    public Action<float> OnUpdateCritDamageUIEvent;
    public Action<float> OnUpdateATKDamageUIEvent;
    public Action<float> OnUpdateATKSpeedUIEvent;
    public Action<float, float> OnUpdateWillPointUIEvent;
    public Action<float, float> OnUpdateStaminaPointUIEvent;

    public Action<float> OnDamageUIEvent;
    public Action<bool> OnCritUIEvent;
    public Action<float> OnUpdateRockHealthUIEvent;

    public void Init()
    {
        OnUI_EventCanvasEnableEvent = null;

        OnUI_SkillSelectionCanvasEnableEvent = null;
        OnButton_SelectSkillSetEvent = null;

        OnUpdateStageUIEvent = null;
        OnUpdateCritRateUIEvent = null;
        OnUpdateCritDamageUIEvent = null;
        OnUpdateATKDamageUIEvent = null;
        OnUpdateATKSpeedUIEvent = null;
        OnUpdateWillPointUIEvent = null;
        OnUpdateStaminaPointUIEvent = null;

        OnDamageUIEvent = null;
        OnCritUIEvent = null;
        OnUpdateRockHealthUIEvent = null;
    }
}
