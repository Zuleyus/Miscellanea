using BepInEx.Configuration;

using HarmonyLib;

using UnityEngine;

namespace Miscellanea.Patches;

[HarmonyPatch]
public static class AssistMA {
	public static ConfigEntry<KeyCode> TargetMainAssistKey;

	static AssistMA() {
		TargetMainAssistKey = Miscellanea.Instance.Config.Bind(
			"Assist MA",
			"Target MainAssist Key",
			KeyCode.T,
			"Which key to use to target the MA's target"
		);
	}

	[HarmonyPrefix, HarmonyPatch(typeof(PlayerControl), "TargetHotkeys")]
	static void PlayerControl_TargetHotkeys_Prefix(PlayerControl __instance) {
		if (Input.GetKeyDown(AssistMA.TargetMainAssistKey.Value) && GameData.SimPlayerGrouping.MainAssist.MyStats.Myself.MyNPC.CurrentAggroTarget is not null) {
			__instance.CurrentTarget?.UntargetMe();
			__instance.CurrentTarget = GameData.SimPlayerGrouping.MainAssist.MyStats.Myself.MyNPC.CurrentAggroTarget;
			__instance.CurrentTarget.TargetMe();
		}
	}
}
