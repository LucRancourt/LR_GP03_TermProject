using UnityEngine;

public class PlayerWallet
{
    private float _wallet = 10000.0f;


    public void AddToWallet(float amount)
    {
        _wallet += amount;

        Debug.Log(_wallet);
    }

    public bool SufficientFunds(float amount)
    {
        if (_wallet >= amount)
            return true;

        return false;
    }

    public bool MakeTransaction(float amount)
    {
        if (!SufficientFunds(amount)) return false;

        _wallet -= amount;

        Debug.Log(_wallet);

        return true;
    }
}
