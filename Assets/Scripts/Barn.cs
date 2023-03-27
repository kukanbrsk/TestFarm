using UnityEngine;

public class Barn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
            player.BackpackPlayer.SellBlocks(transform);
    }
}
