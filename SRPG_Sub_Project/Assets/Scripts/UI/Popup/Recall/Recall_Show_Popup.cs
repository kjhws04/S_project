using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Recall_Show_Popup : UI_Popup
{
    public bool isShowing;
    public Image[] images;

    public override void Init()
    {
        base.Init();
        isShowing = true;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
        #region Bind
        #endregion
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
            ("Bronze DG", 200),
            ("Iron Sword", 200),
            ("Old Spear", 200),
            ("Old Staff", 200),
            ("Wood Shield", 200),

            //2stars(10%)
            ("Axe", 500),
            ("Magic Spear", 500),

            //3stars(20%)
            ("Bronze Shield", 500),
            ("Dagger", 500),
            ("Ice Sword", 500),
            ("Wind Staff", 500),

            //4stars(12%)
            ("Amethyst Staff", 300),
            ("Battle Axe", 300),
            ("Ice Spear", 300),
            ("Magic Bow", 300),

            //5stars(4%)
            ("Ancient Bow", 100),
            ("Flame Axe", 100),
            ("King's Sword", 100),
            ("Stiletto", 100),

            //Character
            //1stars(10%)
            ("Aqua", 1000),

            //2stars(10%)
            ("Zion", 1000),

            //3stars(20%)
            ("Dmitry", 500),
            ("Elie", 500),
            ("Mars", 500),
            ("Sakura", 500),

            //4stars(3%)
            ("Shy", 150),
            ("Van", 150),

            //5stars(1%)
            ("Broyna", 50),
            ("Kan", 50)
        #endregion
        );

        for (int i = 0; i < recallTime; i++)
        {
            images[i].gameObject.SetActive(true);
            images[i].sprite = Managers.Resource.SpriteLoad(wRecall.GetRandomPick());
            Debug.Log($"{images[i].sprite.name}");
        }
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
            ("Bronze Shield", 750),
            ("Dagger", 750),
            ("Ice Sword", 750),
            ("Wind Staff", 750),

            //4stars(20%)
            ("Amethyst Staff", 500),
            ("Battle Axe", 500),
            ("Ice Spear", 500),
            ("Magic Bow", 500),

            //5stars(6%)
            ("Ancient Bow", 150),
            ("Flame Axe", 150),
            ("King's Sword", 150),
            ("Stiletto", 150),

            //3stars(30%)
            ("Dmitry", 750),
            ("Elie", 750),
            ("Mars", 750),
            ("Sakura", 750),

            //4stars(10%)
            ("Shy", 500),
            ("Van", 500),

            //5stars(4%)
            ("Knight", 400)
        #endregion
        );

        for (int i = 0; i < recallTime; i++)
        {
            images[i].gameObject.SetActive(true);
            images[i].sprite = Managers.Resource.SpriteLoad(wRecall.GetRandomPick());
            Debug.Log($"{images[i].sprite.name}");
        }
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
            ("Bronze DG", 200),
            ("Iron Sword", 200),
            ("Old Spear", 200),
            ("Old Staff", 200),
            ("Wood Shield", 200),

            //2stars(16%)
            ("Axe", 800),
            ("Magic Spear", 800),

            //3stars(30%)
            ("Bronze Shield", 750),
            ("Dagger", 750),
            ("Ice Sword", 750),
            ("Wind Staff", 750),

            //Character
            //1stars(10%)
            ("Aqua", 1000),

            //2stars(10%)
            ("Zion", 1000),

            //3stars(24%)
            ("Dmitry", 600),
            ("Elie", 600),
            ("Mars", 600),
            ("Sakura", 600)
        #endregion
        );

        for (int i = 0; i < recallTime; i++)
        {
            images[i].gameObject.SetActive(true);
            images[i].sprite = Managers.Resource.SpriteLoad(wRecall.GetRandomPick());
            Debug.Log($"{images[i].sprite.name}");
        }
        StartCoroutine(WaitingSecond(2));
        isShowing = false;
    }

    IEnumerator WaitingSecond(int sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
