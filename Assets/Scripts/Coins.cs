using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Coins : MonoBehaviour
{
    private Vector2 _coinsPositionUI = new Vector2(245, 890);
    private void OnEnable()
    {
        transform.DOScale(1, 0.5f).SetDelay(0f).SetEase(Ease.Flash);
        transform.GetComponent<RectTransform>().DOAnchorPos(_coinsPositionUI, 1f).SetDelay(0.5f).SetEase(Ease.Flash);
        transform.DOScale(0, 1f).SetDelay(1.5f).SetEase(Ease.Flash);
        StartCoroutine(ReturnInPool());
    }

    private IEnumerator ReturnInPool()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
