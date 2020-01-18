using System.Collections;
using TMPro;
using UnityEngine;

public class PanelCountdownMenu : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private TextMeshProUGUI _txtCountdown = null;
    [SerializeField]
    private GameObject _gameplayMenu = null;

    private int _countdown = 3;

    private IEnumerator ICountdown() {
        while (true) {
            _txtCountdown.text = _countdown.ToString();
            _countdown--;
         
            yield return new WaitForSeconds(1f);

            if (_countdown <= 0) {
                break;
            }
        }

        OpenGamePlayMenu();
        HideThisMenu();
    }

    private void OpenGamePlayMenu() {
        _gameplayMenu.SetActive(true);
    }

    private void HideThisMenu() {
        this.gameObject.SetActive(false);
    }

    public void StartCountdown() {
        StartCoroutine(ICountdown());
    }

}
