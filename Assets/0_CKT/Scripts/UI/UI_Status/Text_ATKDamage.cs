using TMPro;
using UnityEngine;

public class Text_ATKDamage : MonoBehaviour
{
    TextMeshProUGUI _atkDamageTMP;

    private void OnEnable()
    {
        _atkDamageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateATKDamageUIEvent += UpdateUI;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUpdateATKDamageUIEvent -= UpdateUI;

    }

    void UpdateUI(float atkDamage)
    {
        _atkDamageTMP.text = atkDamage.ToString("N2");
    }
}
