using System;
using UnityEngine;

public class PlayerManager
{
    float _critRate; //ġ��Ÿ Ȯ��

    float _maxCritDamage; //ġ��Ÿ ����
    float _maxAttackPower; //���ݷ�
    float _maxAttackSpeed; //���� �ӵ�
    float _maxStaminaPoint; //���¹̳� ��ġ
    float _maxWillPoint; //���� ��ġ

    public void Init()
    {
        _critRate = 50f;

        _maxCritDamage = 50f;
        _maxAttackPower = 100f;
        _maxAttackSpeed = 0.78f;
        _maxStaminaPoint = 100f;
        _maxWillPoint = 100f;
    }

    /// <summary>
    /// ��󿡰� �������� �ִ� �޼ҵ�
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {

    }
}
