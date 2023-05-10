using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class Coins : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOScale(1, 0.5f).SetDelay(0f).SetEase(Ease.Flash);
        transform.GetComponent<RectTransform>().DOAnchorPos(UIController.singletone.CoinsBar.anchoredPosition, 1f).SetDelay(0.5f).SetEase(Ease.Flash);
        Debug.Log(UIController.singletone.CoinsBar.position);
        transform.DOScale(0, 1f).SetDelay(1.5f).SetEase(Ease.Flash);
        StartCoroutine(ReturnInPool());
    }

    private IEnumerator ReturnInPool()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}