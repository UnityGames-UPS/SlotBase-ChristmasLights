using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayTextAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform Text_transform;
    [SerializeField]
    private TMP_Text this_Text;
    [SerializeField]
    private BonusController _bonusManager;

    private void OnDisable()
    {
        if (Text_transform) Text_transform.localPosition = new Vector2(Text_transform.localPosition.x, -171);
        if (Text_transform) Text_transform.localScale = new Vector3(0, 0, 0);
    }

    internal void SetText(string mytext)
    {
        if (this_Text) this_Text.text = mytext;
        AnimateText();
    }

    private void AnimateText()
    {
        DOVirtual.DelayedCall(0.2f, () =>
        { 
            if (Text_transform) Text_transform.DOLocalMoveY(171, 0.5f);
            if (Text_transform) Text_transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            if (!_bonusManager.isGameOver)
            {
                if (_bonusManager) _bonusManager.enableRayCastPanel(false);
            }
            else
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    _bonusManager.GameOver();
                });
            }
        });
    }
}
