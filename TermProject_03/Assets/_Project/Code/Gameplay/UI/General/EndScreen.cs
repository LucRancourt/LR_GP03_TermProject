using TMPro;
using UnityEngine;
using UnityEngine.UI;

using _Project.Code.Core.MVC;
using _Project.Code.Core.ServiceLocator;


public class EndScreen : BaseView<bool>
{
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;


    private void Start()
    {
        replayButton.onClick.AddListener(() => ServiceLocator.Get<SceneService>().ReloadCurrentScene());
        mainMenuButton.onClick.AddListener(() => ServiceLocator.Get<SceneService>().LoadScene("MainMenu"));
    }

    public override void UpdateDisplay(bool isWin)
    {
        if (isWin)
            endText.text = "You Win!";
        else
            endText.text = "You Lose!";
    }

    private void OnDestroy()
    {
        replayButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }
}
