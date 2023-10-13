using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    public class ButtonStateManager : MonoBehaviour
    {
        public void DisableOnEvent()
        {
            GetComponent<Button>().interactable = false;
        }

        public void EnableOnEvent()
        {
            GetComponent<Button>().interactable = true;
        }

        public void OnMouseOver()
        {
            
        }
    }
}
