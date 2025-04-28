//using System.Collections.Generic;
//using System.Reflection.Emit;

//using BepInEx.Configuration;

//using HarmonyLib;

//using UnityEngine;

//namespace Miscellanea.Patches;
//internal class ShowAbsoluteFactionValuesPatch {
//	public static ConfigEntry<bool> ShowAbsoluteFactionValues;

//	public ShowAbsoluteFactionValuesPatch() {
//		ShowAbsoluteFactionValues = Miscellanea.Instance.Config.Bind(
//			"Faction Values",
//			"Show Absolute Faction Values",
//			true,
//			"Add total faction standing value to the messages of rep gain/loss?"
//		);
//	}

//	static string GetFactionModificationAsString(NPCFaction _ref, float _mod) {
//		return $"! {_mod} -> {_ref.Value}";
//	}

//	[HarmonyTranspiler, HarmonyPatch(typeof(GlobalFactionManager), nameof(GlobalFactionManager.ModifyFaction))]
//	static IEnumerable<CodeInstruction> GlobalFactionManager_ModifyFaction_Transpiler(IEnumerable<CodeInstruction> instructions) {
//		Miscellanea.LogDebug("=== Patching GlobalFactionManager.ModifyFaction...");
//#if DEBUG
//		Miscellanea.Log("=== Original GlobalFactionManager.ModifyFaction");
//		instructions.Do(Debug.Log);
//#endif

//#if DEBUG
//		var instructions1 = new CodeMatcher(instructions)
//#else
//		return new CodeMatcher(instructions)
//#endif

//			//	// NPCFaction nPCFaction = FindFactionData(_ref);
//			//	/* 0x000149D8 03                 */ IL_0000: ldarg.1
//			//	/* 0x000149D9 2887010006         */ IL_0001: call class NPCFaction GlobalFactionManager::FindFactionData(string) /* 100663687 */
//			//	/* 0x000149DE 0A                 */ IL_0006: stloc.0
//			//	// if (nPCFaction != null)
//			//	/* 0x000149DF 06                 */ IL_0007: ldloc.0
//			//	/* 0x000149E0 39A4000000         */ IL_0008: brfalse IL_00b1

//			//	// FindFactionData(_ref).Value += _mod;
//			//	/* 0x000149E5 03                 */ IL_000d: ldarg.1
//			//	/* 0x000149E6 2887010006         */ IL_000e: call class NPCFaction GlobalFactionManager::FindFactionData(string) /* 100663687 */
//			//	/* 0x000149EB 25                 */ IL_0013: dup
//			//	/* 0x000149EC 7B2C040004         */ IL_0014: ldfld float32 NPCFaction::Value /* 67109932 */
//			//	/* 0x000149F1 02                 */ IL_0019: ldarg.0
//			//	/* 0x000149F2 58                 */ IL_001a: add
//			//	/* 0x000149F3 7D2C040004         */ IL_001b: stfld float32 NPCFaction::Value /* 67109932 */
//			//	// if (_mod < 0f)
//			//	/* 0x000149F8 02                 */ IL_0020: ldarg.0
//			//	/* 0x000149F9 2200000000         */ IL_0021: ldc.r4 0.0
//			//	/* 0x000149FE 3441               */ IL_0026: bge.un.s IL_0069

//			.MatchStartForward(
//				new CodeMatch(OpCodes.Ldarg_1),
//				new CodeMatch(OpCodes.Call),
//				new CodeMatch(OpCodes.Stloc_0),
//				new CodeMatch(OpCodes.Ldloc_0),
//				new CodeMatch(OpCodes.Brfalse),

//				new CodeMatch(OpCodes.Ldarg_1),
//				new CodeMatch(OpCodes.Call),
//				new CodeMatch(OpCodes.Dup),
//				new CodeMatch(OpCodes.Ldfld),
//				new CodeMatch(OpCodes.Ldarg_0),
//				new CodeMatch(OpCodes.Add),
//				new CodeMatch(OpCodes.Stfld),
//				new CodeMatch(OpCodes.Ldarg_0),
//				new CodeMatch(OpCodes.Ldc_R4),
//				new CodeMatch(OpCodes.Bge_Un_S)
//				)
//			.Advance(6)
//			.SetInstruction(
//				new CodeInstruction(OpCodes.Ldloc_0)
//			)

//#if DEBUG
//			.InstructionEnumeration();

//		Miscellanea.Log("=== Patched GlobalFactionManager.ModifyFaction 1");
//		instructions1.Do(Debug.Log);

//		var instructions2 = new CodeMatcher(instructions1, null)
//#endif

