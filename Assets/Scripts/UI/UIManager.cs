using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;


public class UIManager : MonoBehaviour
{

  [Header("Menu UI")]
  [SerializeField]
  private Button Info_Button;

  [Header("Popus UI")]
  [SerializeField]
  private GameObject MainPopup_Object;

  [Header("info Popup")]
  [SerializeField]
  private GameObject PaytablePopup_Object;
  [SerializeField]
  private Button PaytableExit_Button;
  [SerializeField]
  private Button Next_Button;
  [SerializeField]
  private Button Previous_Button;
  private int paginationCounter = 1;
  [SerializeField] private GameObject[] PageList;
  [SerializeField] private Button[] paginationButtonGrp;
  [SerializeField] private Button Infoback_button;
  [SerializeField] private TMP_Text[] SymbolsText;
  [SerializeField] private TMP_Text Bonus_Text;
  [SerializeField] private TMP_Text Wild_Text;

  [Header("Settings Popup")]
  [SerializeField] private Button Setting_button;
  [SerializeField] private Button SettingExit_button;
  [SerializeField] private Button Setting_back_button;
  [SerializeField] private GameObject Setting_panel;
  [SerializeField] private Slider Sound_slider;
  [SerializeField] private Slider Music_slider;

  [Header("Splash Screen")]
  [SerializeField]
  private GameObject Loading_Object;
  [SerializeField]
  private Image Loading_Image;
  [SerializeField]
  private TMP_Text LoadPercent_Text;
  [SerializeField]
  private TMP_Text Loading_Text;

  [Header("LowBalance Popup")]
  [SerializeField]
  private Button LBExit_Button;
  [SerializeField]
  private Button LBBack_Button;
  [SerializeField]
  private GameObject LBPopup_Object;

  [Header("Disconnection Popup")]
  [SerializeField]
  private Button CloseDisconnect_Button;
  [SerializeField]
  private GameObject DisconnectPopup_Object;

  [Header("Disconnection Popup")]

  [SerializeField]
  private GameObject ReconectingPopup_Object;

  [Header("AnotherDevice Popup")]
  [SerializeField]
  private Button CloseAD_Button;
  [SerializeField]
  private GameObject ADPopup_Object;

  [Header("Quit Popup")]
  [SerializeField]
  private GameObject QuitPopup_Object;
  [SerializeField] private Button YesQuit_Button;
  [SerializeField] private Button BonusQuit_Button;
  [SerializeField]
  private Button NoQuit_Button;
  [SerializeField]
  private Button CrossQuit_Button;
  [SerializeField]
  private Button BackQuit_Button;
  [SerializeField]
  private Button Splash_Quit_Button;

  [Header("Megawin Popup")]
  [SerializeField] private GameObject megawin;
  [SerializeField] private TMP_Text megawin_text;
  [SerializeField] private Image Win_Image;
  [SerializeField] private Sprite HugeWin_Sprite;
  [SerializeField] private Sprite BigWin_Sprite;
  [SerializeField] private Sprite MegaWin_Sprite;
  [SerializeField] private Button MegaWinHideBtn;
  private Tween TextTween, WinTween;

  [Header("Audio")]
  [SerializeField] private AudioController audioController;

  [SerializeField]
  private Button GameExit_Button;

  [SerializeField]
  private SlotBehaviour slotManager;

  [SerializeField]
  private SocketIOManager socketManager;
  private bool isExit = false;
  [SerializeField] internal GameObject RaycastBlocker;

