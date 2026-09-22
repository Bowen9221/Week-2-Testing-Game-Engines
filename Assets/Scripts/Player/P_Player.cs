using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class P_Player : MonoBehaviour
{

    [Header("References")]

    [Header("Private Variables")]

    private int _currentHealth;
    private int _maxHealth = 6; //Each heart has 2HP
    private int _healAmount = 2; //Heal 1 heart per pickup
    [SerializeField] private int _damage = 2;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Encapsulation: Getters and Setters ---------------------------------------
    // Getters: ------------------------------
    public int GetPlayerDamage()
    {
        return _damage;
    }
    // Getters: ------------------------------


    // Setters: ------------------------------

    //Idea for later sword / weapon upgrades. Keeping things available

    //public void SetPlayerDamage(int damage)
    //{
    //    _damage = damage;
    //}

    //Idea for different healing items like a mini heart vs a Big Health Potion. Likely could be moved into the actual items' class not here.
    public void SetHealAmount(int amount)
    {
        _healAmount = amount;
    }
    // Setters: ------------------------------

    // Behavioral: --------------------------
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
    public void Heal(int amount)
    {
        _currentHealth += amount;
    }

    private void CheckIfDead()
    {
        if (_currentHealth <= 0)
        {
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        Debug.Log("You Lose");

        yield return new WaitForEndOfFrame();
        Debug.Log("Loading Scene");
        SceneManager.LoadScene(0);
    }
    // Behavioral: --------------------------

    // Encapsulation: Getters and Setters ---------------------------------------


    private void Update()
    {
        CheckIfDead();
    }

}
