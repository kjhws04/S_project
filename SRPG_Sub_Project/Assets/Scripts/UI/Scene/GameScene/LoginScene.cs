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

        //���� �ϴ� �ؽ�Ʈ �ʱ�ȭ
        GetTextMeshProUGUI((int)Texts.LodingTxt).text = "";

        // �ݹ� �߰� (Fire Manager�� �޽����� �޾� ���)
        Managers.Fire.OnTextChanged += text => ChangeText(text);
        Managers.Sound.Play("BGM_01", Define.Sound.Bgm, 1f);
    }

    public override void Clear()
    {
        // �� �̵� �� �ݹ� ����
        Managers.Fire.OnTextChanged -= text => ChangeText(text);
    }

    private void OnChangedStat(bool sign)
    {
        outputTxt.text = sign ? $"Login : " : $"Logout : ";
        outputTxt.text += Managers.Fire.UserId;
    }

    // <summary>
    // ȭ�� ���� �ϴ��� �ؽ�Ʈ ��ȭ (Firebase Manager - ChangeText())
    // <summary>
    public void ChangeText(string text)
    {
        GetTextMeshProUGUI((int)Texts.LodingTxt).text = text;
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
