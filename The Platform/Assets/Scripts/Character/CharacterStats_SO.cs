using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Scriptable Objects/Character Stats")]
public class CharacterStats_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Character";

    [SerializeField]
    private GameObject _prefab;

    // Health
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maxHealth;

    // Movement
    [SerializeField]
    private float _movementSpeed = 5f;

    // Levelling
    [SerializeField]
    private int _level = 0;
    [SerializeField]
    private int _maxExperience = 0;
    [SerializeField]
    private int _currentExperience = 0;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public float CurrentHealth {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public float MaxHealth {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float MovementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public int Level {
        get { return _level; }
        set { _level = value; }
    }

    public int MaxExperience {
        get { return _maxExperience; }
        set { _maxExperience = value; }
    }

    public int CurrentExperience {
        get { return _currentExperience; }
        set { _currentExperience = value; }
    }

    #endregion

}