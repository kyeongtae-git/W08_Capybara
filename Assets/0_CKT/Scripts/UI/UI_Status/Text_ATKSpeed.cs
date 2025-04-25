using TMPro;
using UnityEngine;

public class Text_ATKSpeed : MonoBehaviour
{
    TextMeshProUGUI _atkSpeedTMP;

    void Start()
    {
        _atkSpeedTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateATKSpeedUIEvent += UpdateUI;
    }

    void UpdateUI(float atkSpeed)
    {
        _atkSpeedTMP.text = atkSpeed.ToString("N2");
    }
}
