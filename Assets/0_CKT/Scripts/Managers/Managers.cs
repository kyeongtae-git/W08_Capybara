using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance => _instance;
    static Managers _instance;

    public static Utils Utils => _utils;
    static Utils _utils = new Utils();

    public static GameManager GameManager => _gameManager;
    static GameManager _gameManager = new GameManager();

    public static PoolManager PoolManager => _poolManager;
    static PoolManager _poolManager = new PoolManager();

    public static UIManager UIManager => _uiManager;
    static UIManager _uiManager = new UIManager();

    public static SoundManager SoundManager => _soundManager;
    static SoundManager _soundManager = new SoundManager();

    public static PlayerManager PlayerManager => _playerManager;
    static PlayerManager _playerManager = new PlayerManager();

    public static RockManager RockManager => _rockManager;
    static RockManager _rockManager = new RockManager();

    public static EventManager EventManager => _eventManager;
    static EventManager _eventManager = new EventManager();

    public static SkillManager SkillManager => _skillManager;
    static SkillManager _skillManager = new SkillManager();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }

        GameManager.Init();
        PoolManager.Init();
        UIManager.Init();
        SoundManager.Init();
        PlayerManager.Init();
        RockManager.Init();
        EventManager.Init();
        SkillManager.Init();
    }
}
