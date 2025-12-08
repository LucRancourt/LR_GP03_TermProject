using _Project.Code.Core.MVC;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelection : BaseView
{
    [Tooltip("In order of: Easy - Medium - Hard")]
    [SerializeField] private Button[] buttons = new Button[3];
    [SerializeField] private Color selectedColor;


    protected override void Awake()
    {
        base.Awake();

        if (buttons.Length != 3)
        {
            Debug.LogError("Should be 3 difficulty buttons!");
            return;
        }

        buttons[0].onClick.AddListener(() => { SetLevelDifficulty(Difficulty.Easy); SetButtonSelected(0); } );
        buttons[1].onClick.AddListener(() => { SetLevelDifficulty(Difficulty.Medium); SetButtonSelected(1); });
        buttons[2].onClick.AddListener(() => { SetLevelDifficulty(Difficulty.Hard); SetButtonSelected(2); });
    }

    private void SetLevelDifficulty(Difficulty difficulty)
    {
        LevelDifficulty.Instance.SetDifficulty(difficulty);
    }

    private void SetButtonSelected(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = Color.white;
        }

        buttons[index].image.color = selectedColor;
    }
}
