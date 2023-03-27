using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button sickle;
    [SerializeField] private TextMeshProUGUI counterCoins;
    [SerializeField] private TextMeshProUGUI counterBlocks;
    [SerializeField] private Image coinsBar;

    [SerializeField] private int _durationShake;
    [SerializeField] private int _strengthShake;
    [SerializeField] private int _vibrateShake;

    private Camera _cam;
    private int _currentAccount;
    private int _currentWeatBlocks;

    public bool limitBlocks { get; private set; }


    private void Awake()
    {
        _cam = Camera.main;
    }
    private void Start()
    {
        counterBlocks.text = ($"{_currentWeatBlocks} / 40 ");
    }
    public void ShowSickle(Vector3 wheatPosition, Action harvest)
    {
        sickle.onClick.AddListener(() => harvest.Invoke());
        sickle.gameObject.SetActive(true);
        sickle.gameObject.transform.position = _cam.WorldToScreenPoint(wheatPosition);
    }

    public void HideSickle() => sickle.gameObject.SetActive(false);

    public void CounterCoins(int counter)
    {
        StartCoroutine(DelayCountMoney(counter));
    }

    public void CounterWheatBlocks(int counter)
    {
        _currentWeatBlocks += counter;
        if (_currentWeatBlocks > 40)
        {
            limitBlocks = true;
            return;
        }
        else
        {
            limitBlocks = false;
        }
        counterBlocks.text = ($"{_currentWeatBlocks} / 40 ");
    }

    private IEnumerator DelayCountMoney(int counter)
    {
        for (int i = 0; i < counter; i++)
        {
            ShakeCoinsBar();
            _currentAccount += 1;
            counterCoins.text = _currentAccount.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ShakeCoinsBar()
    {
        coinsBar.rectTransform.DOShakePosition(_durationShake, _strengthShake, _vibrateShake);
    }
}
