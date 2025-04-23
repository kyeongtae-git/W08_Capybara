using System.Collections;
using UnityEngine;

public class GameManager
{
    public GameState CurGameState => _curGameState;
    GameState _curGameState;

    int _stage;
    int _eventCount = 3;

    public void Init()
    {
        _curGameState = GameState.Idle;
        _stage = 1;
    }

    public void LateInit()
    {
        Managers.UIManager.OnUpdateStageUIEvent?.Invoke(_stage);
    }

    public void GameOver()
    {
        _curGameState = GameState.Idle;
        Debug.Log("������ �ٴڳ����ϴ�.");
    }

    public void GoNextStage()
    {
        _curGameState = GameState.Idle;

        //���� �⺻ ���·� �ʱ�ȭ
        Managers.PlayerManager.Init();
        //���� ��ų ȿ�� �ٽ� ����
        Managers.SkillManager.MaxBuff();

        _stage++;
        Managers.UIManager.OnUpdateStageUIEvent?.Invoke(_stage);
        Debug.Log($"{_stage} ���������� �̵�");

        //�̺�Ʈ �߻����� ��
        if ((_stage % _eventCount) == 0)
        {
            //�̺�Ʈ �߻� UI Ȱ��ȭ (��ų ������ �̺�Ʈ �߻��� ���� ��ư���� ȣ��)
            Managers.UIManager.OnUI_EventCanvasEnableEvent?.Invoke(true);
        }
        //�̺�Ʈ �߻� �� ���� ��
        else
        {
            //��ų ���� UI Ȱ��ȭ
            Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(true);
            Managers.UIManager.OnButton_SelectSkillSetEvent?.Invoke();
        }
    }

    public IEnumerator StartStage()
    {
        //���� �⺻ ���·� �ʱ�ȭ
        Managers.PlayerManager.Init();
        //���� ��ų ȿ�� �ٽ� ����
        Managers.SkillManager.MaxBuff();

        // n�� ���� ���Ĵ� �ִϸ��̼� ���


        // ������ �Ÿ��� �� ����
        Managers.RockManager.Init();
        Vector3 spawnPoint = Managers.RockManager.SpawnPoint;
        Managers.PoolManager.GetPrefabID(0, null, spawnPoint);

        // �� ��� �̵�


        // n�� �Ŀ� �� �տ� �÷��̾� ����
        float moveTime = Managers.RockManager.MoveTime;
        yield return new WaitForSeconds(moveTime);

        // GameState.Fight�� ����
        _curGameState = GameState.Fight;
        Debug.Log($"���� ���� ����� : {_curGameState}");
    }

    public void ChangeGameState(GameState newGameState)
    {
        _curGameState = newGameState;
        Debug.Log($"���� ���� ����� : {_curGameState}");
    }
}
