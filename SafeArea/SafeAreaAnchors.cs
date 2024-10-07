using UnityEngine;

namespace Moevi.UI.SafeArea
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaAnchors : MonoBehaviour
    {
        [SerializeField] private ScreenEdgeFlags _edges = ScreenEdgeFlags.None;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            SetAnchors();
        }

        private void SetAnchors()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            if (_edges.HasFlag(ScreenEdgeFlags.Top))
                _rectTransform.anchorMax = new Vector2(_rectTransform.anchorMax.x, anchorMax.y);

            if (_edges.HasFlag(ScreenEdgeFlags.Bottom))
                _rectTransform.anchorMin = new Vector2(_rectTransform.anchorMin.x, anchorMin.y);

            if (_edges.HasFlag(ScreenEdgeFlags.Left))
                _rectTransform.anchorMin = new Vector2(anchorMin.x, _rectTransform.anchorMin.y);

            if (_edges.HasFlag(ScreenEdgeFlags.Right))
                _rectTransform.anchorMax = new Vector2(anchorMax.x, _rectTransform.anchorMax.y);
        }
    }
}