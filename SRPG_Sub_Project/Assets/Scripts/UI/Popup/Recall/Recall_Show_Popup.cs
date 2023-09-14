using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Recall_Show_Popup : UI_Popup
{
    public bool isShowing;
    public Image[] images;
    public UserData _userData;

    private void Start()
    {
        Init();
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
    }

    public override void Init()
    {
        base.Init();
        isShowing = false;
    }

    public void BtnExitShowPopup()
    {
        if (isShowing)
            return;

        Managers.UI.ClosePopupUI();
    }

    //Åë»ó °¡Ã­
    public void NormalRecall(int recallTime)
    {
        var wRecall = new Gacha<string>();
        wRecall.Add(
        #region Percent List
            //Weapon
            //1stars(10%)
            ("Bronze DG", 200, false),
            ("Iron Sword", 200, false),
            ("Old Spear", 200, false),
            ("Old Staff", 200, false),
            ("Wood Shield", 200, false),

            //2stars(10%)
            ("Axe", 500, false),
            ("Magic Spear", 500, false),

            //3stars(20%)
            ("Bronze Shield", 500, false),
            ("Dagger", 500, false),
            ("Ice Sword", 500, false),
            ("Wind Staff", 500, false),

            //4stars(12%)
            ("Amethyst Staff", 300, false),
            ("Battle Axe", 300, false),
            ("Ice Spear", 300, false),
            ("Magic Bow", 300, false),

            //5stars(4%)
            ("Ancient Bow", 100, false),
            ("Flame Axe", 100, false),
            ("King's Sword", 100, false),
            ("Stiletto", 100, false),

            //Character
            //1stars(10%)
            ("Aqua", 1000, true),

            //2stars(10%)
            ("Zion", 1000, true),

            //3stars(20%)
            ("Dmitry", 500, true),
            ("Elie", 500, true),
            ("Mars", 500, true),
            ("Sakura", 500, true),

            //4stars(3%)
            ("Shy", 150, true),
            ("Van", 150, true),

            //5stars(1%)
            ("Broyna", 50, true),
            ("Kan", 50, true)
        #endregion
        );

        RecallCard(wRecall, recallTime);
        StartCoroutine(WaitingSecond(2));
        isShowing = false;
    }

    //½ºÆä¼È °¡Ã­
    public void SpecialRecall(int recallTime)
    {
        var wRecall = new Gacha<string>();
        wRecall.Add(
        #region Percent List
            //Weapon
            //3stars(30%)
            ("Bronze Shield", 750, false),
            ("Dagger", 750, false),
            ("Ice Sword", 750, false),
            ("Wind Staff", 750, false),

            //4stars(20%)
            ("Amethyst Staff", 500, false),
            ("Battle Axe", 500, false),
            ("Ice Spear", 500, false),
            ("Magic Bow", 500, false),

            //5stars(6%)
            ("Ancient Bow", 150, false),
            ("Flame Axe", 150, false),
            ("King's Sword", 150, false),
            ("Stiletto", 150, false),

            //3stars(30%)
            ("Dmitry", 750, true),
            ("Elie", 750, true),
            ("Mars", 750, true),
            ("Sakura", 750, true),

            //4stars(10%)
            ("Shy", 500, true),
            ("Van", 500, true),

            //5stars(4%)
            ("Knight", 400, true)
        #endregion
        );

        RecallCard(wRecall, recallTime);
        StartCoroutine(WaitingSecond(2));
        isShowing = false;
    }

    //Ä£±¸ °¡Ã­
    public void FrendRecall(int recallTime)
    {
        var wRecall = new Gacha<string>();
        wRecall.Add(
        #region Percent List
            //Weapon
            //1stars(10%)
            ("Bronze DG", 200, false),
            ("Iron Sword", 200, false),
            ("Old Spear", 200, false),
            ("Old Staff", 200, false),
            ("Wood Shield", 200, false),

            //2stars(16%)
            ("Axe", 800, false),
            ("Magic Spear", 800, false),

            //3stars(30%)
            ("Bronze Shield", 750, false),
            ("Dagger", 750, false),
            ("Ice Sword", 750, false),
            ("Wind Staff", 750, false),

            //Character
            //1stars(10%)
            ("Aqua", 1000, true),

            //2stars(10%)
            ("Zion", 1000, true),

            //3stars(24%)
            ("Dmitry", 600, true),
            ("Elie", 600, true),
            ("Mars", 600, true),
            ("Sakura", 600, true)
        #endregion
        );

        RecallCard(wRecall, recallTime);
        StartCoroutine(WaitingSecond(2));
        isShowing = false;
    }

    //Ä«µå ¼ÒÈ¯ ºÎºÐ
    private void RecallCard(Gacha<string> wRecall, int recallTime)
    {

        for (int i = 0; i < recallTime; i++)
        {
            images[i].gameObject.SetActive(true);
            string pickUpName = wRecall.GetRandomPick();

            if (wRecall.CheckIsChar(pickUpName))
            {
                _userData = Managers.Game.GetUserData().GetComponent<UserData>();
                GameObject go = Managers.Resource.SaveCharData(pickUpName);
                Stat _statData = go.GetComponent<Stat>();

                images[i].sprite = _statData.cardImage;
                _userData.AddCharater(pickUpName, _statData);
            }
            else
            {
                images[i].sprite = Managers.Resource.SpriteLoad(pickUpName);
            }
        }
    }

    IEnumerator WaitingSecond(int sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
