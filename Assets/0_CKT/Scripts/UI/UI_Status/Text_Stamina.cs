using TMPro;
using UnityEngine;

public class Text_Stamina : MonoBehaviour
{
    TextMeshProUGUI _staminaTMP;

    void Start()
    {
        _staminaTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateStaminaPointUIEvent += UpdateStage;
    }

    void UpdateStage(float cur, float max)
    {
        _staminaTMP.text = cur.ToString("N0") + " / " + max.ToString("N0");
    }
}
