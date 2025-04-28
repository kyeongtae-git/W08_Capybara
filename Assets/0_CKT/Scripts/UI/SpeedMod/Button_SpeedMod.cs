using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_SpeedMod : MonoBehaviour
{
    Button _speedModButton;
    TextMeshProUGUI _timeScaleTMP;

    int _curTimeScale;
    int _maxTimeScale;

    void OnEnable()
    {
        _speedModButton = GetComponent<Button>();
        _timeScaleTMP = GetComponentInChildren<TextMeshProUGUI>();

        _curTimeScale = 3;
        _maxTimeScale = 3;

        Time.timeScale = _curTimeScale;
        _timeScaleTMP.text = $"배속 X{_curTimeScale}";

        _speedModButton.onClick.AddListener(() => ChangeTimeScale());
    }

    private void OnDestroy()
    {
        _speedModButton.onClick.RemoveAllListeners();
    }

    void ChangeTimeScale()
    {
        if (Managers.GameManager.CurGameState != GameState.Idle) return;
        
        _curTimeScale = (_curTimeScale % _maxTimeScale) + 1;

        Time.timeScale = _curTimeScale;
        _timeScaleTMP.text = $"배속 X{_curTimeScale}";

        Debug.Log($"배속 변경 : {_curTimeScale}");
    }
}
