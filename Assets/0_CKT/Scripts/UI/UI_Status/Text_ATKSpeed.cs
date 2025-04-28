using TMPro;
using UnityEngine;

public class Text_ATKSpeed : MonoBehaviour
{
    TextMeshProUGUI _atkSpeedTMP;

    private void OnEnable()
    {
        _atkSpeedTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateATKSpeedUIEvent += UpdateUI;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUpdateATKSpeedUIEvent -= UpdateUI;
    }

    void UpdateUI(float atkSpeed)
    {
        _atkSpeedTMP.text = atkSpeed.ToString("N2");
    }
}
