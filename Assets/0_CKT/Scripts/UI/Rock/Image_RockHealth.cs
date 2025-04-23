using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_RockHealth : MonoBehaviour
{
    Image _rockHealthImage;

    void Start()
    {
        _rockHealthImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Managers.UIManager.OnUpdateRockHealthUIEvent += UpdateImage;
    }

    private void OnDisable()
    {
        Managers.UIManager.OnUpdateRockHealthUIEvent -= UpdateImage;
    }

    void UpdateImage(float percent)
    {
        _rockHealthImage.fillAmount = percent;
    }
}
