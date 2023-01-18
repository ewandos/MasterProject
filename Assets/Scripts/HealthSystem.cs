using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * THIS WILL BE REPLACED BY AN IMPLEMENTATION OF IHEALTH
 */

public class HealthSystem : IHealth
{
    [SerializeField] public bool bossDead = false;

    [SerializeField] private TextMeshProUGUI text;
    
    public void Start()
    {
        _health = _maxHealth;
    }

    [Button]
    public void TakeDamage(int amount)
    {
		_health -= amount;
        
        if (_health <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
		bossDead = true;

		Destroy(gameObject);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void AddHealth(int amount)
    {
		_health += amount;
        if (_health > _maxHealth)
			_health = _maxHealth;
    }
}