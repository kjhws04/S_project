using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// <surmmary>
// 채팅 텍스트 박스 슬롯
// </surmmary>
public class MessageBox : MonoBehaviour
{
    public TextMeshProUGUI user;
    public TextMeshProUGUI message;

    public void SetMessage(string _user, string _message)
    {
        user.text = _user;
        message.text = _message;
    }
}
