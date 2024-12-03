using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ScreenTransition : MonoBehaviour
{
    public RectTransform blackScreen;
    public Vector2 startPos;
    public Vector2 endPos;
    public float transitionDuration = 1f;

    public void InitializeTransition()
    {
        blackScreen.anchoredPosition = startPos;
        blackScreen.DOAnchorPos(endPos, transitionDuration).SetEase(Ease.InOutQuad);
    }

    public void StartGame()
    {
        blackScreen.DOAnchorPos(startPos, transitionDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene("Game");
        });
    }
}
