using System;
using UnityEngine;

public class PlayerManager
{
    float _critRate; //치명타 확률

    float _maxCritDamage; //치명타 피해
    float _maxAttackPower; //공격력
    float _maxAttackSpeed; //공격 속도
    float _maxStaminaPoint; //스태미나 수치
    float _maxWillPoint; //의지 수치

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
    /// 대상에게 데미지를 주는 메소드
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {

    }
}
