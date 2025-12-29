using UnityEngine;

public class TowerUnitAnimationData : BaseUnitAnimationData
{
    [Header("Down Direction")]
    [SerializeField] private AnimationClip d_Attack;
    [SerializeField] private AnimationClip d_PreAttack;
    [SerializeField] private AnimationClip d_Idle;

    [Header("Side Direction")]
    [SerializeField] private AnimationClip s_Attack;
    [SerializeField] private AnimationClip s_PreAttack;
    [SerializeField] private AnimationClip s_Idle;


    [Header("Up Direction")]
    [SerializeField] private AnimationClip u_Attack;
    [SerializeField] private AnimationClip u_PreAttack;
    [SerializeField] private AnimationClip u_Idle;


    [Header("Down Direction")]
    public AnimationClip D_Attack => d_Attack;
    public AnimationClip D_PreAttack => d_PreAttack;
    public AnimationClip D_Idle => d_Idle;

    [Header("Side Direction")]
    public AnimationClip S_Attack => s_Attack;
    public AnimationClip S_PreAttack => s_PreAttack;
    public AnimationClip S_Idle => s_Idle;


    [Header("Up Direction")]
    public AnimationClip U_Attack => u_Attack;
    public AnimationClip U_PreAttack => u_PreAttack;
    public AnimationClip U_Idle => u_Idle;
}
