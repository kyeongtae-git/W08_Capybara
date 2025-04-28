using TMPro;
using UnityEngine;

public class Text_Stage : MonoBehaviour
{
    TextMeshProUGUI _stageTMP;

    void Start()
    {
        _stageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateStageUIEvent += UpdateStage;
    }

    void UpdateStage(int index)
    {
        string text = (index == 0) ? "D-Day" : $"D-{index}";
        _stageTMP.text = text;
    }
}
