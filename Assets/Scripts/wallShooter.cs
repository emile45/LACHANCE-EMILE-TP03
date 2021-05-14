using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallShooter : MonoBehaviour
{
    public Transform barrel;
    public Transform playerRb;
    private Animation animationUpDown;
    public GameObject bullet;
    private float timeSince=0f;



    // Start is called before the first frame update
    void Start()
    {
        animationUpDown = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        TryDetectLayer();

    }
    void TryDetectLayer()
    {
        if ((Time.time - timeSince) > 8f || timeSince == 0f)
        {
            animationUpDown.enabled = true;
            // Créer un rayon 
            Ray ray = new Ray(barrel.position, barrel.up * 50f);
            RaycastHit hit;

            // S'il est obstrué par un collider autre que le personnage
            if (Physics.Raycast(ray, out hit, 50f))
            {
                // Verification pour savoir s'il s'agit du joueur.
                if (hit.collider.CompareTag("Player"))
                {
                    animationUpDown.enabled = false;
                    Shoot();
                    timeSince = Time.time;
                }
            }
        }

    }

    private void Shoot()
    {
        GameObject bullet_ = Instantiate(bullet, new Vector3(barrel.position.x, barrel.position.y, barrel.position.z+2f), Quaternion.identity);
        Rigidbody rbBullet = bullet_.GetComponent<Rigidbody>();
        rbBullet.isKinematic = false;
        rbBullet.AddForce(new Vector3(0f, 0f, 5f), ForceMode.Impulse);
        Destroy(bullet_, 8f);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(barrel.position, barrel.up * 50f);
    }

    

}
