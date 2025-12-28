using UnityEngine;

public enum EnemyUnitAnimationParameter
{
    Horizontal,
    Vertical,
    IsMoving,
    IsAttacking,    
    IsDead,
    IsDisolving,

}

[RequireComponent(typeof(Animator))]
public class EnemyUnitAnimation : BaseUnitAnimation<EnemyUnitAnimationsData>
{
    [Header("Down Direction")]
    private const string D_ATTACK = "D_Attack";
    private const string D_DEATH = "D_Death";
    private const string D_DISOLVE = "D_Disolve";
    private const string D_WALK = "D_Walk";

    [Header("Side Direction")]
    private const string S_ATTACK = "S_Attack";
    private const string S_DEATH = "S_Death";
    private const string S_DISOLVE = "S_Disolve";
    private const string S_WALK = "S_Walk";

    [Header("Up Direction")]
    private const string U_ATTACK = "U_Attack";
    private const string U_DEATH = "U_Death";
    private const string U_DISOLVE = "U_Disolve";
    private const string U_WALK = "U_Walk";


    public override void Init(EnemyUnitAnimationsData data)
    {

        _clipOverrides[D_ATTACK] = data.D_Attack;
        _clipOverrides[D_DEATH] = data.D_Death;
        _clipOverrides[D_DISOLVE] = data.D_Disolve;
        _clipOverrides[D_WALK] = data.D_Walk;

        _clipOverrides[S_ATTACK] = data.S_Attack;
        _clipOverrides[S_DEATH] = data.S_Death;
        _clipOverrides[S_DISOLVE] = data.S_Disolve;
        _clipOverrides[S_WALK] = data.S_Walk;

        _clipOverrides[U_ATTACK] = data.U_Attack;
        _clipOverrides[U_DEATH] = data.U_Death;
        _clipOverrides[U_DISOLVE] = data.U_Disolve;
        _clipOverrides[U_WALK] = data.U_Walk;

        ApplyClipsOverrides();

    }

}
