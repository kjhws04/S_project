using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using UnityEngine.UI;
using System.Linq;

// <summary>
// ���� ȭ�鿡�� ä�� ��ư�� ������ �� popup
// </summary>
public class Chat_Popup : UI_Popup
{
    List<string> receiveKey = new List<string>();

    public InputField input;
    public Transform messageBox;

    private void Start()
    {
        DatabaseReference chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        //chatDB.OrderByChild("timestamp").LimitToLast(1).ValueChanged += ReceiveMessage; 
        chatDB.OrderByChild("timestamp").ValueChanged += ReceiveMessage;
    }

    // <summary>
    // Firebase�� datadase���� ��ȭ�� ���� ��, ����Ǵ� �Լ�
    // </summary>
    public void ReceiveMessage(object sender, ValueChangedEventArgs e) 
    {
        DataSnapshot snapshot = e.Snapshot;
        Debug.Log($"���� �޽��� ��: {snapshot.ChildrenCount}");

        // �����͸� timestamp ������������ ����
        var sortedMessages = snapshot.Children.OrderByDescending(child => (long)child.Child("timestamp").Value).Reverse();

        foreach (var message in sortedMessages)
        {
            Debug.Log($"{message.Key} + {message.Child("username").Value.ToString()} + {message.Child("message").Value.ToString()}");
            //timestamp�� ����ϸ�, 2�� ����� �Ǵµ�(2�� ����� ������), ����� �ѹ��� �ϵ��� �����ϱ� ���� �ڵ�
            if (!receiveKey.Contains(message.Key))
            {
                AddChatMessage(message.Child("username").Value.ToString(), message.Child("message").Value.ToString());
                receiveKey.Add(message.Key);
            }
        }
    }

    // <summary>
    // Firebase�� datadase�� �޾� Firebase�� ChatMessage�� �ִ� ä�� �޽����� ������ �Լ�, BtnSend() ���
    // </summary>
    public void SendChatMessage(string userName, string message)
    {
        DatabaseReference chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        string key = chatDB.Push().Key;

        Dictionary<string, object> chatDic = new Dictionary<string, object>();
        chatDic.Add("username", userName);
        chatDic.Add("message", message);
        chatDic.Add("timestamp", ServerValue.Timestamp);

        Dictionary<string, object> updateChat = new Dictionary<string, object>();
        updateChat.Add(key, chatDic);

        chatDB.UpdateChildrenAsync(updateChat).ContinueWithOnMainThread( task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Send Message");
            }
        });

        input.text = "";
    }

    // <summary>
    // Firebase�� datadase�� �޾� ChatMessage�� ������ ���� �Լ�
    // </summary>
    public void AddChatMessage(string userName, string msg)
    {
        GameObject message = Managers.Resource.Instantiate("Slot/MessageBox");
        message.transform.SetParent(messageBox);
        message.transform.localScale = new Vector3(1, 1, 1);
        MessageBox messageContents = message.GetComponent<MessageBox>();
        messageContents.SetMessage(userName, msg);
    }

    // Buttons
    public void BtnSend()
    {
        string user = Managers.Fire.user.Email;
        string message = input.text;
        SendChatMessage(user, message);
    }
    public void BtnExit()
    {
        Managers.UI.ClosePopupUI();
    }
}