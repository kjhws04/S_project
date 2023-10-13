using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    //A logger to record the enemy actions.
    public class ActionLogger : MonoBehaviour
    {
        public Text textPrefab;
        public GameObject actionWindow;

        public int maxMessages = 25;

        [SerializeField]
        List<Message> messageList = new List<Message>();

        public void ReceiveMessage(string action)
        {
            if (messageList.Count >= maxMessages)
            {
                Destroy(messageList[0].textObject.gameObject);
                messageList.RemoveAt(0);
            }

            Message newMessage = new Message();

            newMessage.text = action;

            Text newText = Instantiate(textPrefab, actionWindow.transform);

            newMessage.textObject = newText;
            newMessage.textObject.text = action;
            messageList.Add(newMessage);
        }
    }

    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;
    }
}
