using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance => _instance;
    static Managers _instance;

    public static GameManager GameManager => _gameManager;
    static GameManager _gameManager = new GameManager();

    public static PoolManager PoolManager => _poolManager;
    static PoolManager _poolManager = new PoolManager();

    public static SoundManager SoundManager => _soundManager;
    static SoundManager _soundManager = new SoundManager();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GameManager.Init();
        PoolManager.Init();
        SoundManager.Init();
    }
}
