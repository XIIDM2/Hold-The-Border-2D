using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData _data;
    [SerializeField] private PathService _pathService;

    private void Start()
    {
        StartCoroutine(WavesLogicRoutine());
    }

    private IEnumerator WavesLogicRoutine()
    {
        yield return new WaitForSeconds(_data.WavesStartTimer);

        foreach (WaveData.Wave wave in _data.Waves)
        {
            foreach (WaveData.Wave.WaveUnits units in wave.Units)
            {
                for (int i = 0; i < units.Amount; i++)
                {
                    Messenger<UnitType, Waypoint>.Broadcast(Events.UnitSpawn, units.Type, _pathService.GetWaypoint(units.Path));
                    yield return new WaitForSeconds(units.IntervalCurrent);
                }
                yield return new WaitForSeconds(units.IntervalNext);
            }
            yield return new WaitForSeconds(wave.WaveInterval);
        }

        Debug.Log("All Waves Finished");
    }
}
