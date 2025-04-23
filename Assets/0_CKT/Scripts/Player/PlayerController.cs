using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _critRate; //치명타 확률

    float _critDamage; //치명타 피해
    float _attackPower; //공격력
    float _attackSpeed; //공격 속도
    float _staminaPoint; //스태미나 수치
    float _willPoint; //의지 수치

    Coroutine _coAttack;

    void Start()
    {
        Init();
    }

    void Init()
    {
        List<float> playerStatus = Managers.PlayerManager.GetPlayerStatus();
        _critRate = playerStatus[0];
        _critDamage = playerStatus[1];
        _attackPower = playerStatus[2];
        _attackSpeed = playerStatus[3];
        _staminaPoint = playerStatus[4];
        _willPoint = playerStatus[5];

        _coAttack = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int index = 0;
            if ((int)Managers.GameManager.CurGameState == 0)
            {
                index++;
            }

            Managers.GameManager.ChangeGameState((GameState)index);
        }
        
        if (Managers.GameManager.CurGameState == GameState.Fight)
        {
            if (_coAttack == null)
            {
                _coAttack = StartCoroutine(TakeDamage());
            }  
        }
        else
        {
            if (_coAttack != null)
            {
                StopCoroutine(_coAttack);
                _coAttack = null;
            }
        }
    }

    IEnumerator TakeDamage()
    {
        //TODO : 공격 애니메이션 재생
        float attackTime = 1 / _attackSpeed;
        yield return new WaitForSeconds(attackTime);

        float attackDamage = _attackPower;
        if (Managers.Utils.RandomSuccess(_critRate * 0.01f))
        {
            attackDamage *= (1 + (_critDamage * 0.01f));
        }
        Managers.RockManager.OnGetDamageEvent?.Invoke(attackDamage);

        _coAttack = null;
    }
}
