using TMPro;
using UnityEngine;

public class Text_Will : MonoBehaviour
{
    TextMeshProUGUI _willTMP;

    void Start()
    {
        _willTMP = GetComponent<TextMeshProUGUI>();
        Managers.UIManager.OnUpdateWillPointUIEvent += UpdateStage;
    }

    void UpdateStage(float cur, float max)
    {
        _willTMP.text = cur.ToString("N0") + " / " + max.ToString("N0");
    }
}
