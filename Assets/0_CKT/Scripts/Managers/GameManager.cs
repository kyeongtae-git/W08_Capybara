using UnityEngine;

public class GameManager
{
    int _stage;

    public void Init()
    {
        _stage = 1;
    }

    public void GoNextStage()
    {
        
        _stage++;
    }
}
