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
using UnityEditor;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com

[CustomEditor (typeof(AdsManager))]
public class AdsManagerEditor :  Editor
{
	public int selectedPackage;
	private static bool showInstructions = true;
	private static string[] Packages = null;
	private static string[] downloadURlS = new string[] {
		"https://github.com/googleads/googleads-mobile-unity/releases",
		"http://www.chartboo.st/sdk/unity",
		""
	};
	private static string[] moreDetails = new string[] {
		"https://firebase.google.com/docs/admob/unity/start",
		"https://answers.chartboost.com/hc/en-us/articles/200780379-Download-Integrate-the-Chartboost-SDK-for-Unity#integration",
		"https://docs.unity3d.com/Manual/UnityAdsHowTo.html"
	};

	public override void OnInspectorGUI ()
	{
		if (Application.isPlaying) {
			return;
		}

		AdsManager attrib = (AdsManager)target;//get the target
		if (Packages == null) {
			selectedPackage = 0;
			System.Array packagesEnum = System.Enum.GetValues (typeof(AdPackage.Package));
			if (packagesEnum.Length == 0) {
				return;
			}

			Packages = new string[packagesEnum.Length];
			for (int i = 0; i < packagesEnum.Length; i++) {
				Packages [i] = packagesEnum.GetValue (i).ToString ();
			}
		}

		EditorGUILayout.Separator ();
		selectedPackage = GUILayout.Toolbar (selectedPackage, Packages);
		EditorGUILayout.Separator ();

		for (int i = 0; i < attrib.adPackages.Count; i++) {
			if (selectedPackage != i) {
				continue;
			}
							
			ShowInstruction (attrib.adPackages [i].package);

			EditorGUILayout.Separator ();
			EditorGUILayout.BeginHorizontal ();
			if (attrib.adPackages [i].package != AdPackage.Package.UNITY) {
				GUI.backgroundColor = Colors.greenColor;
				if (GUILayout.Button ("Download " + Packages [selectedPackage], GUILayout.Width (180), GUILayout.Height (20))) {
					Application.OpenURL (downloadURlS [i]);
				}
				GUI.backgroundColor = Colors.whiteColor;
			}

			GUI.backgroundColor = Colors.greenColor;
			if (GUILayout.Button ("More Details About " + Packages [selectedPackage], GUILayout.Width (220), GUILayout.Height (20))) {
				Application.OpenURL (moreDetails [i]);
			}
			GUI.backgroundColor = Colors.whiteColor;
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Separator ();
			attrib.adPackages [i].isEnabled = EditorGUILayout.Toggle ("Enable " + Packages [selectedPackage] + " ADS", attrib.adPackages [i].isEnabled);
			EditorGUILayout.Separator ();

			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Event");
			GUILayout.Space (40);
			GUILayout.Label ("Ad Type");
			GUILayout.Space (20);
			GUILayout.Label ("Ad Position");
			GUILayout.Label ("Active");
			
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Separator ();
			foreach (AdPackage.AdEvent adEvent in attrib.adPackages[i].adEvents) {
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.EnumPopup (adEvent.evt);
				EditorGUI.BeginDisabledGroup (attrib.adPackages [i].package == AdPackage.Package.UNITY);
				adEvent.type = (AdPackage.AdEvent.Type)EditorGUILayout.EnumPopup (adEvent.type);
				if (adEvent.type == AdPackage.AdEvent.Type.BANNER && attrib.adPackages [i].package == AdPackage.Package.CHARTBOOST) {
					adEvent.type = AdPackage.AdEvent.Type.INTERSTITIAL;
				}
				EditorGUI.EndDisabledGroup ();

				EditorGUI.BeginDisabledGroup (adEvent.type != AdPackage.AdEvent.Type.BANNER || attrib.adPackages [i].package == AdPackage.Package.CHARTBOOST || attrib.adPackages [i].package == AdPackage.Package.UNITY);
				//adEvent.adPostion = (GoogleMobileAds.Api.AdPosition)EditorGUILayout.EnumPopup (adEvent.adPostion);
				EditorGUI.EndDisabledGroup ();
				
				adEvent.isEnabled = EditorGUILayout.Toggle (adEvent.isEnabled);
				EditorGUILayout.EndHorizontal ();
			}
		}
		EditorGUILayout.Separator ();

		if (GUI.changed) {
			DirtyUtil.MarkSceneDirty ();
		}
	}

			
	private void ShowInstruction (AdPackage.Package package)
	{
		EditorGUILayout.Separator ();
		EditorGUILayout.HelpBox ("Follow the instructions below on how to enable the " + Packages [selectedPackage] + " Advertisements", MessageType.Info);
		EditorGUILayout.Separator ();
		showInstructions = EditorGUILayout.Foldout (showInstructions, "Instructions");
		EditorGUILayout.Separator ();

		if (!showInstructions) {
			return;
		}

		EditorGUILayout.BeginHorizontal ();
		ShowReadManual ();
		ShowContactUS ();
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Separator ();

		if (package == AdPackage.Package.ADMOB) {
			ShowAdmobInstructions ();
		} else if (package == AdPackage.Package.CHARTBOOST) {
			ShowChartBoostInstructions ();
		} else if (package == AdPackage.Package.UNITY) {
			ShowUnityAdsInstructions ();
		}
		ShowCommonInstructions ();
					
		EditorGUILayout.Separator ();
	}

