using System.Collections.Generic;
using UnityEngine;

public class UI_SkillSeletctionDown : MonoBehaviour
{
    Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        CanvasOn(true, false);
    }

    private void OnEnable()
    {
        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent += CanvasOn;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent -= CanvasOn;
    }

    void CanvasOn(bool canvas, bool gameClear)
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = canvas;
    }


    public void CanvasOnOff(bool canvas)
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = canvas;
    }

}
