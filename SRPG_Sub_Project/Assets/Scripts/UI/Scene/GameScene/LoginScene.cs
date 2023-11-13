using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading.Tasks;

public class LoginScene : BaseScene
{
    public InputField em;
    public InputField pw;
    public TextMeshPro outputTxt;

    enum Texts { UserID }
    private void Start()
    {
        Init();
        Managers.Fire.Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        Bind<TextMeshProUGUI>(typeof(Texts));
        if (Managers.Fire.user != null)
        {
            GetTextMeshProUGUI((int)Texts.UserID).text = $"ID : {Managers.Fire.user.UserId}";
        }
        //Managers.Fire.LoginStat += OnChangedStat;
    }

    public override void Clear()
    {

    }

    private void OnChangedStat(bool sign)
    {
        outputTxt.text = sign ? $"Login : " : $"Logout : ";
        outputTxt.text += Managers.Fire.UserId;
    }

    #region Buttons
    // ȸ�� ���� ��ư
    public void Register_Btn()
    {
        Managers.Fire.Register(em.text, pw.text);
    }

    // �α��� ��ư
    public void Login_Btn()
    {
        Managers.Fire.Login(em.text, pw.text);
    }

    // �α׾ƿ� ��ư
    public void Logout_Btn()
    {
        Managers.Fire.Logout();
    }
    #endregion
}
