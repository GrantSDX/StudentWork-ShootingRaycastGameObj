using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform _spawnBulet;
    [SerializeField] private GameObject _buletPrefab;
    [SerializeField] private float force;
    private PlayerGame _playerGame;

    private void Start()
    {
        _playerGame = FindObjectOfType<PlayerGame>();
    }

    // Стрельба по курсору
    public void ShotGun(RaycastHit hit)
    {
        
        var bulet = Instantiate(_buletPrefab, _spawnBulet.position, _spawnBulet.rotation);
        var rb = bulet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce((hit.point - _spawnBulet.position).normalized * force, ForceMode.Impulse);
     
        bulet.GetComponent<Bullet>().SeporationCylinder();

        
    }
    
    
    public void ShotGun()
    {
        
        var bulet = Instantiate(_buletPrefab, _spawnBulet.position, _spawnBulet.rotation);

        var rb = bulet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        
        rb.AddForce((_spawnBulet.position - transform.position).normalized * force, ForceMode.Impulse);

        bulet.GetComponent<Bullet>().SeporationCylinder();

    }

    public IEnumerator ShootCoroutine()
    {
        while (true)
        {           
            if (Input.GetMouseButton(0))
            {
                
              RaycastHit hit;
              Debug.Log(Physics.Raycast(_playerGame.RayCam));

              if (Physics.Raycast(_playerGame.RayCam, out hit))
              { 
                    
                    ShotGun(hit);
                
              }
               
                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
    }



}
