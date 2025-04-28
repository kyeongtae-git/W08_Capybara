using TMPro;
using UnityEngine;

public class Text_Stage : MonoBehaviour
{
    TextMeshProUGUI _stageTMP;

    private void OnEnable()
    {
        _stageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateStageUIEvent += UpdateStage;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUpdateStageUIEvent -= UpdateStage;
    }

    void UpdateStage(int index)
    {
        string text = (index == 0) ? "D-Day" : $"D-{index}";
        _stageTMP.text = text;
    }
}
