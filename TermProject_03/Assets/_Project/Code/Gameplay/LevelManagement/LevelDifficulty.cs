using _Project.Code.Core.General;


public class LevelDifficulty : Singleton<LevelDifficulty>
{
    private static readonly float _easyModifier = 1.0f;
    private static readonly float _mediumModifier = 0.72f;
    private static readonly float _hardModifier = 0.45f;

    public float DifficultyModifier { get; private set; } = _mediumModifier;

    public void SetDifficulty(Difficulty selectedDifficulty)
    {
        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                DifficultyModifier = _easyModifier;
                break;

            case Difficulty.Medium:
                DifficultyModifier = _mediumModifier;
                break;

            case Difficulty.Hard:
                DifficultyModifier = _hardModifier;
                break;
        }
    }
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}