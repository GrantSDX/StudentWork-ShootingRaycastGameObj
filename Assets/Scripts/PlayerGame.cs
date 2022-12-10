using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGame : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private PlayerGun _playerGun;
    
    public float SpeedRotCamera;
    public float SpeedRotGun;

    private Ray _rayCam;
    public Ray RayCam => _rayCam;

    private float xRot;
    private float yRot;
    private float xPos;
    private float zPos;
    private Vector3 distans;

    public float fireRate;
    private float _nextTimeToFire = 0f;

    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(_playerGun.ShootCoroutine());
    }

    void Update()
    {
        Cursor.visible = true;

        xRot += Input.GetAxis("Mouse X");
        yRot += Input.GetAxis("Mouse Y");
        xPos = Input.GetAxis("Horizontal");
        zPos = Input.GetAxis("Vertical");    
        Movement(xPos,zPos,xRot,yRot);

        var mousePosition = Input.mousePosition;
        _rayCam = _playerCamera.ScreenPointToRay(mousePosition);
  
        if (Input.GetMouseButton(1) && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / fireRate;
            _playerGun.ShotGun();
        }
    }
   
    public void Movement(float xPos, float zPos, float xRot, float yRot)
    {
        // поворот игрока с помощью камеры и мыши
        _playerCamera.transform.rotation = Quaternion.Euler(-yRot , xRot * SpeedRotCamera, 0f);    
        transform.rotation = Quaternion.Euler(0f, xRot * SpeedRotCamera, 0f);
  
        _playerGun.transform.rotation = Quaternion.LookRotation(_playerCamera.transform.forward);

        // переводит локальные координаты в мировые и позволяет двигаться в направлении обзора
        distans = new Vector3(xPos, 0f, zPos);
        distans = transform.TransformDirection(distans); 
        transform.position += distans * 10f * Time.deltaTime;
    }
}
