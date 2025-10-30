using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    // Variables
    private float _wallet;


    // Functions
    public void AddToWallet(float amount)
    {
        _wallet += amount;

        Debug.Log(_wallet);
    }

    public bool MakeTransaction(float amount)
    {
        if (_wallet >= amount)
        {
            _wallet -= amount;

            Debug.Log(_wallet);


            return true;
        }

        return false;
    }
}
