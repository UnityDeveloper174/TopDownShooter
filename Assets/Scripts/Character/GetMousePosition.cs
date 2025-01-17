using UnityEngine;

public class GetMousePosition : MonoBehaviour
{
    
    private Camera _cam;
    [SerializeField] private LayerMask layerMask = default;
    [SerializeField] public Transform target;
    [SerializeField] private float speed = 10f;
    [SerializeField] public float radius = 10f;
    [SerializeField] private AutoAim autoAim;

    [SerializeField] private LayerMask layerMaskEnemy;
    
    private CharacterMovement characterMovement;

    public bool targetFound = false;
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        _cam = Camera.main;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Update is called once per frame
    void Update()
    {
        Ray mouseWorldPosition = _cam.ScreenPointToRay(Input.mousePosition);
        
        if(autoAim.enable)
        {
            Collider[] enemys = Physics.OverlapSphere(transform.position, radius);

            if(enemys.Length > 0)
            {
                foreach(Collider enemy in enemys)
                {
                    if(enemy.gameObject.tag == "Enemy")
                    {
                        targetFound = true;
                        target.position = Vector3.MoveTowards(target.position, enemy.transform.position, 0.07f);
                        break;
                    }
                    else
                    {
                        targetFound = false;
                        target.position = Vector3.MoveTowards(target.position, transform.position + characterMovement.direction * 4f, 0.025f);
                        
                    }
                    

                }
            
            }
            
          
        }
        else
        {
            if(Physics.Raycast(mouseWorldPosition, out RaycastHit hitInfo, layerMask))
            {
                target.position = Vector3.Lerp(target.position, hitInfo.point, speed * Time.deltaTime);
            }
            else
            {
                target.position = Vector3.Lerp(target.position, mouseWorldPosition.GetPoint(100), speed * Time.deltaTime);
            }
        }
        
       
    }
}
