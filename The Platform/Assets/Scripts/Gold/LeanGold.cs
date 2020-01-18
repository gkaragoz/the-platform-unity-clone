using UnityEngine;

public class LeanGold : MonoBehaviour, IPooledObject {

    [Header("Initializations")]
    [SerializeField]
    private float _scaleTime = 0.5f;

    private void Awake() {
        LeanTween.scale(this.gameObject, this.transform.localScale * 1.2f, _scaleTime).setEaseInOutQuint().setLoopPingPong();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.instance.AddGoldToPlayer();
            PlayGetGoldSoundFX();
            this.gameObject.SetActive(false);
        }
    }

    // TODO
    private void PlayGetGoldSoundFX() {

    }

    public void OnObjectReused() {
        this.gameObject.SetActive(true);
    }

}
