using UnityEngine;

public class AmmoShooter : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private uint cooldown;
    [SerializeField] private Transform ammoFolder;

    private float time = 0;
    protected AudioManager audioManager;

    void Start()
    {
        
        audioManager = AudioManager.Instance;
    }
    void FixedUpdate()
    {
        Attack();
    }
    private void Attack()
    {

        if (Time.timeSinceLevelLoad > time + cooldown)
        {
            Debug.Log("heck");
            InitAmmo();
            if (audioManager != null)
            {
                audioManager.PlaySound(5);

            }
            time = Time.timeSinceLevelLoad;

        }
    }
    private void InitAmmo()
    {
        GameObject newAmmo = Instantiate(ammoPrefab, transform.position, Quaternion.identity, ammoFolder);
    }
}