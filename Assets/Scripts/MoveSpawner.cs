using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveSpawner : MonoBehaviour
{
    public GameObject Mover;

    private SystemController _systemController;
    private GameObject[] _rotators;

    // Use this for initialization
    void Start()
    {
        _systemController = GameObject.Find("System").GetComponent<SystemController>();
        _rotators = _systemController.Rotators;
        Spawn();
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(4 / _systemController.Speed);
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject start = (GameObject)Instantiate(Mover, new Vector3(16, 0), Quaternion.identity);
        MoveController controller = (MoveController)start.GetComponent("MoveController");
        controller.System = _systemController;

        System.Random rng = new System.Random();
        var r = Enumerable.Range(0, _rotators.Length - 1).OrderBy(x => rng.Next()).ToArray();
        var ra = new[] { _rotators[r[0]], _rotators[r[1]], _rotators[r[2]] };

        foreach (GameObject x in ra)
        {
            x.transform.eulerAngles = new Vector3(0, 0, 90 * rng.Next(4));
        }

        controller.Rotators = ra;
    }
}
