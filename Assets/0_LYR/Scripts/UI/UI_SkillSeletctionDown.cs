using System.Collections.Generic;
using UnityEngine;

public class UI_SkillSeletctionDown : MonoBehaviour
{
    Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        

        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent += CanvasOn;

        CanvasOn(true, false);
    }


    void CanvasOn(bool canvas, bool gameClear)
    {
        _canvas.enabled = canvas;
    }


    public void CanvasOnOff(bool canvas)
    {
        _canvas.enabled = canvas;
    }

}
