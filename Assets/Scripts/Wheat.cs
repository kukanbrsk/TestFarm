using System.Collections;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    public bool IsReady { get; private set; }

    [SerializeField] private float startHight;
    [SerializeField] private float targetGrowHight;
    [SerializeField] private WheatSetting wheatSetting;

    [SerializeField] private BlockWheat blockWheat;

    private Vector3 _startPosition;
    private Vector3 _targetGrow;

    void Start()
    {
        _startPosition = new Vector3(transform.position.x, startHight, transform.position.z);
        _targetGrow = new Vector3(transform.position.x, targetGrowHight, transform.position.z);
        StartCoroutine(GrowWheat());
    }

    private IEnumerator GrowWheat()
    {
        var distance = targetGrowHight - startHight;
        while (transform.position.y < targetGrowHight)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetGrow, distance * Time.deltaTime / wheatSetting.GrowTime);
            yield return null;
        }
        IsReady = true;
    }

    public void CutWheat()
    {
        transform.position = _startPosition;
        StartCoroutine(GrowWheat());
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        Instantiate(blockWheat).transform.position = _targetGrow;
    }
}
