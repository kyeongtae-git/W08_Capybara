using TMPro;
using UnityEngine;

public class Text_CritDamage : MonoBehaviour
{
    TextMeshProUGUI _critDamageTMP;

    void Start()
    {
        _critDamageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateCritDamageUIEvent += UpdateUI;
    }

    void UpdateUI(float critDamage)
    {
        _critDamageTMP.text = critDamage.ToString("N2");
    }
}
