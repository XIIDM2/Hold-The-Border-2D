using System.Collections;
using UnityEngine;

public class WaveService : IWaveService
{
    public IEnumerator WavesLogicRoutine(WaveData data, IPathProvider pathProvider)
    {
        yield return new WaitForSeconds(data.WavesStartTimer);

        foreach (WaveData.Wave wave in data.Waves)
        {
            foreach (WaveData.Wave.WaveUnits units in wave.Units)
            {
                for (int i = 0; i < units.Amount; i++)
                {
                    Messenger<UnitType, Waypoint>.Broadcast(Events.UnitSpawn, units.Type, pathProvider.GetWaypoint(units.Path));
                    yield return new WaitForSeconds(units.IntervalCurrent);
                }
                yield return new WaitForSeconds(units.IntervalNext);
            }
            yield return new WaitForSeconds(wave.WaveInterval);
        }

        Debug.Log("All Waves Finished");
    }
}
