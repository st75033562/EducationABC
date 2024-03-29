/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com

public class SceneStartup : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		ShowAd ();
	}

	public void ShowAd ()
	{
		if (SceneManager.GetActiveScene ().name == "Logo") {
			AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_LOGO_SCENE);
		} else if (SceneManager.GetActiveScene ().name == "Main") {
			AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_MAIN_SCENE);
		} else if (SceneManager.GetActiveScene ().name == "LowercaseAlbum") {
			AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_LOWERCASE_SCENE);
		} else if (SceneManager.GetActiveScene ().name == "UppercaseAlbum") {
			AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_UPPERCASE_SCENE);
		} else if (SceneManager.GetActiveScene ().name == "NumbersAlbum") {
			AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_NUMBERS_SCENE);
		} else if (SceneManager.GetActiveScene ().name == "ScentenceAlbum") {
			AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_SCENTENCE_SCENE);
		} else if (SceneManager.GetActiveScene ().name == "Game") {
			AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_GAME_SCENE);
		}
	}

	void OnDestroy ()
	{
		AdsManager.instance.HideAdvertisment ();
	}
}
