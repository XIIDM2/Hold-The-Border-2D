using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Objects/Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _maxHeath;

    public int MaxHeath => _maxHeath;
}
