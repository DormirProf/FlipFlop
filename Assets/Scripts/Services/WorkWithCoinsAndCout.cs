using Scripts.Secure;

namespace Scripts.Services
{
    public struct WorkWithCoinsAndCout 
    {
        private SafeInt _coin;
        private SafeInt _cout;

        public SafeInt Coin => _coin;
        public SafeInt Cout => _cout;

        public void AddCout() => 
            _cout = new SafeInt(_cout + 1);
        public void CleanCout() => 
            _cout = new SafeInt(0);
        public void CoinUpdate() => 
            _coin = new SafeInt(PlayerPrefsSafe.GetInt("savescoins"));

        public void TakeCoin(int value) 
        {
            _coin = new SafeInt(PlayerPrefsSafe.GetInt("savescoins"));
            _coin -= new SafeInt(value);
            PlayerPrefsSafe.SetInt("savescoins", Coin);
        }

        public void AddCoin(int value) 
        {
            _coin = new SafeInt(PlayerPrefsSafe.GetInt("savescoins"));
            _coin += new SafeInt(value);
            PlayerPrefsSafe.SetInt("savescoins", Coin);
        }
    }   
}