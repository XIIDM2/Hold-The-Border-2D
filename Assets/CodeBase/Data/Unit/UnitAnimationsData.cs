using UnityEngine;

[CreateAssetMenu(fileName = "Unit Animations Data", menuName = "Scriptable Objects/Units/Unit Animations Data")]
public class UnitAnimationsData : ScriptableObject
{
    [Header("Down Direction")]
    [SerializeField] private AnimationClip d_Attack;
    [SerializeField] private AnimationClip d_Death;
    [SerializeField] private AnimationClip d_Disolve;
    [SerializeField] private AnimationClip d_Walk;

    [Header("Side Direction")]
    [SerializeField] private AnimationClip s_Attack;
    [SerializeField] private AnimationClip s_Death;
    [SerializeField] private AnimationClip s_Disolve;
    [SerializeField] private AnimationClip s_Walk;

    [Header("Up Direction")]
    [SerializeField] private AnimationClip u_Attack;
    [SerializeField] private AnimationClip u_Death;
    [SerializeField] private AnimationClip u_Disolve;
    [SerializeField] private AnimationClip u_Walk;

    [Header("Down Direction")]
    public AnimationClip D_Attack => d_Attack;
    public AnimationClip D_Death => d_Death;
    public AnimationClip D_Disolve => d_Disolve;
    public AnimationClip D_Walk => d_Walk;

    [Header("Side Direction")]
    public AnimationClip S_Attack => s_Attack;
    public AnimationClip S_Death => s_Death;
    public AnimationClip S_Disolve => s_Disolve;
    public AnimationClip S_Walk => s_Walk;

    [Header("Up Direction")]
    public AnimationClip U_Attack => u_Attack;
    public AnimationClip U_Death => u_Death;
    public AnimationClip U_Disolve => u_Disolve;
    public AnimationClip U_Walk => u_Walk;
}
