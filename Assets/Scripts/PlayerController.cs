using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Animator animator;

    private readonly int _runBlend = Animator.StringToHash("RunBlend");
    private readonly int _harvesting = Animator.StringToHash("Harvesting");

    private Backpack _backpack;
    public Backpack BackpackPlayer => _backpack;

    private UIController _UIController;

    private Action _moveStarted;

    private Wheat _wheat;

    void Start()
    {
        _UIController = FindObjectOfType<UIController>();
        _backpack = GetComponentInChildren<Backpack>();
    }

    private void FixedUpdate()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
            _moveStarted?.Invoke();
        }
        var blendValue = Mathf.Max(Mathf.Abs(joystick.Horizontal), Mathf.Abs(joystick.Vertical));
        animator.SetFloat(_runBlend, blendValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wheat wheat) && wheat.IsReady)
        {
            _UIController.ShowSickle(wheat.transform.position, Harvest);
            _wheat = wheat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Wheat wheat))
            _UIController.HideSickle();
    }

    private void Harvest()
    {
        _UIController.HideSickle();
        animator.SetBool(_harvesting, true);
        _moveStarted = EndHarvest;
    }

    public void EndHarvest()
    {
        animator.SetBool(_harvesting, false);
        _moveStarted = null;
    }

    public void CutWheat()
    {
        _wheat.CutWheat();
    }
}
