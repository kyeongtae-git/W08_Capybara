using TMPro;
using UnityEngine;

public class Text_CritRate : MonoBehaviour
{
    TextMeshProUGUI _critRateTMP;

    void Start()
    {
        _critRateTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateCritRateUIEvent += UpdateUI;
    }

    void UpdateUI(float critRate)
    {
        _critRateTMP.text = critRate.ToString("N2");
    }
}
