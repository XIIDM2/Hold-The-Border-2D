using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public IReadOnlyList<WaveUnits> Units => _units;
        public float WaveInterval => _waveInterval;

        [SerializeField] private WaveUnits[] _units;

        [Header("Interval Between Next Wave")]
        [SerializeField] private float _waveInterval;

        [System.Serializable]
        public struct WaveUnits
        {
            public UnitType Type => _unitType;
            public int Amount => _amount;
            public float Interval => _interval;

            public Waypoint Start => _start;

            [SerializeField]  private UnitType _unitType;
            [SerializeField]  private int _amount;

            [Header("Start Waypoint")]
            [SerializeField] private Waypoint _start;

            [Header("Interval Between Next Units")]
            [SerializeField]  private float _interval;
        }


    }

    [SerializeField] private float _wavesStartInterval;
    [SerializeField] private Wave[] _waves;

    private void Start()
    {
        StartCoroutine(WavesLogicRoutine());
    }

    private IEnumerator WavesLogicRoutine()
    {
        yield return new WaitForSeconds(_wavesStartInterval);

        foreach (Wave wave in _waves)
        {
            foreach (Wave.WaveUnits units in wave.Units)
            {
                for (int i = 0; i < units.Amount; i++)
                {
                    Messenger<UnitType, Waypoint>.Broadcast(Events.UnitSpawn, units.Type, units.Start);
                }
                yield return new WaitForSeconds(units.Interval);
            }
            yield return new WaitForSeconds(wave.WaveInterval);
        }

        Debug.Log("All Waves Finished");
    }
}
