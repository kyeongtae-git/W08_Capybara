using UnityEngine;

public class UI_SkillSeletctionUp : MonoBehaviour
{
    Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        CanvasOnOff(false);
    }


    public void CanvasOnOff(bool canvas)
    {
        _canvas.enabled = canvas;
    }


}
