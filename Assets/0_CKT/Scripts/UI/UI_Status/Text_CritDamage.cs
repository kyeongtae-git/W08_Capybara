using TMPro;
using UnityEngine;

public class Text_CritDamage : MonoBehaviour
{
    TextMeshProUGUI _critDamageTMP;

    private void OnEnable()
    {
        _critDamageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateCritDamageUIEvent += UpdateUI;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUpdateCritDamageUIEvent -= UpdateUI;
    }

    void UpdateUI(float critDamage)
    {
        _critDamageTMP.text = critDamage.ToString("N2");
    }
}
