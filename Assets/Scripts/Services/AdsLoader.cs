using GoogleMobileAds.Api;

namespace Scripts.Services
{
    public struct AdsLoader
    {
        private static InterstitialAd _ads  = null;
        private static AdRequest _request  = null;
        private static BannerView _banner  = null;
        private static bool _isBannerActive = false;

        public static BannerView Banner => _banner;
        public static bool BannerActive => _isBannerActive;

        public static void LoadBaner()
        {
            var txtBanner = "ca-app-pub-9683150824833016/4358022226";
            _isBannerActive = false;
            _banner = new BannerView(txtBanner, AdSize.Banner, AdPosition.Bottom);
            _request = new AdRequest.Builder().Build();
            _banner.LoadAd(_request);
        }
    
        public static void ShowAds(){
            var txtloseBlock = "ca-app-pub-9683150824833016/6410659862";
            _ads = new InterstitialAd(txtloseBlock);
            AdRequest request = new AdRequest.Builder().Build();
            _ads.LoadAd(request);
            _ads.OnAdLoaded += OnAdLoaded;
        }

        private static void OnAdLoaded(object sender, System.EventArgs args) =>  _ads.Show();
    }
}
