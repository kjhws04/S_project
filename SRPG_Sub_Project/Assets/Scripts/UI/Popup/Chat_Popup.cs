using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using UnityEngine.UI;
using System.Linq;

// <summary>
// 메인 화면에서 채팅 버튼을 눌렸을 때 popup
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
    // Firebase의 datadase에서 변화가 있을 시, 실행되는 함수
    // </summary>
    public void ReceiveMessage(object sender, ValueChangedEventArgs e) 
    {
        DataSnapshot snapshot = e.Snapshot;
        Debug.Log($"받은 메시지 수: {snapshot.ChildrenCount}");

        // 데이터를 timestamp 내림차순으로 정렬
        var sortedMessages = snapshot.Children.OrderByDescending(child => (long)child.Child("timestamp").Value).Reverse();

        foreach (var message in sortedMessages)
        {
            Debug.Log($"{message.Key} + {message.Child("username").Value.ToString()} + {message.Child("message").Value.ToString()}");
            //timestamp를 사용하면, 2번 출력이 되는데(2번 출력이 정상임), 출력을 한번만 하도록 방지하기 위한 코드
            if (!receiveKey.Contains(message.Key))
            {
                AddChatMessage(message.Child("username").Value.ToString(), message.Child("message").Value.ToString());
                receiveKey.Add(message.Key);
            }
        }
    }

    // <summary>
    // Firebase의 datadase를 받아 Firebase의 ChatMessage에 있는 채팅 메시지를 보내는 함수, BtnSend() 사용
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
    // Firebase의 datadase를 받아 ChatMessage에 내용을 띄우는 함수
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