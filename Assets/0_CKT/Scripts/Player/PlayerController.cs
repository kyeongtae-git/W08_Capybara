using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //0 ġ��Ÿ Ȯ��
    //1 ġ��Ÿ ����
    //2 ���ݷ�
    //3 ���� �ӵ�
    //4 ���� ��ġ
    //5 ���¹̳� ��ġ
    List<float> _playerStatus = new List<float>();

    Coroutine _coAttack;
    Coroutine _coStartBuff;

    float _decreaseWill;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _coAttack = null;
        _coStartBuff = null;
        _decreaseWill = 10f * Time.deltaTime;
    }

    void Update()
    {
        if (Managers.GameManager.CurGameState == GameState.Fight)
        {
            _coStartBuff = _coStartBuff ?? StartCoroutine(Managers.SkillManager.CoStartBuff());

            _coAttack = _coAttack ?? StartCoroutine(TakeDamage());

            //���� ����
            Managers.PlayerManager.ChangeStatus((int)StatusType.Will, _decreaseWill);
        }
        else
        {
            _coStartBuff = null;

            if (_coAttack != null)
            {
                StopCoroutine(_coAttack);
                _coAttack = null;
            }
        }
    }

    IEnumerator TakeDamage()
    {
        _playerStatus = Managers.PlayerManager.CurStatusList;

        //TODO : ���� �ִϸ��̼� ���

        float attackTime = 1 / _playerStatus[(int)StatusType.ATKSpeed];
        yield return new WaitForSeconds(attackTime);

        float attackDamage = _playerStatus[(int)StatusType.ATKDamage];
        if (Managers.Utils.RandomSuccess(_playerStatus[(int)StatusType.CritRate] * 0.01f))
        {
            attackDamage *= (1 + (_playerStatus[(int)StatusType.CritDamage] * 0.01f));
        }
        Managers.RockManager.OnGetDamageEvent?.Invoke(attackDamage);
        Managers.SkillManager.HitBuff();

        _coAttack = null;
    }
}
