using System.Collections;
using UnityEngine;

public class CollectableGenerator : MonoBehaviour {

    // TODO. This is for gold only. Conver it to GENERIC.

    public bool IsRunning { get { return _isRunning; } }
    [SerializeField]
    private float _goldSpawnRate = 3f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isRunning = false;
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkGoldCoroutine = null;

    private IEnumerator ICheckGolds() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_goldSpawnRate);

        while (true) {
            yield return waitForSeconds;

            SpawnGold();
        }
    }

    private void SpawnGold() {
        Vector3 randomPosition = new Vector3(
            0f,
            1f,
            Random.Range(GameManager.instance.LeftMapPivotTransform.position.z, GameManager.instance.RightMapPivotTransform.position.z));
        
        ObjectPooler.instance.SpawnFromPool("Gold", randomPosition, Quaternion.identity);
    }

    public void StartGoldSpawns() {
        if (_checkGoldCoroutine == null) {
            _checkGoldCoroutine = StartCoroutine(ICheckGolds());
            _isRunning = true;
        }
    }

    public void StopGoldSpawns() {
        StopCoroutine(_checkGoldCoroutine);
        _isRunning = false;
    }


}
