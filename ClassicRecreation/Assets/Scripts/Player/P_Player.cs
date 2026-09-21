using UnityEngine;
using UnityEngine.InputSystem;

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
    public int GetPlayerDamage(int damage)
    {
        damage = _damage;
        return damage;
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
    // Behavioral: --------------------------

    // Encapsulation: Getters and Setters ---------------------------------------


   

}
