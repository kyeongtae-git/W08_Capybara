using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ParticleSystem _particle;

    Coroutine _coAttack;

    Animator _animator;

    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }

    void OnEnable()
    {
        Init();
    }

    void Init()
    {
        _particle = GetComponentInChildren<ParticleSystem>();

        _coAttack = null;

        _animator = GetComponent<Animator>();
        //_animator.SetBool("isWalk",true);
    }

    void Update()
    {
        if (Managers.GameManager.CurGameState == GameState.Fight)
        {
            _animator.SetBool("isWalk", false);

            _coAttack = _coAttack ?? StartCoroutine(TakeDamage());

            //의지 감소
            Managers.PlayerManager.DecreaseWill();

            if (Managers.PlayerManager.RunOutOfWill())
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
            _coAttack = null;
            //_animator.SetBool("isWalk", true);
        }
    }

    IEnumerator TakeDamage()
    {
        //TODO : 공격 애니메이션 재생
        _animator.SetTrigger("attack");

        //공격속도
        float atkTime = Managers.PlayerManager.GetAttackTime();
        yield return new WaitForSeconds(atkTime);

        //이펙트 재생
        _particle.Play();
        
        float damage = Managers.PlayerManager.GetFinalDamage();
        //데미지 주기
        Managers.RockManager.OnGetDamageEvent?.Invoke(damage);
        //데미지 표시
        Managers.UIManager.OnDamageUIEvent?.Invoke(damage);

        _coAttack = null;
    }
}
