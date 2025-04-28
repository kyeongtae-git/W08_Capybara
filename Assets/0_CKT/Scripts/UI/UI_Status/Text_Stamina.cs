using TMPro;
using UnityEngine;

public class Text_Stamina : MonoBehaviour
{
    TextMeshProUGUI _staminaTMP;

    private void OnEnable()
    {
        _staminaTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateStaminaPointUIEvent += UpdateStage;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUpdateStaminaPointUIEvent -= UpdateStage;
    }

    void UpdateStage(float cur, float max)
    {
        _staminaTMP.text = cur.ToString("N0") + " / " + max.ToString("N0");
    }
}
