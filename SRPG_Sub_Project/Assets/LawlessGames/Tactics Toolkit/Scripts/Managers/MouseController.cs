using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

namespace TacticsToolkit
{
    public class MouseController : MonoBehaviour
    {
        public bool Use3DMap = false;
        public CharacterSpawner characterSpawner;
        public GameEventGameObject focusOnNewTile;
        public GameEvent actionButtonEvent;

        private OverlayTile focusedOnTile;

        private void Start()
        {
            focusedOnTile = MapManager.Instance.GetOverlayByTransform(transform.position);
            if (focusedOnTile != null)
            {
                focusOnNewTile.Raise(focusedOnTile.gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                actionButtonEvent.Raise();
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OverlayTile newFocusedOnTile;

            if (!Use3DMap)
                newFocusedOnTile = GetFocusedOnTile2D(mousePos);
            else
                newFocusedOnTile = GetFocusedOnTile3D();

            if (newFocusedOnTile)
            {
                if (Hastooltip(newFocusedOnTile))
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(newFocusedOnTile.transform.position);
                    TooltipManager.Show(newFocusedOnTile.tileData.tooltipName, newFocusedOnTile.tileData.tooltipDescription, screenPos, new Vector2(50, 50));
                }

                if (focusedOnTile != newFocusedOnTile)
                {
                    TooltipManager.Hide();
                    transform.position = newFocusedOnTile.transform.position;
                    focusedOnTile = newFocusedOnTile;

                    if (focusOnNewTile)
                    {
                        focusOnNewTile.Raise(newFocusedOnTile.gameObject);
                    }
                }
            }
        }

        private static bool Hastooltip(OverlayTile newFocusedOnTile) => newFocusedOnTile.tileData && newFocusedOnTile.tileData.hasTooltip && TooltipManager.instance != null;


        //Returns the tile you are currently moused over
        public OverlayTile GetFocusedOnTile2D(Vector3 mousePos)
        {
            Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

            if (hits.Length > 0)
            {
                var firstHit = hits.OrderByDescending(i => i.collider.transform.position.z).First();
                return firstHit.collider.gameObject.GetComponent<OverlayTile>();
            }
            return null;
        }

        //Returns the tile you are currently moused over 3D
        public OverlayTile GetFocusedOnTile3D()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                var renderer = hit.transform.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    return hit.collider.gameObject.GetComponent<OverlayTile>();
                }
            }
            return null;
        }

        public void SetActiveTile(GameObject activeTile)
        {
            focusedOnTile = activeTile.GetComponent<OverlayTile>();
            transform.position = activeTile.transform.position;
        }
    }
}
