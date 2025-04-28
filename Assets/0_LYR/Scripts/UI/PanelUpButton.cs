using UnityEngine;
using UnityEngine.UI;

public class PanelUpButton : MonoBehaviour
{
    Button _panelUpButton;
    Canvas _skillCanvas;
    UI_SkillSeletctionDown _downbuttonUI;
    UI_SkillSeletctionUp _upButtonUI;


    void OnEnable()
    {
        _skillCanvas = FindAnyObjectByType<UI_SkillSelection>().gameObject.GetComponent<Canvas>();
        _downbuttonUI = FindAnyObjectByType<UI_SkillSeletctionDown>();
        _upButtonUI = FindAnyObjectByType<UI_SkillSeletctionUp>();
        _panelUpButton = GetComponent<Button>();

        _panelUpButton.onClick.RemoveAllListeners();
        _panelUpButton.onClick.AddListener(() => _skillCanvas.enabled = true);
        _panelUpButton.onClick.AddListener(() => _downbuttonUI.CanvasOnOff(true));
        _panelUpButton.onClick.AddListener(() => _upButtonUI.CanvasOnOff(false));
    }

    private void OnDisable()
    {
        _panelUpButton.onClick.RemoveAllListeners();
    }
}
