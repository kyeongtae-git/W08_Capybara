using System;
using System.Collections.Generic;
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
        _maxAttackPower = 10f;
        _maxAttackSpeed = 0.78f;
        _maxStaminaPoint = 100f;
        _maxWillPoint = 100f;
    }

    public List<float> GetPlayerStatus()
    {
        List<float> playerStatus = new List<float>();

        playerStatus.Add(_critRate);
        playerStatus.Add(_maxCritDamage);
        playerStatus.Add(_maxAttackPower);
        playerStatus.Add(_maxAttackSpeed);
        playerStatus.Add(_maxStaminaPoint);
        playerStatus.Add(_maxWillPoint);

        return playerStatus;
    }
}
