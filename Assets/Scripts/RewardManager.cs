using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameObject coinsParent;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private int _coinNo;
    private Vector3[] _initialPos;
    private Quaternion[] _initialRotation;
    private int _allCoins;

    void Start()
    {
        _initialPos = new Vector3[_coinNo];
        _initialRotation = new Quaternion[_coinNo];
        for (int i = 0; i < coinsParent.transform.childCount; i++)
        {
            _initialPos[i] = coinsParent.transform.GetChild(i).position;
            _initialRotation[i] = coinsParent.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < coinsParent.transform.childCount; i++)
        {
            coinsParent.transform.GetChild(i).position = _initialPos[i];
            coinsParent.transform.GetChild(i).rotation = _initialRotation[i];
        }
    }
    //Заменить тут название 
    public void Reward()
    {
        Reset();

        var delay = 0f;
        coinsParent.SetActive(true);

        for (int i = 0; i < coinsParent.transform.childCount; i++)
        {
            coinsParent.transform.GetChild(i).DOScale(1f, 0.5f).SetDelay(delay).SetEase(Ease.Flash);
            coinsParent.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(250, 900), 1f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

            coinsParent.transform.GetChild(i).DOScale(0f, 0.5f).SetDelay(delay + 1.5f).SetEase(Ease.Flash);
            delay += 0.2f;
        }
        StartCoroutine(CounterCoins(1));
    }

    private IEnumerator CounterCoins(int coinNo)
    {
        yield return new WaitForSeconds(1f);
        var timer = 0f;
        for (int i = 1; i <= coinNo; i++)
        {
            _allCoins += 15;
            counter.text = _allCoins.ToString();
            timer += 0.05f;
            yield return new WaitForSeconds(timer);
        }
    }

}
