using _Project.Code.Core.MVC;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelection : BaseView
{
    [Tooltip("In order of: Easy - Medium - Hard")]
    [SerializeField] private Button[] difficultySelectButtons = new Button[3];


    protected override void Awake()
    {
        base.Awake();

        if (difficultySelectButtons.Length != 3)
        {
            Debug.LogError("Should be 3 difficulty buttons!");
            return;
        }

        difficultySelectButtons[0].onClick.AddListener(() => LevelDifficulty.Instance.SetDifficulty(Difficulty.Easy));
        difficultySelectButtons[1].onClick.AddListener(() => LevelDifficulty.Instance.SetDifficulty(Difficulty.Medium));
        difficultySelectButtons[2].onClick.AddListener(() => LevelDifficulty.Instance.SetDifficulty(Difficulty.Hard));
    }
}
