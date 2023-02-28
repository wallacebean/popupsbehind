using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using LLHandlers;
using LLGUI;
using GameplayEntities;
using TMPro;
using LLScreen;
using Debug = UnityEngine.Debug; // ADDED this line as well 

namespace popupsbehind
{
	[BepInPlugin("us.wallace.plugins.llb.popupsbehind", "popups behind Plug-In", "1.0.0.0")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			Debug.Log("Patching effects settings...");

			var harmony = new Harmony("us.wallace.plugins.llb.popupsbehind");
			harmony.PatchAll();
		}
	}
	class KShowGetReadyPatch
	{
		[HarmonyPatch(typeof(ScreenGameHud), nameof(ScreenGameHud.KShowGetReady), MethodType.Enumerator)]
		[HarmonyPrefix]
		static bool KShowGetReady_Prefix(ScreenGameHud __instance, float dur)

		{
			{
				float y = 40f;
				global::UnityEngine.RectTransform tfBar = global::LLGUI.LLControl.CreateImage(global::LLScreen.UIScreen.GetUIBack(), global::JPLELOFJOOH.BNFIDCAPPDK("_spriteWinBackdrop"), 0f, y).rectTransform;
				global::UnityEngine.RectTransform tfGetReady = global::LLGUI.LLControl.CreateImage(global::LLScreen.UIScreen.GetUIBack(), global::JPLELOFJOOH.BNFIDCAPPDK("_spriteGetReady"), 0f, y).rectTransform;
				global::KoroutineBehaviour.KoroutineId kId = __instance.SetFinalizer(delegate ()
				{
					global::UnityEngine.Object.Destroy(tfBar.gameObject);
					global::UnityEngine.Object.Destroy(tfGetReady.gameObject);
				});
				tfBar.localScale = new global::UnityEngine.Vector3(1.31f, 0.5f, 1f);
				tfGetReady.localPosition = new global::UnityEngine.Vector3(0f, y, 0f);
				tfGetReady.localScale = global::UnityEngine.Vector3.one * 0.8f;
				global::LLHandlers.AudioHandler.PlayAnnounce(global::LLHandlers.Sfx.ANN_GET_READY);
				__instance.StartKoroutine(kId, __instance.KAnimHor(dur, tfGetReady, 60f, -60f));
				__instance.StopKoroutine(kId, true);
				
			}
			return false;
		}
	}
	class KShowPlayBallPatch
	{
		[HarmonyPatch(typeof(ScreenGameHud), nameof(ScreenGameHud.KShowPlayBall), MethodType.Enumerator)]
		[HarmonyPrefix]
		static bool KShowPlayBall_Prefix(ScreenGameHud __instance, float dur)
		{
			global::UnityEngine.RectTransform tfBat = global::LLGUI.LLControl.CreateImage(global::LLScreen.UIScreen.GetUIBack(), global::JPLELOFJOOH.HPGOLPEOPLN("_spriteBat1", false), 0f, 0f).rectTransform;
			global::UnityEngine.RectTransform tfBat2 = global::LLGUI.LLControl.CreateImage(global::LLScreen.UIScreen.GetUIBack(), global::JPLELOFJOOH.HPGOLPEOPLN("_spriteBat2", false), 0f, 0f).rectTransform;
			global::KoroutineBehaviour.KoroutineId kId = __instance.SetFinalizer(delegate ()
			{
				global::UnityEngine.Object.Destroy(tfBat.gameObject);
				global::UnityEngine.Object.Destroy(tfBat2.gameObject);
			});
			tfBat.localScale = global::UnityEngine.Vector3.one * 0.8f;
			tfBat2.localScale = global::UnityEngine.Vector3.one * 0.8f;
			global::UnityEngine.UI.Image imPlayBall = global::LLGUI.LLControl.CreateImage(global::LLScreen.UIScreen.GetUIBack(), global::JPLELOFJOOH.HPGOLPEOPLN("_spritePlayBall", false), 0f, 0f);
			imPlayBall.transform.localScale = global::UnityEngine.Vector3.one * 0.8f;
			imPlayBall.enabled = false;
			float durMove = 0.1f;
			float xBat = 800f;
			float xBat2 = -800f;
			for (float f = 0f; f < 1f; f += global::UnityEngine.Time.deltaTime / durMove)
			{
				tfBat.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(xBat, 0f, f), 0f, 0f);
				tfBat2.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(xBat2, 0f, f), 0f, 0f);
				
			}
			imPlayBall.enabled = true;
			tfBat.anchoredPosition = global::UnityEngine.Vector3.zero;
			tfBat2.anchoredPosition = global::UnityEngine.Vector3.zero;
			global::LLHandlers.AudioHandler.PlayAnnounce(global::LLHandlers.Sfx.ANN_PLAY_BALL);
			new global::UnityEngine.WaitForSeconds(dur - (2f * durMove));
			imPlayBall.enabled = false;
			for (float f = 0f; f < 1f; f += global::UnityEngine.Time.deltaTime / durMove)
			{
				tfBat.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(0f, xBat, f), 0f, 0f);
				tfBat2.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(0f, xBat2, f), 0f, 0f);
				
			}
			__instance.StopKoroutine(kId, true);
			return false;
		}

	}


	class KShowBurstPatch
	{
		[HarmonyPatch(typeof(ScreenGameHud), nameof(ScreenGameHud.KShowBurst), MethodType.Enumerator)]
		[HarmonyPrefix]
		static bool KShowPlayBall_Prefix(ScreenGameHud __instance, int burstNr, float dur)
		{
			string text = burstNr.ToString();
			float num = 200f;
			int num2 = 0;
			if (num2 >= text.Length)
			{
				
			}
			global::UnityEngine.RectTransform tfBurst = global::LLGUI.LLControl.CreatePanel(global::LLScreen.UIScreen.tfUIRoot, "Burst", 0f, 0f);
			global::UnityEngine.RectTransform imBurst = global::LLGUI.LLControl.CreateImage(global::LLScreen.UIScreen.GetUIBack(), global::JPLELOFJOOH.HPGOLPEOPLN("_spriteBurst", false), 0f, 0f).rectTransform;
			int lndbodjbnfm = (int)(text[num2] - '0');
			global::UnityEngine.RectTransform imBurstNum = global::LLGUI.LLControl.CreateImage(global::LLScreen.UIScreen.GetUIBack(), global::JPLELOFJOOH.HPGOLPEOPLN("_spriteBurstNumbers", lndbodjbnfm, false), num, 0f).rectTransform;
			num += 72f;
			global::KoroutineBehaviour.KoroutineId kId = __instance.SetFinalizer(delegate ()
			{
				if (tfBurst != null)
				{
					global::UnityEngine.Object.Destroy(tfBurst.gameObject);
					global::UnityEngine.Object.Destroy(imBurst.gameObject);
					global::UnityEngine.Object.Destroy(imBurstNum.gameObject);
				}
			});
			try
			{
				if (global::NCMFHODLNAJ.JHONJFDBPNB == global::NMNBAFMOBNA.NLJIKMKLIMC)
				{
					
				}
				imBurst.transform.localScale = global::UnityEngine.Vector3.one * 0.8f;
				imBurstNum.transform.localScale = global::UnityEngine.Vector3.one * 0.8f;
				float durMoveFast = 0.15f;
				float durMoveSlow = dur - 2f * durMoveFast;
				float x2 = -160f;
				float x3 = 55f;
				float y = 160f;
				float z2 = 40f;
				float z3 = 255f;
				float zy = 160f;
				for (float f = 0f; f < 1f; f += global::UnityEngine.Time.deltaTime / durMoveFast)
				{
					tfBurst.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(x2 - 800f, x2, f), y, 0f);
					imBurst.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(x2 - 800f, x2, f), y, 0f);
					imBurstNum.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(z2 - 1000f, z2, f), zy, 0f);
					while (global::World.state == global::NMNBAFMOBNA.CBPEHBCCEMG)
					{
						
					}
					if (global::World.state == global::NMNBAFMOBNA.NLJIKMKLIMC)
					{
						f = 1f;
					}
					
					if (global::NCMFHODLNAJ.JHONJFDBPNB == global::NMNBAFMOBNA.NLJIKMKLIMC)
					{
						
					}
				}
				for (float f = 0f; f < 1f; f += global::UnityEngine.Time.deltaTime / durMoveSlow)
				{
					tfBurst.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(x2, x3, f), y, 0f);
					imBurst.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(x2, x3, f), y, 0f);
					imBurstNum.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(z2, z3, f), zy, 0f);
					while (global::World.state == global::NMNBAFMOBNA.CBPEHBCCEMG)
					{
						
					}
					if (global::World.state == global::NMNBAFMOBNA.NLJIKMKLIMC)
					{
						f = 1f;
					}
					
					if (global::NCMFHODLNAJ.JHONJFDBPNB == global::NMNBAFMOBNA.NLJIKMKLIMC)
					{
						
					}
				}
				for (float f = 0f; f < 1f; f += global::UnityEngine.Time.deltaTime / durMoveFast)
				{
					tfBurst.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(x3, x3 + 800f, f), y, 0f);
					imBurst.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(x3, x3 + 800f, f), y, 0f);
					imBurstNum.anchoredPosition = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Lerp(z3, z3 + 1072f, f), zy, 0f);
					while (global::World.state == global::NMNBAFMOBNA.CBPEHBCCEMG)
					{
						
					}
					if (global::World.state == global::NMNBAFMOBNA.NLJIKMKLIMC)
					{
						f = 1f;
					}
					
					if (global::NCMFHODLNAJ.JHONJFDBPNB == global::NMNBAFMOBNA.NLJIKMKLIMC)
					{
						
					}
				}
			}
			finally
			{
				__instance.StopKoroutine(kId, true);
			}
			return false;
		}
	}
	
}




