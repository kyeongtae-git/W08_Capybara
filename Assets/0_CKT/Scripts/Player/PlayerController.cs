using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _critRate; //ġ��Ÿ Ȯ��

    float _critDamage; //ġ��Ÿ ����
    float _attackPower; //���ݷ�
    float _attackSpeed; //���� �ӵ�
    float _staminaPoint; //���¹̳� ��ġ
    float _willPoint; //���� ��ġ

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
        //TODO : ���� �ִϸ��̼� ���
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
