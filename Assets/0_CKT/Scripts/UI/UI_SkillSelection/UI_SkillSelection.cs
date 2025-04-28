using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelection : MonoBehaviour
{
    Canvas _canvas;
    Button_SelectSkill[] _skills;

    void Start()
    {
        _canvas = GetComponent<Canvas>();

        Button[] buttons = GetComponentsInChildren<Button>();
        _skills = new Button_SelectSkill[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            _skills[i] = buttons[i].GetComponent<Button_SelectSkill>();
        }

        OnCanvasEnable(true, false);
    }

    private void OnEnable()
    {
        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent += OnCanvasEnable;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent -= OnCanvasEnable;
    }

    void OnCanvasEnable(bool canvas, bool gameClear)
    {
        if (gameClear)
        {
            StartCoroutine(Managers.GameManager.StartStage());
        }
        else
        {
            _canvas.enabled = canvas;
            Debug.Log($"스킬 선택 패널 {canvas}");

            int max = System.Enum.GetValues(typeof(SkillType)).Length;
            List<int> list = Managers.Utils.GetRandomNumbers(0, max, _skills.Length);

            for (int i = 0; i < _skills.Length; i++)
            {
                _skills[i].SetSkill(list[i]);
            }
        }
    }
}
