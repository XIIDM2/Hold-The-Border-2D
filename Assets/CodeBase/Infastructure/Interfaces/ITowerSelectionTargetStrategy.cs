using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface ITowerSelectionTargetStrategy
    {
        ITargetable SelectTarget(IReadOnlyCollection<ITargetable> targetables);
    }
}