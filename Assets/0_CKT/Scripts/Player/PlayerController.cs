using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ParticleSystem _particle;

    Coroutine _coAttack;
    Coroutine _coStartBuff;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _particle = GetComponentInChildren<ParticleSystem>();

        _coAttack = null;
        _coStartBuff = null;
    }

    void Update()
    {
        if (Managers.GameManager.CurGameState == GameState.Fight)
        {
            List<float> playerStatusList = Managers.PlayerManager.CurStatusList.ToList();
            float useStamina = Managers.PlayerManager.UseStamina;
            _coStartBuff = _coStartBuff ?? StartCoroutine(Managers.SkillManager.CoStartBuff());
            _coAttack = _coAttack ?? StartCoroutine(TakeDamage(playerStatusList, useStamina));

            //���� ����
            float useWill = Managers.PlayerManager.UseWill;
            Managers.PlayerManager.ChangeStatus((int)StatusType.Will, -useWill * Time.deltaTime);
            if (Managers.PlayerManager.CurStatusList[(int)StatusType.Will] <= 0)
            {
                Managers.GameManager.GameOver();
            }
        }
        else
        {
            if (_coAttack != null)
            {
                StopCoroutine(_coAttack);
                
            }
            _coStartBuff = null;
            _coAttack = null;
        }
    }

    IEnumerator TakeDamage(List<float> playerStatus, float useStamina)
    {
        //TODO : ���� �ִϸ��̼� ���

        float attackTime = 1 / playerStatus[(int)StatusType.ATKSpeed];
        yield return new WaitForSeconds(attackTime);

        float attackDamage = playerStatus[(int)StatusType.ATKDamage];
        if (Managers.Utils.RandomSuccess(playerStatus[(int)StatusType.CritRate] * 0.01f))
        {
            attackDamage *= (1 + (playerStatus[(int)StatusType.CritDamage] * 0.01f));
        }

        if (playerStatus[(int)StatusType.Stamina] <= 0)
        {
            attackDamage *= 0.5f;
        }

        //����Ʈ ���
        _particle.Play();
        //���¹̳� �Ҹ�
        Managers.PlayerManager.ChangeStatus((int)StatusType.Stamina, -useStamina);
        //������ �ֱ�
        Managers.RockManager.OnGetDamageEvent?.Invoke(attackDamage);
        //���� �� ȿ��
        Managers.SkillManager.HitBuff();

        _coAttack = null;
    }
}