  private void Start()
  {
    if (Info_Button) Info_Button.onClick.RemoveAllListeners();
    if (Info_Button) Info_Button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

    if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
    if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); GoToPage(0); });

    if (Next_Button) Next_Button.onClick.RemoveAllListeners();
    if (Next_Button) Next_Button.onClick.AddListener(delegate { TurnPage(true); });

    if (Previous_Button) Previous_Button.onClick.RemoveAllListeners();
    if (Previous_Button) Previous_Button.onClick.AddListener(delegate { TurnPage(false); });

    if (Previous_Button) Previous_Button.interactable = false;

    if (paginationButtonGrp[0]) paginationButtonGrp[0].onClick.RemoveAllListeners();
    if (paginationButtonGrp[0]) paginationButtonGrp[0].onClick.AddListener(delegate { GoToPage(0); });

    if (paginationButtonGrp[1]) paginationButtonGrp[1].onClick.RemoveAllListeners();
    if (paginationButtonGrp[1]) paginationButtonGrp[1].onClick.AddListener(delegate { GoToPage(1); });

    if (paginationButtonGrp[2]) paginationButtonGrp[2].onClick.RemoveAllListeners();
    if (paginationButtonGrp[2]) paginationButtonGrp[2].onClick.AddListener(delegate { GoToPage(2); });

    if (paginationButtonGrp[3]) paginationButtonGrp[3].onClick.RemoveAllListeners();
    if (paginationButtonGrp[3]) paginationButtonGrp[3].onClick.AddListener(delegate { GoToPage(3); });

    if (paginationButtonGrp[4]) paginationButtonGrp[4].onClick.RemoveAllListeners();
    if (paginationButtonGrp[4]) paginationButtonGrp[4].onClick.AddListener(delegate { GoToPage(4); });

    if (paginationButtonGrp[5]) paginationButtonGrp[5].onClick.RemoveAllListeners();
    if (paginationButtonGrp[5]) paginationButtonGrp[5].onClick.AddListener(delegate { GoToPage(5); });

    if (Infoback_button) Infoback_button.onClick.RemoveAllListeners();
    if (Infoback_button) Infoback_button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

    if (Setting_button) Setting_button.onClick.RemoveAllListeners();
    if (Setting_button) Setting_button.onClick.AddListener(delegate { OpenPopup(Setting_panel); });

    if (Sound_slider) Sound_slider.onValueChanged.RemoveAllListeners();
    if (Sound_slider) Sound_slider.onValueChanged.AddListener(delegate { ChangeSound(); });

    if (Music_slider) Music_slider.onValueChanged.RemoveAllListeners();
    if (Music_slider) Music_slider.onValueChanged.AddListener(delegate { ChangeMusic(); });

    if (SettingExit_button) SettingExit_button.onClick.RemoveAllListeners();
    if (SettingExit_button) SettingExit_button.onClick.AddListener(delegate { ClosePopup(Setting_panel); });

    if (Setting_back_button) Setting_back_button.onClick.RemoveAllListeners();
    if (Setting_back_button) Setting_back_button.onClick.AddListener(delegate { ClosePopup(Setting_panel); });

    if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
    if (GameExit_Button) GameExit_Button.onClick.AddListener(delegate { OpenPopup(QuitPopup_Object); });

    if (Splash_Quit_Button) Splash_Quit_Button.onClick.RemoveAllListeners();
    if (Splash_Quit_Button) Splash_Quit_Button.onClick.AddListener(delegate { OpenPopup(QuitPopup_Object); });

    if (NoQuit_Button) NoQuit_Button.onClick.RemoveAllListeners();
    if (NoQuit_Button) NoQuit_Button.onClick.AddListener(delegate { ClosePopup(QuitPopup_Object); });

    if (CrossQuit_Button) CrossQuit_Button.onClick.RemoveAllListeners();
    if (CrossQuit_Button) CrossQuit_Button.onClick.AddListener(delegate { if (!isExit) { ClosePopup(QuitPopup_Object); } });

    if (BackQuit_Button) BackQuit_Button.onClick.RemoveAllListeners();
    if (BackQuit_Button) BackQuit_Button.onClick.AddListener(delegate { if (!isExit) { ClosePopup(QuitPopup_Object); } });

    if (LBExit_Button) LBExit_Button.onClick.RemoveAllListeners();
    if (LBExit_Button) LBExit_Button.onClick.AddListener(delegate { ClosePopup(LBPopup_Object); });

    if (LBBack_Button) LBBack_Button.onClick.RemoveAllListeners();
    if (LBBack_Button) LBBack_Button.onClick.AddListener(delegate { ClosePopup(LBPopup_Object); });

    if (YesQuit_Button) YesQuit_Button.onClick.RemoveAllListeners();
    if (YesQuit_Button) YesQuit_Button.onClick.AddListener(CallOnExitFunction);

    if (BonusQuit_Button) BonusQuit_Button.onClick.RemoveAllListeners();
    if (BonusQuit_Button) BonusQuit_Button.onClick.AddListener(CallOnExitFunction);

    if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.RemoveAllListeners();
    if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.AddListener(delegate { CallOnExitFunction(); socketManager.ReactNativeCallOnFailedToConnect(); }); //BackendChanges

    if (MegaWinHideBtn) MegaWinHideBtn.onClick.RemoveAllListeners();
    if (MegaWinHideBtn) MegaWinHideBtn.onClick.AddListener(OnClickMegaWinHide);

  }

  internal void LowBalPopup()
  {
    OpenPopup(LBPopup_Object);
  }

  internal void DisconnectionPopup()
  {
    if (!isExit)
    {
      OpenPopup(DisconnectPopup_Object);
    }
  }


  internal void PopulateWin(int type, double amount)
  {
    double initAmount = 0;
    double originalAmount = amount;
    switch (type)
    {
      case 1:
        if (Win_Image) Win_Image.sprite = BigWin_Sprite;
        break;
      case 2:
        if (Win_Image) Win_Image.sprite = HugeWin_Sprite;
        break;
      case 3:
        if (Win_Image) Win_Image.sprite = MegaWin_Sprite;
        break;
    }
    if (megawin) megawin.SetActive(true);
    if (MainPopup_Object) MainPopup_Object.SetActive(true);

    TextTween = DOTween.To(() => initAmount, (val) => initAmount = val, amount, 1f).OnUpdate(() =>
    {
      if (megawin_text) megawin_text.text = initAmount.ToString("f2");
    });

    WinTween = DOVirtual.DelayedCall(3.5f, () =>
    {
      if (MainPopup_Object) MainPopup_Object.SetActive(false);
      if (megawin) megawin.SetActive(false);
      if (megawin_text) megawin_text.text = "0";
      slotManager.CheckPopups = false;

    });
  }
  private void OnClickMegaWinHide()
  {
    TextTween?.Kill();
    WinTween?.Kill();


    if (MainPopup_Object) MainPopup_Object.SetActive(false);
    if (megawin) megawin.SetActive(false);
    if (megawin_text) megawin_text.text = "0";
    slotManager.CheckPopups = false;
  }

  internal void ADfunction()
  {
    OpenPopup(ADPopup_Object);
  }

  private void CallOnExitFunction()
  {
    isExit = true;
    audioController.PlayButtonAudio();
    slotManager.CallCloseSocket();
  }

  internal void InitialiseUIData(Paylines symbolsText)
  {
    PopulateSymbolsPayout(symbolsText);
  }

  private void PopulateSymbolsPayout(Paylines paylines)
  {
    for (int i = 0; i < SymbolsText.Length; i++)
    {
      string text = null;
      if (paylines.symbols[i].multiplier[0] != 0)
      {
        text += "5x - " + paylines.symbols[i].multiplier[0] + "x";
      }
      if (paylines.symbols[i].multiplier[1] != 0)
      {
        text += "\n4x - " + paylines.symbols[i].multiplier[1] + "x";
      }
      if (paylines.symbols[i].multiplier[2] != 0)
      {
        text += "\n3x - " + paylines.symbols[i].multiplier[2] + "x";
      }
      if (SymbolsText[i]) SymbolsText[i].text = text;
    }

    for (int i = 0; i < paylines.symbols.Count; i++)
    {
      if (paylines.symbols[i].name.ToUpper() == "BONUS")
      {
        if (Bonus_Text) Bonus_Text.text = paylines.symbols[i].description.ToString();
      }
      if (paylines.symbols[i].name.ToUpper() == "WILD")
      {
        if (Wild_Text) Wild_Text.text = paylines.symbols[i].description.ToString();
      }
    }
  }

  private void OpenPopup(GameObject Popup)
  {
    if (audioController) audioController.PlayButtonAudio();
    if (Popup) Popup.SetActive(true);
    if (MainPopup_Object) MainPopup_Object.SetActive(true);
  }

  private void ClosePopup(GameObject Popup)
  {
    if (audioController) audioController.PlayButtonAudio();

    if (Popup) Popup.SetActive(false);
    if (!DisconnectPopup_Object.activeSelf)
    {
      if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }
  }

  internal void ReconnectionPopup()
  {
    OpenPopup(ReconectingPopup_Object);
  }

  internal void CheckAndClosePopups()
  {
    if (ReconectingPopup_Object.activeInHierarchy)
    {
      ClosePopup(ReconectingPopup_Object);
    }
    if (DisconnectPopup_Object.activeInHierarchy)
    {
      ClosePopup(DisconnectPopup_Object);
    }
  }

  private void TurnPage(bool type)
  {
    if (audioController) audioController.PlayButtonAudio();

    if (type)
      paginationCounter++;
    else
      paginationCounter--;

    GoToPage(paginationCounter - 1);
  }

  private void GoToPage(int index)
  {
    paginationCounter = index + 1;
    paginationCounter = Mathf.Clamp(paginationCounter, 1, 6);
    if (Next_Button) Next_Button.interactable = !(paginationCounter >= 6);

    if (Previous_Button) Previous_Button.interactable = !(paginationCounter <= 1);

    for (int i = 0; i < PageList.Length; i++)
    {
      PageList[i].SetActive(false);
    }

    for (int i = 0; i < paginationButtonGrp.Length; i++)
    {
      paginationButtonGrp[i].interactable = true;
      paginationButtonGrp[i].transform.GetChild(0).gameObject.SetActive(false);
    }

    PageList[paginationCounter - 1].SetActive(true);
    paginationButtonGrp[paginationCounter - 1].interactable = false;
    paginationButtonGrp[paginationCounter - 1].transform.GetChild(0).gameObject.SetActive(true);
  }

  private void ChangeSound()
  {
    audioController.ChangeVolume("wl", Sound_slider.value);
    audioController.ChangeVolume("button", Sound_slider.value);

  }

  private void ChangeMusic()
  {
    audioController.ChangeVolume("bg", Music_slider.value);

  }
}
