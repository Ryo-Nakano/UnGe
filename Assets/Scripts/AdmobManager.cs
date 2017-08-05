using UnityEngine;
using System.Collections;
using admob;

public class AdmobManager : MonoBehaviour
{

	public static AdmobManager instance;

	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);

		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("start unity demo-------------");
		initAdmob ();
		showbanner ();
		//ad.loadInterstitial ();
		Debug.Log ("反応してるよ");
	}

	void showbanner ()
	{
		Admob.Instance ().showBannerRelative (AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
		Debug.Log ("okbanner");
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Debug.Log (KeyCode.Escape + "-----------------");
			// ad.removeAllBanner();
		}
	}

	public Admob ad;
	//bool isAdmobInited = false;
	void initAdmob ()
	{

		//  isAdmobInited = true;
		ad = Admob.Instance ();
		ad.bannerEventHandler += onBannerEvent;
		ad.interstitialEventHandler += onInterstitialEvent;
		ad.rewardedVideoEventHandler += onRewardedVideoEvent;
		ad.nativeBannerEventHandler += onNativeBannerEvent;
		ad.initAdmob("ca-app-pub-8142952487566928/7403954297", "ca-app-pub-3856030372876560/4873797139");
			//   ad.setTesting(true);
			ad.setGender (AdmobGender.MALE);
			string[] keywords = { "game", "crash", "male game" };
			ad.setKeywords (keywords);
			Debug.Log ("admob inited -------------");

			}

			public void LoadInterStitial ()
			{
				if (ad.isInterstitialReady ()) {
					ad.showInterstitial ();
					Debug.Log ("okinterstitial");
				}
			}



			//    void OnGUI ()
			//    {
			//        if (GUI.Button (new Rect (120, 0, 100, 60), “showInterstitial”)) {
			//          
			////            if (ad.isInterstitialReady ()) {
			////                ad.showInterstitial ();
			////                Debug.Log (“showInterstitial”);
			////            } else {
			////                ad.loadInterstitial ();
			////                Debug.Log (“loadInterstitial”);
			//            }
			//
			//        if (GUI.Button (new Rect (240, 0, 100, 60), “showRewardVideo”)) {
			//           
			//            if (ad.isRewardedVideoReady ()) {
			//                ad.showRewardedVideo ();
			//            } else {
			//                ad.loadRewardedVideo (“ca-app-pub-3940256099942544/xxxxxxxxxx”);
			//            }
			//        }
			//        if (GUI.Button (new Rect (0, 100, 100, 60), “showbanner”)) {
			//            Admob.Instance ().showBannerRelative (AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
			//            Debug.Log (“showbanner”);
			//        }
			//        if (GUI.Button (new Rect (120, 100, 100, 60), “showbannerABS”)) {
			//            Admob.Instance ().showBannerAbsolute (AdSize.Banner, 0, 300);
			//        }
			//        if (GUI.Button (new Rect (240, 100, 100, 60), “removebanner”)) {
			//            Admob.Instance ().removeBanner ();
			//        }
			//      
			//        string nativeBannerID = “ca-app-pub-3940256099942544/2562852117”;//google
			//        if (GUI.Button (new Rect (0, 200, 100, 60), “showNative”)) {
			//          
			//        Admob.Instance ().showNativeBannerRelative (new AdSize (320, 120), AdPosition.BOTTOM_CENTER, 0, nativeBannerID);
			//    }
			//        if (GUI.Button (new Rect (120, 200, 100, 60), “showNativeABS”)) {
			//            Admob.Instance ().showNativeBannerAbsolute (new AdSize (320, 120), 0, 300, nativeBannerID);
			//        }
			//        if (GUI.Button (new Rect (240, 200, 100, 60), “removeNative”)) {
			//            Admob.Instance ().removeNativeBanner ();
			//        }
			//    }

			void onInterstitialEvent (string eventName, string msg)
			{
				Debug.Log ("handler onAdmobEvent---" + eventName + "   " + msg);
				if (eventName == AdmobEvent.onAdLoaded) {
					Admob.Instance ().showInterstitial ();
				}
			}

			void onBannerEvent (string eventName, string msg)
			{
				Debug.Log ("handler onAdmobBannerEvent---" + eventName + "   " + msg);
			}

			void onRewardedVideoEvent (string eventName, string msg)
			{
				Debug.Log ("handler onRewardedVideoEvent---" + eventName + "   " + msg);
			}

			void onNativeBannerEvent (string eventName, string msg)
			{
				Debug.Log ("handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
			}
			}