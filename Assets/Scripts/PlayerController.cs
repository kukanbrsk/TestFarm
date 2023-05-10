using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject backpack;
    private readonly int _runBlend = Animator.StringToHash("RunBlend");
    private readonly int _harvesting = Animator.StringToHash("Harvesting");

    private Backpack _backpack;
    public Backpack BackpackPlayer => _backpack;

    private UIController _UIController;

    private Action _moveStarted;
    private Wheat _wheat;

    private int _direction = 1;
    [SerializeField] private int limit;
   [SerializeField] private float angularVelocity;

    private int _currentAccount;

    void Start()
    {
        _UIController = FindObjectOfType<UIController>();
        _backpack = GetComponentInChildren<Backpack>();
        transform.position = SaveManager.Singleton.GetPosition();

        _currentAccount = SaveManager.Singleton.GetCoins();
        UIController.singletone.DemonstationMoney(_currentAccount);

        // counterCoins.text = _currentAccount.ToString();

    }

    private void FixedUpdate()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
            backpack.transform.Rotate(angularVelocity * _direction* Time.deltaTime, 0, 0);
            var angle = backpack.transform.localEulerAngles.x;
            if (angle>180)
                angle = 360 - angle;

            if (angle >= limit)
                _direction *= -1;

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

    private void OnApplicationQuit()
    {
        SaveManager.Singleton.SetPosition(transform.position);
        SaveManager.Singleton.SetCoins(_currentAccount);

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

    public void CutWheat()=> _wheat.CutWheat();

    public void CounterCoins(int counter)
    {
        StartCoroutine(DelayCountMoney(counter));
    }

    private IEnumerator DelayCountMoney(int counter)
    {
        for (int i = 0; i < counter; i++)
        {
            //ShakeCoinsBar();
            _currentAccount += 1;
            UIController.singletone.DemonstationMoney(_currentAccount);
          //  counterCoins.text = _currentAccount.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
