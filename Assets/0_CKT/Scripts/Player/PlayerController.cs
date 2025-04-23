using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //0 치명타 확률
    //1 치명타 피해
    //2 공격력
    //3 공격 속도
    //4 의지 수치
    //5 스태미나 수치
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

            //의지 감소
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

        //TODO : 공격 애니메이션 재생

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
