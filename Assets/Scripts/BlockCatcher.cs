using UnityEngine;

public class BlockCatcher : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CoinsPool coinsPool;
    //[SerializeField] private UIController uIController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BlockWheat blockWheat))
        {
            coinsPool.CreateCoins(transform);
            // uIController.CounterCoins(15);
            playerController.CounterCoins(15);
        }
    }
}
