using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BlockWheat : MonoBehaviour
{
    [SerializeField] private WheatSetting wheatSetting;

    private bool _isFree = true;
    private Rigidbody _rigidbodyBlock;
    private UIController _UIController;


    private void Awake()
    {
        //StartCoroutine(DelayPick());
        _rigidbodyBlock = GetComponent<Rigidbody>();
        _rigidbodyBlock.AddForce(Vector3.up * wheatSetting.ForceBlock, ForceMode.Impulse);
        _UIController = FindObjectOfType<UIController>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out PlayerController player) && _isFree && !_UIController.limitBlocks)
        {
            AddTouchBlockToBackpack(player.BackpackPlayer);
        }
    }

    public void MoveToBarn(Transform barn)
    {
        transform.parent = null;
        transform.DOMove(barn.position, 1).OnComplete(DestroyBlock);
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

   //private IEnumerator DelayPick()
   // {
   //     yield return new WaitForSeconds(2);
   //     _isFree = true;
   // }

    public void AddTouchBlockToBackpack(Backpack backpack)
    {
        gameObject.layer = LayerMask.NameToLayer("Not interactable");
        _rigidbodyBlock.isKinematic = true;
        backpack.AddBlockToBackpack(this);
        _isFree = false;
    }
}
