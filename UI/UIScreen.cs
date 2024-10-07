using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Moevi.Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIScreen : MonoBehaviour
    {
        [Header("Panel Settings")]
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform panel; 
        [SerializeField] private Button button;
        [Space] 
        [SerializeField] private bool closeOnBackground;
    
        [SerializeField, Min(0)] protected float fadeDuration = 0.25f;

        [SerializeField] private UnityEvent onEnable;
        [SerializeField] private UnityEvent onDisable;
    
        protected RectTransform Panel => panel;
        protected RectTransform Background => background;

        private CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();
        
        protected virtual void Awake()
        {
            SwitchOffPanel();

            if (closeOnBackground)
            {
                background.GetComponent<Button>().onClick.AddListener(SwitchOffPanel);
            }
            if (button) button.onClick.AddListener(SwitchOffPanel);
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
        
        

        protected virtual void OnSwitchOn() { }
        protected virtual void OnSwitchOff() { }
    }
}
