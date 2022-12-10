using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void SeporationCylinder()
    {   
        StartCoroutine(BulletCoroutine(transform));      
    }  
    private IEnumerator BulletCoroutine(Transform stair)
    {
        yield return new WaitForSeconds(3f + 0.5f);
        var stairOne = stair.GetChild(0);
       
        stairOne.SetParent(null);
        stairOne.GetComponent<Rigidbody>().isKinematic = false;
        stairOne.GetComponent<Rigidbody>().useGravity = true;

        yield return new WaitForSeconds(0.01f);
        var stairTwo = stair.GetChild(0);
        
        stairTwo.SetParent(null);
        stairTwo.GetComponent<Rigidbody>().isKinematic = false;
        stairTwo.GetComponent<Rigidbody>().useGravity = true;
        var rbStairTwo = stairTwo.GetComponent<Rigidbody>();
        var direction = stairTwo.transform.position - transform.position;
        rbStairTwo.AddForce(direction * 50f, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);
        Destroy(stairOne.gameObject);
        Destroy(stairTwo.gameObject);
        Destroy(gameObject);
        
    }
}
