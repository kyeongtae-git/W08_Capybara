using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager
{
    // 치확, 치피, 공격력, 공격속도, 의지, 스태미나
    float[] _baseStatusArray = { 50f, 50f, 10f, 0.78f, 100f, 100f };
    List<float> _maxStatusList = new List<float>();

    public List<float> CurStatusList => _curStatusList;

    List<float> _curStatusList = new List<float>();

    public float UseWill => _useWill;
    float _useWill = 5f;

    public float UseStamina => _useStamina;
    float _useStamina = 10f;

    public void Init()
    {
        _maxStatusList = new List<float>();
        for (int i = 0; i < _baseStatusArray.Length; i++)
        {
            _maxStatusList.Add(_baseStatusArray[i]);
        }

        _curStatusList = new List<float>();
        for (int i = 0; i < _maxStatusList.Count; i++)
        {
            _curStatusList.Add(_maxStatusList[i]);
        }
    }

    public void UpdateMaxStatus(int index, float buff)
    {
        _maxStatusList[index] *= buff;
        _curStatusList[index] = _maxStatusList[index];
        Debug.Log($"{(StatusType)index} 영구 증가 적용");
    }

    public void UpdateCurStatus(int index, float buff)
    {
        _curStatusList[index] *= buff;
    }

    public void ChangeStatus(int index, float change)
    {
        _curStatusList[index] += change;
        _curStatusList[index] = Mathf.Clamp(_curStatusList[index], 0, _maxStatusList[index]);
    }

    public void UpdateUI_Status()
    {
        List<float> uiList = _curStatusList.ToList();
        uiList[(int)StatusType.Will] = _curStatusList[(int)StatusType.Will] / _maxStatusList[(int)StatusType.Will];
        uiList[(int)StatusType.Stamina] = _curStatusList[(int)StatusType.Stamina] / _maxStatusList[(int)StatusType.Stamina];

        Managers.UIManager.OnUpdateStatusUIEvent?.Invoke(uiList);
    }
}
