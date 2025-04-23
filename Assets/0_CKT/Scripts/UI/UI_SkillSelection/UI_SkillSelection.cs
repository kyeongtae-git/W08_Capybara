using UnityEngine;

public class UI_SkillSelection : MonoBehaviour
{
    Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent += OnCanvasEnable;

        OnCanvasEnable(true);
    }

    void OnCanvasEnable(bool boolean)
    {
        _canvas.enabled = boolean;
        Debug.Log($"��ų ���� �г� {boolean}");
    }
}
