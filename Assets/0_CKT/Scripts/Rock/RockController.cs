using UnityEngine;

public class RockController : MonoBehaviour
{
    Canvas _canvas;
    
    float _maxHealth;
    float _curHealth;
    float _moveSpeed;
    Vector3 _stopPoint;

    private void OnEnable()
    {
        _maxHealth = Managers.RockManager.MaxHealth;
        _curHealth = _maxHealth;
        _moveSpeed = Managers.RockManager.MoveSpeed;
        _stopPoint = Managers.RockManager.StopPoint;
        Managers.RockManager.OnGetDamageEvent += GetDamage;

        _canvas = GetComponentInChildren<Canvas>();
        if (_canvas != null) _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        Managers.RockManager.OnGetDamageEvent -= GetDamage;
    }

    private void Update()
    {
        if (transform.position.x > _stopPoint.x)
        {
            transform.position -= transform.right * _moveSpeed * Time.deltaTime;
        }
        else
        {
            _canvas.enabled = true;
        }
    }

    void GetDamage(float damage)
    {
        //ü�� ����
        _curHealth -= damage;
        _curHealth = Mathf.Clamp(_curHealth, 0, _maxHealth);
        Debug.Log($"Rock ������ ���� : {damage}, ���� ü�� : {_curHealth}");

        float healthPercent = _curHealth / _maxHealth;
        Managers.UIManager.OnUpdateRockHealthUIEvent?.Invoke(healthPercent);

        //��� Ȯ��
        if (_curHealth <= 0)
        {
            Managers.GameManager.GoNextStage();
            Debug.Log($"Rock �ı���, ���� ���� �̵�");
            gameObject.SetActive(false);
        }
    }
}
