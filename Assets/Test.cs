using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject towerprefab;

    private void Create()
    {
        Instantiate(towerprefab);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Upgrade Tower"))
        {
            Create();
        }
    }
}
