using UnityEngine;

public class UI_Event : MonoBehaviour
{
    Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        Managers.UIManager.OnUI_EventCanvasEnableEvent += OnCanvasEnable;
    }

    void OnCanvasEnable(bool boolean)
    {
        _canvas.enabled = boolean;
        Debug.Log($"이벤트 패널 {boolean}");
    }
}
