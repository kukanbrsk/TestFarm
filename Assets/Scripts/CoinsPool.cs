using UnityEngine;

public class CoinsPool : MonoBehaviour
{
    public PoolMono<Coins> pool => _pool;
    [SerializeField] private int poolCount;
    [SerializeField] private bool autoExspand;
    [SerializeField] private Coins coinsPrefab;
    [SerializeField] private GameObject parentCoins;
    private Camera cam;
    private PoolMono<Coins> _pool;

    void Start()
    {
        cam = Camera.main;
        _pool = new PoolMono<Coins>(coinsPrefab, poolCount, parentCoins.transform);
        _pool.autoExpand = autoExspand;
    }

    public void CreateCoins(Transform barnPos)
    {
        var coins = pool.GetFreeElement();
        coins.transform.position = cam.WorldToScreenPoint(barnPos.position);
    }
}
