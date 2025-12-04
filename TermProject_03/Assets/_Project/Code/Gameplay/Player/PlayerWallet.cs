using System;
using UnityEngine;

using _Project.Code.Core.General;
using _Project.Code.Core.MVC;


public class PlayerWallet : Singleton<PlayerWallet>
{
    [SerializeField] private int defaultFunds = 500;
    private int _wallet;

    public event Action<int> OnDataChanged;


    private void Start() { Initialize(); }

    public void Initialize() { SetWallet(defaultFunds); }

    private void SetWallet(int amount)
    {
        _wallet = amount;
        OnDataChanged?.Invoke(_wallet);
    }

    public void AddToWallet(int amount, bool applyDifficultyModifier = true)
    {
        if (applyDifficultyModifier)
            amount = (int)(amount * LevelDifficulty.Instance.DifficultyModifier);

        _wallet += amount;

        OnDataChanged?.Invoke(_wallet);
    }

    public bool SufficientFunds(int amount)
    {
        if (_wallet >= amount)
            return true;

        return false;
    }

    public bool MakeTransaction(int amount)
    {
        if (!SufficientFunds(amount)) return false;

        _wallet -= amount;
        OnDataChanged?.Invoke(_wallet);

        return true;
    }

    public void Dispose() {  }
}
