using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [SerializeField] WavesData _data;

    private void Start()
    {
        StartCoroutine(WavesLogicRoutine());
    }

    private IEnumerator WavesLogicRoutine()
    {
        yield return new WaitForSeconds(_data.WavesStartInterval);

        foreach (WavesData.Wave wave in _data.Waves)
        {
            foreach (WavesData.Wave.WaveUnits units in wave.Units)
            {
                for (int i = 0; i < units.Amount; i++)
                {
                    //Messenger<UnitType, Waypoint>.Broadcast(Events.UnitSpawn, units.Type, units.Start);
                    yield return new WaitForSeconds(units.Interval);
                }
            }
            yield return new WaitForSeconds(wave.WaveInterval);
        }

        Debug.Log("All Waves Finished");
    }
}