//			//	// UpdateSocialLog.LogAdd("You've lost standing with " + nPCFaction.Desc + "!", "grey");
//			//	/* 0x00014A00 7234B90070         */ IL_0028: ldstr "You've lost standing with " /* 1879095604 */
//			//	/* 0x00014A05 06                 */ IL_002d: ldloc.0
//			//	/* 0x00014A06 7B2B040004         */ IL_002e: ldfld string NPCFaction::Desc /* 67109931 */
//			//	/* 0x00014A0B 726AB90070         */ IL_0033: ldstr "!" /* 1879095658 */
//			//	/* 0x00014A10 284A00000A         */ IL_0038: call string [netstandard]System.String::Concat(string, string, string) /* 167772234 */
//			//	/* 0x00014A15 72A2050070         */ IL_003d: ldstr "grey" /* 1879049634 */
//			//	/* 0x00014A1A 2894060006         */ IL_0042: call string UpdateSocialLog::LogAdd(string, string) /* 100664980 */
//			//	/* 0x00014A1F 26                 */ IL_0047: pop

//			.MatchStartForward(
//				new CodeMatch(OpCodes.Ldstr),
//				new CodeMatch(OpCodes.Ldloc_0),
//				new CodeMatch(OpCodes.Ldfld),
//				new CodeMatch(OpCodes.Ldstr),
//				new CodeMatch(OpCodes.Call),
//				new CodeMatch(OpCodes.Ldstr),
//				new CodeMatch(OpCodes.Call),
//				new CodeMatch(OpCodes.Pop)
//				)
//			.Advance(5)
//			//.SetInstruction(
//			//	new CodeInstruction(Transpilers.EmitDelegate(GetCurrentFameLvCap))
//			//)
//#if DEBUG
//			.InstructionEnumeration();

//		Miscellanea.Log("=== Patched GlobalFactionManager.ModifyFaction 1");
//		instructions2.Do(Debug.Log);

//		var instructions3 = new CodeMatcher(instructions2)
//#endif

//			//	// UpdateSocialLog.LocalLogAdd("You've lost standing with " + nPCFaction.Desc + "!", "grey");
//			//	/* 0x00014A20 7234B90070         */ IL_0048: ldstr "You've lost standing with " /* 1879095604 */
//			//	/* 0x00014A25 06                 */ IL_004d: ldloc.0
//			//	/* 0x00014A26 7B2B040004         */ IL_004e: ldfld string NPCFaction::Desc /* 67109931 */
//			//	/* 0x00014A2B 726AB90070         */ IL_0053: ldstr "!" /* 1879095658 */
//			//	/* 0x00014A30 284A00000A         */ IL_0058: call string [netstandard]System.String::Concat(string, string, string) /* 167772234 */
//			//	/* 0x00014A35 72A2050070         */ IL_005d: ldstr "grey" /* 1879049634 */
//			//	/* 0x00014A3A 289A060006         */ IL_0062: call string UpdateSocialLog::LocalLogAdd(string, string) /* 100664986 */
//			//	/* 0x00014A3F 26                 */ IL_0067: pop

//			.MatchStartForward(
//				new CodeMatch(OpCodes.Ldc_I4_S),
//				new CodeMatch(OpCodes.Call),
//				new CodeMatch(OpCodes.Starg_S),
//				new CodeMatch(OpCodes.Br)
//			)
//			//.SetInstruction(
//			//	new CodeInstruction(Transpilers.EmitDelegate(GetCurrentFameLvCap))
//			//)
//			.InstructionEnumeration();

//#if DEBUG
//		Miscellanea.Log("=== Patched GlobalFactionManager.ModifyFaction 2");
//		instructions3.Do(Debug.Log);

//		return instructions3;
//#endif

//	}

//public static void ModifyFaction(float _mod, string _ref) {
//		NPCFaction nPCFaction = GlobalFactionManager.FindFactionData(_ref);
//		if (nPCFaction != null) {
//			GlobalFactionManager.FindFactionData(_ref).Value += _mod;
//			if (_mod < 0f) {
//				UpdateSocialLog.LogAdd("You've lost standing with " + nPCFaction.Desc + "!", "grey");
//				UpdateSocialLog.LocalLogAdd("You've lost standing with " + nPCFaction.Desc + "!", "grey");
//			} else if (_mod > 0f) {
//				UpdateSocialLog.LogAdd("You've gained standing with " + nPCFaction.Desc + "!", "grey");
//				UpdateSocialLog.LocalLogAdd("You've gained standing with " + nPCFaction.Desc + "!", "grey");
//			}
//		}
//	}
//}
