using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Moevi.Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIScreen : MonoBehaviour
    {
        [Header("Panel Settings")]
        [SerializeField] protected RectTransform background;
        [SerializeField] protected RectTransform panel; 
        [SerializeField] private Button button;
        [Space] 
        [SerializeField] private bool closeOnBackground;
    
        [SerializeField, Min(0)] protected float fadeDuration = 0.25f;

        [SerializeField] protected UnityEvent onEnable;
        [SerializeField] protected UnityEvent onDisable;
    
        protected RectTransform Panel => panel;
        protected RectTransform Background => background;

        private CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();
        
        protected virtual void Awake()
        {
            RectTransform rect = transform as RectTransform;
            rect.localPosition = Vector3.zero;
            SwitchOffPanel();

            if (closeOnBackground)
            {
//                background.GetComponent<Button>().onClick.AddListener(SwitchOffPanel);
            }
            if (button) button.onClick.AddListener(SwitchOffPanelAsync);
        }

        protected virtual void OnDestroy()
        {
            CanvasGroup.DOKill();
            Panel.DOKill();
            if (button) button.onClick.RemoveListener(SwitchOffPanel);
        }

        public void SwitchOnPanelAsync(Ease fadeEase = Ease.Linear)
        {
            CanvasGroup.DOKill();
            panel.DOKill();

            onEnable?.Invoke();
            panel.localScale = Vector3.zero;
            CanvasGroup.DOFade(1, fadeDuration/2).SetEase(fadeEase);
            panel.DOScale(1, fadeDuration).SetEase(fadeEase).OnComplete(() =>
            {
                CanvasGroup.interactable = true;
                CanvasGroup.blocksRaycasts = true;
                OnSwitchOn();
                CanvasGroup.DOKill();
                panel.DOKill();

            });
        }
        
        public void SwitchOnPanelAsync()
        {
            CanvasGroup.DOKill();
            panel.DOKill();
            onEnable?.Invoke();
            panel.localScale = Vector3.one * 0.6f;
            CanvasGroup.interactable = true;
            CanvasGroup.DOFade(1, fadeDuration / 2).SetEase(Ease.Linear);
            panel.DOScale(1, fadeDuration).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                CanvasGroup.blocksRaycasts = true;
                OnSwitchOn();
                CanvasGroup.DOKill();
                panel.DOKill();
            });
            onEnable?.Invoke();
        }
        
        public void SwitchOffPanelAsync()
        {
            CanvasGroup.DOKill();
            panel.DOKill();
            
            onDisable?.Invoke();
            CanvasGroup.DOFade(0, fadeDuration).SetEase(Ease.Linear);
            panel.DOScale(0, fadeDuration/2).SetEase(Ease.InOutElastic).OnComplete(SwitchOffPanel);
        }
        
        public void SwitchOnPanel()
        {
            onEnable?.Invoke();
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.alpha = 1;
            OnSwitchOn();
        }

        public void SwitchOffPanel()
        {
            CanvasGroup?.DOKill();
            Panel?.DOKill();
            onDisable?.Invoke();
            OnSwitchOff();
            CanvasGroup.alpha = 0;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }
        
        public static void RefreshLayoutGroupsImmediateAndRecursive(GameObject root)
        {
            var componentsInChildren = root.GetComponentsInChildren<LayoutGroup>(true);
            foreach (var layoutGroup in componentsInChildren)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
            }

            var parent = root.GetComponent<LayoutGroup>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(parent.GetComponent<RectTransform>());
        }

        protected virtual void OnSwitchOn() { }
        protected virtual void OnSwitchOff() { }
    }
}
