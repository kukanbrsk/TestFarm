using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Backpack : MonoBehaviour
{
    [SerializeField] private Stack<BlockWheat> towerOfWeatBlocks = new Stack<BlockWheat>();
    [SerializeField] private UIController uIController;
    [SerializeField] private BlockWheat prefab;

    private Coroutine _delaySellCoroutine;
 
    private void Start()
    {
        GetSaveBlocks(SaveManager.Singleton.GetBlocksCount());
        Debug.Log(SaveManager.Singleton.GetBlocksCount());
    }

    public void AddBlockToBackpack(BlockWheat wheat)
    {
        var targetPosition = Vector3.up * 0.1f * towerOfWeatBlocks.Count;
        wheat.transform.parent = transform;
        wheat.transform.DOLocalMove(targetPosition, 1);
        wheat.transform.DOLocalRotate(Vector3.zero, 1);
        towerOfWeatBlocks.Push(wheat);
        uIController.CounterWheatBlocks(1);
    }

    public void SellBlocks(Transform barn)
    {
        if (_delaySellCoroutine == null)
            _delaySellCoroutine = StartCoroutine(DelaySell(barn));
    }

    private IEnumerator DelaySell(Transform barn)
    {
        var count = towerOfWeatBlocks.Count;
        for (int i = 0; i < count; i++)
        {
            var block = towerOfWeatBlocks.Pop();
            block.MoveToBarn(barn);
            uIController.CounterWheatBlocks(-1);

            yield return new WaitForSeconds(0.5f);
        }
        _delaySellCoroutine = null;
    }

    private void GetSaveBlocks(int count)
    {
        for (int i = 0; i < count; i++)
        {
          Instantiate(prefab, transform.position, Quaternion.identity).AddTouchBlockToBackpack(this);
        }
    }

    private void OnApplicationQuit()
    {
        SaveManager.Singleton.SetBlocks(towerOfWeatBlocks.Count);
    }
}
