using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserData : MonoBehaviour
{
    [SerializeField] string _userName = "ShyAI5364";
    [SerializeField] int _userLevel = 15;
    [SerializeField] int _heart = 200;
    [SerializeField] int _ticket1 = 100;
    [SerializeField] int _ticket2 = 21;
    [SerializeField] int _ticketFriend = 100;
    public Sprite _modelImg;
    public Sprite _moedlDotImg;
    
    public string UserName { get { return _userName; } set { _userName = value; } }
    public int UserLevel { get { return _userLevel; } set { _userLevel = value; } }
    public int Heart { get { return _heart; } set { _heart = value; } }
    public int Ticket1 { get { return _ticket1; } set { _ticket1 = value; } }
    public int Ticket2 { get { return _ticket2; } set { _ticket2 = value; } }
    public int TicketFriend { get { return _ticketFriend; } set { _ticketFriend = value; } }

    private void Awake()
    {
        GameObject go = Managers.Resource.Instantiate("SPUM_Units/Knight");
        _modelImg = go.GetComponent<Stat>().modelImg;
    }

    public void ChangeModel()
    {
        Debug.Log("TODO");
    }
}
