using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface ITowerSelectionTargetStrategy
    {
        ITargetable SelectTarget(List<ITargetable> targetables);
    }
}