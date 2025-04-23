using UnityEngine;
using UnityEngine.UI;

public class Image_Will : MonoBehaviour
{
    Image _willImage;

    void Start()
    {
        _willImage = GetComponent<Image>();
        Managers.UIManager.OnImage_WillUpdateImageEvent += UpdateImage;
    }

    void UpdateImage(float percent)
    {
        _willImage.fillAmount = percent;
    }
}
