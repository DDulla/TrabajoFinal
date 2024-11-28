using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ScreenTransition : MonoBehaviour
{
    public RectTransform blackScreen;

    private void Start()
    {

    }

    public void InitializeTransition()
    {
        blackScreen.anchoredPosition = Vector2.zero;
        blackScreen.DOAnchorPos(new Vector2(0, Screen.height), 1f).SetEase(Ease.InOutQuad);
    }

    public void StartGame()
    {
        blackScreen.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene("Game");
        });
    }
}
