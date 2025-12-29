using System.Collections;
using UnityEngine;

public class WaveService : IWaveService
{
    public IEnumerator WavesLogicRoutine(WaveData data, IPathProvider pathProvider)
    {
        yield return new WaitForSeconds(data.WavesStartTimer);

        foreach (WaveData.WaveConfig wave in data.Waves)
        {
            foreach (WaveData.WaveConfig.WaveUnitsConfig units in wave.Units)
            {
                for (int i = 0; i < units.Amount; i++)
                {
                    Messenger<EnemyUnitType, Waypoint>.Broadcast(Events.UnitSpawned, units.Type, pathProvider.GetWaypoint(units.Path));
                    yield return new WaitForSeconds(units.IntervalCurrent);
                }
                yield return new WaitForSeconds(units.IntervalNext);
            }
            yield return new WaitForSeconds(wave.WaveInterval);
        }

        Debug.Log("All Waves Finished");
    }
}
