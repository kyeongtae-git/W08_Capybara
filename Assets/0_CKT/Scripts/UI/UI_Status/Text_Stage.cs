using TMPro;
using UnityEngine;

public class Text_Stage : MonoBehaviour
{
    TextMeshProUGUI _stageTMP;

    void Start()
    {
        _stageTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateStageEvent += UpdateStage;
    }

    void UpdateStage(int index)
    {
        _stageTMP.text = $"{index} Day";
    }
}
