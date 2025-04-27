using BepInEx;
using BepInEx.Logging;

using HarmonyLib;

namespace Miscellanea;

public static class PluginInfo {
	public const string PLUGIN_GUID = "Zuleyus.Erenshor.Miscellanea";
	public const string PLUGIN_AUTHOR = "Zuleyus";
	public const string PLUGIN_NAME = "Miscellanea";
	public const string PLUGIN_NAME_INTERNAL = "Miscellanea";
	public const string PLUGIN_VERSION = "1.0.0";
}


/* Module ideas:
 * - Chat improvements:
 *   - (-> separate mod) add classes for chat channels
 *   - timestamps
 *   - move mana regen messages to combat log (too spammy for main chat)
 * - T for MA target
 * - show current values in rep messages
 */

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Miscellanea : BaseUnityPlugin
{
	public static Miscellanea Instance { get; private set; }

	private void Awake()
	{
		var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
		harmony.PatchAll();

		Instance = this;
		Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
	}


	internal static void LogSay(object payload) {
		Log(payload);
		UpdateSocialLog.LogAdd(payload as string);
	}

	internal static void Log(object payload) {
#if DEBUG
		LogMsg(payload);
#else
		LogInfo(payload);
#endif
	}

	internal static void LogWarning(object payload) {
		Instance.Logger.Log(LogLevel.Warning, payload);
	}

	internal static void LogMsg(object payload) {
		Instance.Logger.Log(LogLevel.Message, payload);
	}

	internal static void LogInfo(object payload) {
		Instance.Logger.Log(LogLevel.Info, payload);
	}

	internal static void LogDebug(object payload) {
#if DEBUG
		LogInfo(payload);
#else
		Instance.Logger.Log(LogLevel.Debug, payload);
#endif
	}
}
