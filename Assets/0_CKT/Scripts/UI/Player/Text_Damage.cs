using System.Collections;
using TMPro;
using UnityEngine;

public class Text_Damage : MonoBehaviour
{
    TextMeshProUGUI _damageTMP;
    Coroutine _coUpdateUI;

    float _normalSize = 50f;
    float _critSize = 100f;

    private void OnEnable()
    {
        _damageTMP = GetComponent<TextMeshProUGUI>();
        _damageTMP.enabled = false;

        Managers.UIManager.OnDamageUIEvent += UpdateUI;
        Managers.UIManager.OnCritUIEvent += CritText;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnDamageUIEvent -= UpdateUI;
        Managers.UIManager.OnCritUIEvent -= CritText;
    }

    void UpdateUI(float damage)
    {
        if (_coUpdateUI != null)
        {
            StopCoroutine(_coUpdateUI);
        }
        _coUpdateUI = StartCoroutine(CoUpdateUI(damage));
    }

    IEnumerator CoUpdateUI(float damage)
    {
        _damageTMP.enabled = true;
        _damageTMP.text = damage.ToString("N2");

        yield return new WaitForSeconds(0.5f);

        _damageTMP.enabled = false;
        _coUpdateUI = null;
    }

    void CritText(bool crit)
    {
        _damageTMP.fontSize = (crit) ? _critSize : _normalSize;
    }
}
