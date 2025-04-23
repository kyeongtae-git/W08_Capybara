using UnityEngine;

public class UI_SkillSelection : MonoBehaviour
{
    Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent += OnCanvasEnable;
    }

    void OnCanvasEnable(bool boolean)
    {
        _canvas.enabled = boolean;
        Debug.Log($"스킬 선택 패널 {boolean}");
    }
}
