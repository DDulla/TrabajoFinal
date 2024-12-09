using UnityEngine;
using DG.Tweening;

public class TitleAnimator : MonoBehaviour
{
    public RectTransform titleTransform;
    public AnimationCurve titleCurve;
    public float duration = 1f;
    public float delay = 1.5f;
    public Vector2 initialPosition; 
    public Vector2 targetPosition; 

    private void Start()
    {
        titleTransform.anchoredPosition = initialPosition;
        Invoke(nameof(AnimateTitle), delay);
    }

    private void AnimateTitle()
    {
        titleTransform.DOAnchorPos(targetPosition, duration).SetEase(titleCurve);
    }
}