	private void ShowReadManual ()
	{
		GUI.backgroundColor = Colors.yellowColor;
		if (GUILayout.Button ("Read the Manual", GUILayout.Width (120), GUILayout.Height (20))) {
			Application.OpenURL (Links.docPath);
		}
		GUI.backgroundColor = Colors.whiteColor;
	}

	private void ShowContactUS ()
	{
		GUI.backgroundColor = Colors.greenColor;
		if (GUILayout.Button ("Contact US", GUILayout.Width (100), GUILayout.Height (20))) {
			Application.OpenURL (Links.indieStudioContactUsURL);
		}
		GUI.backgroundColor = Colors.whiteColor;
	}

	private void ShowCommonInstructions ()
	{
		EditorGUILayout.Separator ();
		EditorGUILayout.HelpBox ("* If an active Event is found in more than one Package , the script will execute the first one", MessageType.None);
		EditorGUILayout.HelpBox ("* If you any questions , suggestions or problems you can contact us", MessageType.None);
		EditorGUILayout.Separator ();
	}

	private void ShowAdmobInstructions ()
	{
		EditorGUILayout.HelpBox ("1. Download Admob package, and then import it to your project", MessageType.None);
		EditorGUILayout.HelpBox ("2. Important - Read 'Setup Admob Advertisements' section in the Manual to enable the Admob Advertisements", MessageType.None);
		EditorGUILayout.HelpBox ("3. After applying the previous step ,insert your Admob UnitID in the Ad Mob component below", MessageType.None);
		EditorGUILayout.HelpBox ("4. Modify the attributes below as you wish", MessageType.None);
		EditorGUILayout.HelpBox ("5. Click on Apply button that located on the top to save your changes", MessageType.None);
	}

	private void ShowChartBoostInstructions ()
	{
		EditorGUILayout.HelpBox ("1. Download ChartBoost package, and then import it to your project", MessageType.None);
		EditorGUILayout.HelpBox ("2. Insert your ChartBoost AppID & App Signiture using ChartBoost->Edit Settings", MessageType.None);
		EditorGUILayout.HelpBox ("3. Important - Read 'Setup ChartBoost Advertisements' section in the Manual to enable the ChartBoost Advertisements", MessageType.None);
		EditorGUILayout.HelpBox ("4. Modify the attributes below as you wish", MessageType.None);
		EditorGUILayout.HelpBox ("5. Click on Apply button that located on the top to save your changes", MessageType.None);
	}

	private void ShowUnityAdsInstructions ()
	{
		EditorGUILayout.HelpBox ("1. Link your App and then enable Unity ADS from Window->Services", MessageType.None);
		EditorGUILayout.HelpBox ("3. Important - Read 'Setup Unity Advertisements' section in the Manual to enable the Unity Advertisements", MessageType.None);
		EditorGUILayout.HelpBox ("4. Modify the attributes below as you wish", MessageType.None);
		EditorGUILayout.HelpBox ("5. Click on Apply button that located on the top to save your changes", MessageType.None);
	}
}
