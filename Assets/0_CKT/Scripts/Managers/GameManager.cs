using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public GameState CurGameState => _curGameState;
    GameState _curGameState;

    int _maxStage = 54;

    public int Stage => _stage;
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
        Debug.Log("의지가 바닥났습니다.");
        SceneManager.LoadScene("GameOver");
    }

    public void GoNextStage()
    {
        _curGameState = GameState.Idle;

        //스탯 기본 상태로 초기화
        Managers.PlayerManager.Init();
        //영구 스킬 효과 다시 적용
        Managers.SkillManager.MaxBuff();

        _stage++;
        Managers.UIManager.OnUpdateStageUIEvent?.Invoke(_stage);
        Debug.Log($"{_stage} 스테이지로 이동");

        //이벤트 발생했을 때
        /*if ((_stage % _eventCount) == 0)
        {
            //이벤트 발생 UI 활성화 (스킬 선택은 이벤트 발생의 선택 버튼에서 호출)
            Managers.UIManager.OnUI_EventCanvasEnableEvent?.Invoke(true);
        }
        //이벤트 발생 안 했을 때
        else*/
        {
            //스킬 선택 UI 활성화
            Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(true);
            Managers.UIManager.OnButton_SelectSkillSetEvent?.Invoke();
        }
    }

    public IEnumerator StartStage()
    {
        //스탯 기본 상태로 초기화
        Managers.PlayerManager.Init();
        //영구 스킬 효과 다시 적용
        Managers.SkillManager.MaxBuff();

        // n초 동안 흙파는 애니메이션 재생


        // 적당한 거리에 돌 생성
        //55스테이지라면 에메랄드 바위 생성하기
        Managers.RockManager.Init();
        Vector3 spawnPoint = Managers.RockManager.SpawnPoint;
        int index = (_stage > _maxStage) ? 1 : 0;
        Managers.PoolManager.GetPrefabID(index, null, spawnPoint);

        // 뒷 배경 이동


        // n초 후에 돌 앞에 플레이어 도착
        float moveTime = Managers.RockManager.MoveTime;
        yield return new WaitForSeconds(moveTime);

        // GameState.Fight로 변경
        //55스테이지라면 LoadScene으로 승리씬 불러오기
        if (_stage > _maxStage)
        {
            //TODO : 타이틀 씬 이름 넣기
            SceneManager.LoadScene("GameClear");
        }
        else
        {
            _curGameState = GameState.Fight;
            Debug.Log($"게임 상태 변경됨 : {_curGameState}");
        }
    }
}
