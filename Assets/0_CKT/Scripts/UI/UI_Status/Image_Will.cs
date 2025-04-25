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

    void UpdateImage(float amount)
    {
        _willImage.fillAmount = amount;
    }
}
