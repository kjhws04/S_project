using UnityEngine;

namespace TacticsToolkit
{
    //Generic color shifter
    public class ColorOscillator : MonoBehaviour
    {
        public Color color1;
        public Color color2;

        public float speed;

        // Update is called once per frame
        void Update()
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time * speed, 1));
        }
    }
}
