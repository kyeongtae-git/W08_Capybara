using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetSkillImage : MonoBehaviour
{
    private Dictionary<string, Sprite> _spriteMap;
    Sprite _defaultSprite;

    void OnEnable()
    {
        LoadSkillImeage();
    }

    void LoadSkillImeage()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("LYR/Sprites");
        
        _spriteMap = new Dictionary<string, Sprite>();
        foreach (var sp in sprites)
        {
            _spriteMap[sp.name] = sp;
            Debug.Log("등록된 스프라이트" + sp.name);
        }
    }

    public Sprite GetSprite(string spriteName)
    {
        if (_spriteMap != null && _spriteMap.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }
}
