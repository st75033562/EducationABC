/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
//using UnityEngine.Advertisements;
using System;
using System.Collections.Generic;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com

[ExecuteInEditMode]
[RequireComponent(typeof(AdMob))]
[RequireComponent(typeof(ChartboostAd))]
[DisallowMultipleComponent]
public class AdsManager : MonoBehaviour
{
		/// <summary>
		/// The admob reference.
		/// </summary>
		private static AdMob admob;

		/// <summary>
		/// The chart boost ad reference.
		/// </summary>
		private static ChartboostAd chartBoostAd;

		/// <summary>
		/// This Gameobject defined as a Singleton.
		/// </summary>
		public static AdsManager instance;

		/// <summary>
		/// A list of AdPackage.
		/// </summary>
		public List<AdPackage> adPackages = new List<AdPackage> ();

		void Start ()
		{
				if (Application.isPlaying) {
						Init ();
				}
		}

		void Update ()
		{
				if (!Application.isPlaying) {
						Build ();
				}
		}

		/// <summary>
		/// Init this instance.
		/// </summary>
		private void Init ()
		{
				if (instance == null) {
						instance = this;
						DontDestroyOnLoad (gameObject);
						if (admob == null)
								admob = GetComponent<AdMob> ();
						if (chartBoostAd == null)
								chartBoostAd = GetComponent<ChartboostAd> ();
				} else {
						Destroy (gameObject);
				}
		}

		/// <summary>
		/// Build AdPackages & AdEvent lists.
		/// </summary>
		public void Build ()
		{
				BuildAdPackages ();
				foreach (AdPackage adPackage in adPackages) {
						adPackage.BuildAdEvents ();
				}
		}

		/// <summary>
		/// Show the advertisment.
		/// </summary>
		/// <param name="evt">Event.</param>
		public void ShowAdvertisment (AdPackage.AdEvent.Event evt)
		{
				if (adPackages == null) {
						return;
				}

				bool eventFound = false;
				foreach (AdPackage adPackage in adPackages) {

						if (!adPackage.isEnabled) {
								continue;
						}

						if (adPackage.adEvents == null) {
								return;
						}

						if (eventFound) {
								break;//single advertisment
						}

						foreach (AdPackage.AdEvent adEvent in adPackage.adEvents) {
								if (adEvent.evt == evt) {
										if (adEvent.isEnabled) {
												if (adPackage.package == AdPackage.Package.ADMOB) {
														AdMobAdvertisment (adEvent);
												} else if (adPackage.package == AdPackage.Package.CHARTBOOST) {
														ChartBoostAdvertisment (adEvent);
												} else if (adPackage.package == AdPackage.Package.UNITY) {
														UnityAdvertisment ();
												}
												eventFound = true;
										}
										break;
								}
						}		
				}
		}

		/// <summary>
		/// Hide the advertisment.
		/// </summary>
		public void HideAdvertisment ()
		{
			/*
				foreach (AdPackage adPackage in adPackages) {
						if (!adPackage.isEnabled) {
								continue;
						}
						if (adPackage.package == AdPackage.Package.ADMOB) {
								if (string.IsNullOrEmpty (admob.androidBannerAdUnitID) && string.IsNullOrEmpty (admob.IOSBannerAdUnitID)) {
										Debug.LogWarning ("Banner AdUnit is not defined in AdMob component");
										return;
								}

								admob.DestroyBanner ();
						}
				}
			*/
		}

		/// <summary>
		/// Show the Admob advertisment.
		/// </summary>
		/// <param name="adEvent">Ad event.</param>
		private void AdMobAdvertisment (AdPackage.AdEvent adEvent)
		{
			/*
				if (adEvent.type == AdPackage.AdEvent.Type.BANNER) {
						//Show admob banner advertisment
						if (string.IsNullOrEmpty (admob.androidBannerAdUnitID) && string.IsNullOrEmpty (admob.IOSBannerAdUnitID)) {
								Debug.LogWarning ("Banner AdUnit is not defined in AdMob component");
								return;
						}

						admob.RequestBanner (adEvent.adPostion);
				} else if (adEvent.type == AdPackage.AdEvent.Type.INTERSTITIAL) {
						//Show admob Interstitial Advertisment
						if (string.IsNullOrEmpty (admob.androidInterstitialAdUnitID) && string.IsNullOrEmpty (admob.IOSInterstitialAdUnitID)) {
								Debug.LogWarning ("Interstitial AdUnit is not defined in AdMob component");
								return;
						}
						admob.RequestInterstitial ();
				} else if (adEvent.type == AdPackage.AdEvent.Type.RewardBasedVideo) {
						if (string.IsNullOrEmpty (admob.androidRewardBasedVideoAdUnitID) && string.IsNullOrEmpty (admob.IOSRewardBasedVideoAdUnitID)) {
								Debug.LogWarning ("RewardBasedVideo AdUnit is not defined in AdMob component");
								return;
						}
						//Show admob RewardBasedVideo Advertisment
						admob.RequestRewardBasedVideo ();
				}
			*/
		}

		/// <summary>
		/// Show the ChartBoost advertisment.
		/// </summary>
		/// <param name="adEvent">Ad event.</param>
		private void ChartBoostAdvertisment (AdPackage.AdEvent adEvent)
		{
			/*
				if (adEvent.type == AdPackage.AdEvent.Type.INTERSTITIAL) {
						//Show chartboost Interstitial Advertisment
						chartBoostAd.ShowInterstitial ();
				} else if (adEvent.type == AdPackage.AdEvent.Type.RewardBasedVideo) {
						//Show chartboost RewardBasedVideo Advertisment
						chartBoostAd.ShowRewardedVideo ();
				}
			*/
		}

		/// <summary>
		/// Show the Unity advertisment.
		/// </summary>
		private void UnityAdvertisment ()
		{
			/*
				if (Advertisement.IsReady ()) {
						Advertisement.Show ();
				}
			*/
		}
	
		/// <summary>
		/// Build the ad Package.
		/// </summary>
		public void BuildAdPackages ()
		{
				Array adPackageEnum = Enum.GetValues (typeof(AdPackage.Package));

				if (NeedsToResetPackagesList (adPackageEnum, adPackages)) {
						adPackages.Clear ();
				}

				foreach (AdPackage.Package p in adPackageEnum) {
						if (!InAdPackagesList (adPackages, p)) {
								adPackages.Add (new AdPackage (){ package = p});
						}
				}
		}

		/// <summary>
		/// Whether the package in the adPackagees list
		/// </summary>
		/// <returns><c>true</c>, if package was found, <c>false</c> otherwise.</returns>
		/// <param name="adPackagees">Ad Packages List.</param>
		/// <param name="package">The given package.</param>
		public bool InAdPackagesList (List<AdPackage> adPackagees, AdPackage.Package package)
		{
				if (adPackages == null) {
						return false;
				}
		
				foreach (AdPackage adPackage in adPackages) {
							if (adPackage.package == package) {
								return true;
						}
				}
				return false;
		}

		/// <summary>
		/// Whether to reset Packages list or not.
		/// </summary>
		/// <returns><c>true</c>, if reset Packages list was needed, <c>false</c> otherwise.</returns>
		/// <param name="adPackageEnum">Ad Package enum.</param>
		/// <param name="adPackages">Ad Packages.</param>
		public bool NeedsToResetPackagesList (Array adPackageEnum, List<AdPackage> adPackages)
		{
				if (adPackageEnum.Length != adPackages.Count) {
						return true;
				}

				return false;
		}
}
