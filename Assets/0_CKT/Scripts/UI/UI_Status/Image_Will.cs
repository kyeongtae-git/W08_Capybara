using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Will : MonoBehaviour
{
    Image _willImage;

    void Start()
    {
        _willImage = GetComponent<Image>();
        Managers.UIManager.OnUpdateStatusUIEvent += UpdateImage;
    }

    void UpdateImage(List<float> list)
    {
        _willImage.fillAmount = list[(int)StatusType.Will];
    }
}
