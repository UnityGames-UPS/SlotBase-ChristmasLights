using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBonusGift : MonoBehaviour
{
    [SerializeField]
    private Button this_Button;
    [SerializeField]
    private Image this_GameObject;
    [SerializeField]
    private GameObject selected_GameObject;
    [SerializeField]
    private PlayTextAnimation text_AnimScript;
    [SerializeField]
    private BonusController _bonusManager;
    [SerializeField]
    private SocketIOManager _socketManager;

    int index = 0;
    private void Start()
    {
        if (this_Button) this_Button.onClick.RemoveAllListeners();        
        if (this_Button) this_Button.onClick.AddListener(SelectGift);
    }

    private void SelectGift()
    {
        if (_bonusManager) _bonusManager.enableRayCastPanel(true);

        //_socketManager.OnBonusCollect(index);

        StartCoroutine(DisplayBonusResult(index));
       
    }
    IEnumerator DisplayBonusResult( int graveNo)
    {

        _socketManager.OnBonusCollect(graveNo);
        StartCoroutine(PlayShakeAnimation(gameObject));
        yield return new WaitUntil(() => _socketManager.isResultdone);

        if (_socketManager.bonusData.payload.payout == 0)
        {
            if (_bonusManager) _bonusManager.PlayWinLooseSound(false);
            if (text_AnimScript) text_AnimScript.SetText("No Bonus");


            if (this_GameObject) this_GameObject.enabled = false;
            if (selected_GameObject) selected_GameObject.SetActive(true);
            if (_bonusManager) _bonusManager.enableRayCastPanel(true);
            _bonusManager.isGameOver = true;
            yield return new WaitForSeconds(2f);
            _bonusManager.GameOver();
            yield break;
        }
        if (_bonusManager) _bonusManager.PlayWinLooseSound(true);
        if (text_AnimScript) text_AnimScript.SetText("+" + _socketManager.bonusData.payload.winAmount.ToString("f3"));
        _bonusManager.UpdateTotalText(_socketManager.bonusData.payload.winAmount);
        if (this_GameObject) this_GameObject.enabled = false;
        if (selected_GameObject) selected_GameObject.SetActive(true);


        if (_bonusManager) _bonusManager.enableRayCastPanel(true);
    }


    IEnumerator PlayShakeAnimation(GameObject obj)
    {
        Vector3 originalPos = obj.transform.localPosition;
        float shakeAmount = 5f;
        float shakeSpeed = 50f;

        while (!_socketManager.isResultdone)
        {
            float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            float offsetY = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount;

            obj.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            yield return null;
        }


        obj.transform.localPosition = originalPos;
    }
    internal void ResetGift(int ind)
    {
        index = ind;
        if (selected_GameObject) selected_GameObject.SetActive(false);
        if (this_GameObject) this_GameObject.enabled = true;
    }
}
