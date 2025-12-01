using System;

using _Project.Code.Core.General;
using _Project.Code.Core.MVC;


public class PlayerWallet : Singleton<PlayerWallet>, IModel
{
    private int _wallet = 0;

    public event Action OnDataChanged;


    public void Initialize() { SetWallet(500); }

    private void SetWallet(int amount)
    {
        _wallet = amount;
        OnDataChanged?.Invoke();
    }

    public void AddToWallet(int amount)
    {
        _wallet += amount;
        OnDataChanged?.Invoke();
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
        OnDataChanged?.Invoke();

        return true;
    }

    public void Dispose() {  }
}
