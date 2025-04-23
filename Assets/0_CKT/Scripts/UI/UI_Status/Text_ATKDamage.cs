using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_ATKDamage : MonoBehaviour
{
    TextMeshProUGUI _atkDamageTMP;

    void Start()
    {
        _atkDamageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateStatusUIEvent += UpdateUI;
    }

    void UpdateUI(List<float> list)
    {
        _atkDamageTMP.text = list[(int)StatusType.ATKDamage].ToString("N2");
    }
}
