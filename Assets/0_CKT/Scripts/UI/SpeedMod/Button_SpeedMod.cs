using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_SpeedMod : MonoBehaviour
{
    Button _speedModButton;
    TextMeshProUGUI _timeScaleTMP;

    int _curTimeScale = 1;
    int _maxTimeScale = 3;

    void Start()
    {
        _curTimeScale = 1;
        _maxTimeScale = 3;

        _speedModButton = GetComponent<Button>();
        _timeScaleTMP = GetComponentInChildren<TextMeshProUGUI>();

        _speedModButton.onClick.AddListener(() => ChangeTimeScale());
    }

    void ChangeTimeScale()
    {
        _curTimeScale = (_curTimeScale % _maxTimeScale) + 1;

        Time.timeScale = _curTimeScale;
        _timeScaleTMP.text = $"��� X{_curTimeScale}";

        Debug.Log($"��� ���� : {_curTimeScale}");
    }
}
