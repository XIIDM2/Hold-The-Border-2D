using UnityEngine;

public class TowerRangedUnitAnimation : UnitAnimationsOverrides<TowerUnitAnimationData>
{
    [Header("Down Direction")]
    private const string D_ATTACK = "D_Attack";
    private const string D_PREATTACK = "D_PreAttack";
    private const string D_IDLE = "D_Idle";

    [Header("Side Direction")]
    private const string S_ATTACK = "S_Attack";
    private const string S_PREATTACK = "S_PreAttack";
    private const string S_IDLE = "S_Idle";

    [Header("Up Direction")]
    private const string U_ATTACK = "U_Attack";
    private const string U_PREATTACK = "U_PreAttack";
    private const string U_IDLE = "U_Idle";

    protected override void InitData(TowerUnitAnimationData data)
    {

        _clipOverrides[D_ATTACK] = data.D_Attack;
        _clipOverrides[D_PREATTACK] = data.D_PreAttack;
        _clipOverrides[D_IDLE] = data.D_Idle;

        _clipOverrides[S_ATTACK] = data.S_Attack;
        _clipOverrides[S_PREATTACK] = data.S_PreAttack;
        _clipOverrides[S_IDLE] = data.S_Idle;

        _clipOverrides[U_ATTACK] = data.U_Attack;
        _clipOverrides[U_PREATTACK] = data.U_PreAttack;
        _clipOverrides[U_IDLE] = data.U_Idle;

        ApplyClipsOverrides();

    }
}
