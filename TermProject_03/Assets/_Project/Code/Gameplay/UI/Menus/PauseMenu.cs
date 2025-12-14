using UnityEngine;
using UnityEngine.UI;

using _Project.Code.Core.ServiceLocator;
using _Project.Code.Core.Events;
using System.Collections;

public class PauseMenu : MenuPopUp
{
    [Header("Pause Settings")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image pauseBackground;

    private Color _blackTransparent = new Color(0.0f, 0.0f, 0.0f, 0.4f);
    private Color _fullTransparent = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    [Header("Buttons")]
    [SerializeField] private Button resumeGame;
    [SerializeField] private Button openSettingsMenu;
    [SerializeField] private Button returnToMainMenu;

    [Header("Tween Details")]
    [SerializeField] private RectTransform targetPosition;
    [SerializeField] private float moveDuration = 1.0f;

    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Coroutine currentMoveCoroutine;


    protected override void Awake()
    {
        base.Awake();

        rectTransform = pauseMenu.GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;

        resumeGame.onClick.AddListener(ResumeClicked);
        openSettingsMenu.onClick.AddListener(OpenSettingsMenu);
        returnToMainMenu.onClick.AddListener(ReturnToMainMenu);
    }

    private void Start()
    {
        ServiceLocator.Get<EventBus>().Subscribe<PauseInputEvent>(this, OnPauseInputEvent);
    }

    public void FireOnPauseInputEvent()
    {
        ServiceLocator.Get<EventBus>().Publish(new PauseInputEvent());
    }

    private void OnPauseInputEvent(PauseInputEvent evt)
    {
        if (Time.timeScale == 0.0f)
            UnPauseGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;

        OpenMenu();
    }

    private void UnPauseGame()
    {
        CloseMenu();

        Time.timeScale = 1.0f;
    }

    private void ResumeClicked()
    {
        ServiceLocator.Get<EventBus>().Publish(new PauseInputEvent());
    }

    public override void OpenMenu()
    {
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        pauseBackground.color = _blackTransparent;
        currentMoveCoroutine = StartCoroutine(MoveCoroutine(startPosition, targetPosition.anchoredPosition));
    }

    public override void CloseMenu()
    {
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveCoroutine(targetPosition.anchoredPosition, startPosition));
        pauseBackground.color = _fullTransparent;
    }

    IEnumerator MoveCoroutine(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
    }

    private void OpenSettingsMenu()
    {
        ServiceLocator.Get<SettingsMenu>().OpenMenu();
    }

    private void ReturnToMainMenu()
    {
        if (ServiceLocator.TryGet(out ConfirmationWindow window))
        {
            window.OnConfirmed += SendToMainMenu;
            window.OnDeclined += ClearConfirmationWindowSubscriptions;
            window.OpenMenu();
        }
    }

    private void ClearConfirmationWindowSubscriptions()
    {
        if (ServiceLocator.TryGet(out ConfirmationWindow window))
        {
            window.OnConfirmed -= SendToMainMenu;
            window.OnDeclined -= ClearConfirmationWindowSubscriptions;
        }
    }

    private void SendToMainMenu()
    {
        ClearConfirmationWindowSubscriptions();

        UnPauseGame();
        ServiceLocator.Get<SceneService>().LoadScene("MainMenu");
    }
}
