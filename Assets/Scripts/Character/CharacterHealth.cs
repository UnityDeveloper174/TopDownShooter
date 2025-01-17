using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{

    [SerializeField] private Slider healthBar;
    [SerializeField] private float smoothTime = 10f;
    public GameObject gameOverUI;

    
    [SerializeField]
    [Range(0f, 100f)]
    private float health = 100f;

    private CharacterMovement characterMovement;
    private float currentVelocity;

    

   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverUI.SetActive(false);
        characterMovement = GetComponent<CharacterMovement>();
    }
    
    void LateUpdate()
    {
        HealthStatus();
    }

    private void HealthStatus()
    {
        health = Mathf.Clamp(health, 0f, 100f);
        healthBar.value = Mathf.Lerp(healthBar.value, health, smoothTime * Time.unscaledDeltaTime);
        if(health <= 0f)
        {
            characterMovement.enabled = false;
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
