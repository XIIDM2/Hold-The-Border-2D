using Core.FSM;

namespace Gameplay.Units.FSM
{
    public class UnitState<T> : State<T> where T : BaseUnitController<T>
    {
    }
}