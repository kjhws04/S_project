using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// <surmmary>
// ä�� �ؽ�Ʈ �ڽ� ����
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
