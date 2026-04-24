using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 30f;
    public Camera Maincamera;
    public EnemyAI[] EnemyAi;
    EnemyAI target;

    // public ParticleSystem Muzzelflash;
    // public GameObject Flareeffect;
    public float Muzzelforce = 30f;
    // private Enemy target;
    //public AudioSource shootAudioSource;
    //public soundClips SoundClips;
    
     private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }     
        
         void shoot()
        {
        
           if (Physics.Raycast(Maincamera.transform.position, Maincamera.transform.forward, out RaycastHit hit, range))
            {
                Debug.Log(hit.transform.name);
                target = hit.transform.GetComponent<EnemyAI>();
                if(hit.transform.GetComponent<EnemyAI>())
                
                if(target != null){
                    
                }
            }
        }
    }
}
