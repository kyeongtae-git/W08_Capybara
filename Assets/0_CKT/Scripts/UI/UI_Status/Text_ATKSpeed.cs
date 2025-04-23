using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_ATKSpeed : MonoBehaviour
{
    TextMeshProUGUI _atkSpeedTMP;

    void Start()
    {
        _atkSpeedTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateUIEvent += UpdateUI;
    }

    void UpdateUI(List<float> list)
    {
        _atkSpeedTMP.text = list[(int)StatusType.ATKSpeed].ToString("N2");
    }
}
