using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    private float _ySpeed;
    private bool _isJump;
    private bool _isShooting;
    private bool _isSwitchingGun;

    private CharacterController _characterController;
    private Animator _animator;
    
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private Transform transformCamera;
    [SerializeField] private Transform pivot;
    [SerializeField] private Gun gun;

    public Gun Gun => gun;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float rotation = Input.GetAxis("Mouse X");
        
        Vector3 inputDirection = new Vector3(horizontal, 0.0f, vertical);
        Vector3 movement = new Vector3(horizontal * speed, gravity, vertical * speed);
        
        var inputAngle = horizontal < 0.0f ? -Vector3.Angle(Vector3.forward, inputDirection) : Vector3.Angle(Vector3.forward, inputDirection);
        _animator.SetFloat("direction", inputAngle / 180.0f);
        _animator.SetFloat("idle", inputDirection.magnitude);
        movement = Quaternion.AngleAxis(transformCamera.rotation.eulerAngles.y, Vector3.up) * movement;

        _characterController.Move(movement * Time.deltaTime);
        _characterController.transform.Rotate(Vector3.up, rotation);
        
        if(Input.GetMouseButtonDown(0) && !_isShooting && !_isSwitchingGun)
        {
            StartCoroutine(StartShotAnimation(_animator));
        }
    }

    public void ChangeGun(Gun newGun)
    {
        if (gun == newGun) return;
        
        StartCoroutine(SwitchGun(_animator, newGun));
    }
    
    private IEnumerator StartShotAnimation(Animator animator)
    {
        _isShooting = true;
        animator.SetTrigger("shot");
        yield return new WaitForSeconds(0.1f);
        gun.Shoot(pivot);
        yield return new WaitForSeconds(0.75f);
        _isShooting = false;
    }

    private IEnumerator SwitchGun(Animator animator, Gun newGun)
    {
        _isSwitchingGun = true;
        animator.SetTrigger("SwitchGun");
        yield return new WaitForSeconds(0.9f);
        gun.gameObject.SetActive(false);
        gun = newGun;
        gun.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _isSwitchingGun = false;
    }
}
