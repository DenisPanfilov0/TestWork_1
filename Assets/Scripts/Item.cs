using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class Item : ScriptableObject
{
    public Sprite Icon => _icon;
    public string Name => _name;
    public int Count => _count;
    public string Description => _description;
    public int HealthIncrease => _healthIncrease;
    public int DamageIncrease => _damageIncrease;
    
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _count;
    [SerializeField] private string _description;
    [SerializeField] private int _healthIncrease;
    [SerializeField] private int _damageIncrease;
}
