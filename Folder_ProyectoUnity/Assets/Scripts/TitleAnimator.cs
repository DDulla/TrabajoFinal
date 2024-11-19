using UnityEngine;
using DG.Tweening;

public class TitleAnimator : MonoBehaviour
{
    public RectTransform titleTransform; 
    public AnimationCurve titleCurve; 
    public float duration = 1f; 
    public float delay = 1.5f; 

    private Vector2 initialPosition;
    private Vector2 targetPosition; 

    private void Start()
    {
        targetPosition = titleTransform.anchoredPosition;
        initialPosition = new Vector2(targetPosition.x, Screen.height + titleTransform.rect.height);
        titleTransform.anchoredPosition = initialPosition;
        Invoke(nameof(AnimateTitle), delay);
    }

    private void AnimateTitle()
    {
        titleTransform.DOAnchorPos(targetPosition, duration).SetEase(titleCurve);
    }
}
