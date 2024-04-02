using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_functions : MonoBehaviour
{
// Serializable
    [Header("Managers")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private AudioManager audioManager;
    
    [Space(5)]
    [Header("Objects")]
    [SerializeField] private GameObject damageVignette;
    [SerializeField] private Transform cameraTransform;     // Reference to the camera's transform
    [SerializeField] private ParticleSystem[] fireEffect;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private AudioClip fireAudio;

    [Space(5)]
    [Header("Settings")]
    [SerializeField] private float raycastDistance = 10000f; // Distance for the raycast

//Private:
    private Animator animator;

    void Start()
    {
        animator = GameManager.instance.player.GetComponent<Animator>();
        // Link input to function
        inputManager.player_Mappings.PlayerInteract.Shoot.started += _ => Shoot();
        inputManager.player_Mappings.UI.Pause.started += _ => menuManager.ShowPauseScreen();
    }

    void Update()
    {
        RectTransform rectTransform = damageVignette.gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
    private void Shoot()
    {
        if(!GameManager.instance.isPaused)
        {
            // Play partcle effect
            int randomFireEffect = UnityEngine.Random.Range(0, fireEffect.Length);
            //Debug.Log(randomFireEffect);
            Instantiate(fireEffect[randomFireEffect], barrelTransform.position, barrelTransform.rotation);
            fireEffect[randomFireEffect].Play();

            // Play animation
            animator.SetTrigger("Shoot");

            //Play audio
            audioManager.PlayAudio(fireAudio, barrelTransform.position);

            // Get the position and direction of the raycast
            Vector3 raycastOrigin = cameraTransform.position; 
            Vector3 raycastDirection = cameraTransform.forward; // Get the forward direction from the camera

            if (Physics.Raycast(raycastOrigin, raycastDirection, out RaycastHit hit, raycastDistance))
            {
                IEnemy enemyInterface = hit.collider.gameObject.GetComponent<IEnemy>();

                // Check if interface is NULL
                enemyInterface?.Hit();

                // If the ray hits something, show the ray and the hit point
                Debug.DrawLine(raycastOrigin, hit.point, Color.red, 0.1f);
            }
            else
            {
                // If the ray doesn't hit anything, show the ray
                Vector3 endPosition = raycastOrigin + raycastDirection * raycastDistance;
                Debug.DrawLine(raycastOrigin, endPosition, Color.green, 0.1f);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            TakeDamage(5f);
        }
    }

    private void TakeDamage(float damageAmount)
    {
        GameManager.instance.playerHealth -= damageAmount; 
        GameManager.instance.UpdateHealthUI();
        if(GameManager.instance.playerHealth <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }
        Debug.Log(GameManager.instance.playerHealth);
    }
}
