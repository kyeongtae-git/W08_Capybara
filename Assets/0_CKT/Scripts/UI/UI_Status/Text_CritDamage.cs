using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_CritDamage : MonoBehaviour
{
    TextMeshProUGUI _critDamageTMP;

    void Start()
    {
        _critDamageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateUIEvent += UpdateUI;
    }

    void UpdateUI(List<float> list)
    {
        _critDamageTMP.text = list[(int)StatusType.CritDamage].ToString("N2");
    }
}
