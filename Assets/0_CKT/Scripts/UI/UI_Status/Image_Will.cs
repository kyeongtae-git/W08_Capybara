using UnityEngine;
using UnityEngine.UI;

public class Image_Will : MonoBehaviour
{
    Image _willImage;

    void Start()
    {
        _willImage = GetComponent<Image>();
        Managers.UIManager.OnUpdateWillPointUIEvent += UpdateImage;
    }

    void UpdateImage(float cur, float max)
    {
        _willImage.fillAmount = (cur/max);
    }
}
