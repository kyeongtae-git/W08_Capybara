﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public GameState CurGameState => _curGameState;
    GameState _curGameState;

    public int MaxStage => _maxStage;
    int _maxStage = 54;

    public int Stage => _stage;
    int _stage;

    //int _eventCount = 3;

    public void Init()
    {
        _curGameState = GameState.Idle;
        _stage = 1;
    }

    public void GameOver()
    {
        _curGameState = GameState.Idle;
        Debug.Log($"의지가 바닥났습니다. 마지막 스테이지 : {_stage}");
        SceneManager.LoadScene("GameOver");
    }

    public void GoNextStage()
    {
        _curGameState = GameState.Idle;

        //스탯 기본 상태로 초기화
        Managers.PlayerManager.Init(false);

        _stage++;
        int dDay = (_maxStage - _stage) + 1;
        Managers.UIManager.OnUpdateStageUIEvent?.Invoke(dDay);
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
            //게임 클리어 여부
            bool gameClear = (_stage > _maxStage) ? true : false;
            
            //스킬 선택 UI 활성화 (게임 클리어라면 바로 스테이지 시작)
            Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(true, gameClear);
            //Managers.UIManager.OnButton_SelectSkillSetEvent?.Invoke();
        }
    }

    public IEnumerator StartStage()
    {
        //대기는 아니면서 광질 시작은 아닌 상태 (배속 변경 안되게 하는 용도)
        _curGameState = GameState.Ready;

        //스탯 기본 상태로 초기화
        Managers.PlayerManager.Init(false);

        // 적당한 거리에 돌 생성
        //55스테이지라면 에메랄드 바위 생성하기
        Managers.RockManager.Init();
        Vector3 spawnPoint = Managers.RockManager.SpawnPoint;
        int index = (_stage > _maxStage) ? 1 : 0;
        Managers.PoolManager.GetPrefabID(index, null, spawnPoint);

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
