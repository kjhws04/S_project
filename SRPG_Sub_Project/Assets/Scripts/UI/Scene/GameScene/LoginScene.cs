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

    enum Texts { LodingTxt }

    private void Start()
    {
        Util.ChangeResolution();

        Init();
        Managers.Fire.Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        Bind<TextMeshProUGUI>(typeof(Texts));
        //Managers.Fire.LoginStat += OnChangedStat;

        //우측 하단 텍스트 초기화
        GetTextMeshProUGUI((int)Texts.LodingTxt).text = "";

        // 콜백 추가 (Fire Manager의 메시지를 받아 출력)
        Managers.Fire.OnTextChanged += text => ChangeText(text);
        Managers.Sound.Play("BGM_01", Define.Sound.Bgm, 1f);
    }

    public override void Clear()
    {
        // 씬 이동 후 콜백 제거
        Managers.Fire.OnTextChanged -= text => ChangeText(text);
    }

    private void OnChangedStat(bool sign)
    {
        outputTxt.text = sign ? $"Login : " : $"Logout : ";
        outputTxt.text += Managers.Fire.UserId;
    }

    // <summary>
    // 화면 우측 하단의 텍스트 변화 (Firebase Manager - ChangeText())
    // <summary>
    public void ChangeText(string text)
    {
        GetTextMeshProUGUI((int)Texts.LodingTxt).text = text;
    }

    #region Buttons
    // 회원 가입 버튼
    public void Register_Btn()
    {
        Managers.Fire.Register(em.text, pw.text);
    }

    // 로그인 버튼
    public void Login_Btn()
    {
        Managers.Fire.Login(em.text, pw.text);
    }

    // 로그아웃 버튼
    public void Logout_Btn()
    {
        Managers.Fire.Logout();
    }
    #endregion
}
