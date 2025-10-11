using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    [SerializeField]
    private Image MyImg;

    internal void SetBg(Sprite MySprite)
    {
        if (MyImg)
        {
            MyImg.sprite = MySprite;
            MyImg.gameObject.SetActive(true);
        } 
    }

    internal void ResetBG()
    {
        if (MyImg)
        {
            MyImg.gameObject.SetActive(false);
        }
    }
}
