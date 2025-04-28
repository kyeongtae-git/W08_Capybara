using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_SpeedMod : MonoBehaviour
{
    Button _speedModButton;
    TextMeshProUGUI _timeScaleTMP;

    int _curTimeScale = 2; //(0, 1, 2) + 1
    int _maxTimeScale = 3;

    void Start()
    {
        _speedModButton = GetComponent<Button>();
        _timeScaleTMP = GetComponentInChildren<TextMeshProUGUI>();

        _curTimeScale = 2;
        _maxTimeScale = 3;

        ChangeTimeScale();
        _speedModButton.onClick.AddListener(() => ChangeTimeScale());
    }

    void ChangeTimeScale()
    {
        if (Managers.GameManager.CurGameState != GameState.Idle) return;
        
        _curTimeScale = (_curTimeScale % _maxTimeScale) + 1;

        Time.timeScale = _curTimeScale;
        _timeScaleTMP.text = $"��� X{_curTimeScale}";

        Debug.Log($"��� ���� : {_curTimeScale}");
    }
}
