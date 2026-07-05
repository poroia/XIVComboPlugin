using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.Configuration;
using Dalamud.Utility;
using Newtonsoft.Json;
using XIVCombo.Attributes;
using XIVCombo.Combos;
using XIVCombo.Interface;

namespace XIVCombo;

/// <summary>
/// Plugin configuration.
/// </summary>
[Serializable]
public class PluginConfiguration : IPluginConfiguration
{
    private static readonly Dictionary<CustomComboPreset, CustomComboPreset[]> ConflictingCombos;
    private static readonly Dictionary<CustomComboPreset, CustomComboPreset?> ParentCombos;  // child: parent

    static PluginConfiguration()
    {

        ConflictingCombos = Enum.GetValues<CustomComboPreset>()
            .Distinct() // Prevent ArgumentExceptions from adding the same int twice, should not be seen anymore
            .ToDictionary(
                preset => preset,
                preset => preset.GetAttribute<ConflictingCombosAttribute>()?.ConflictingPresets ?? Array.Empty<CustomComboPreset>());

        ParentCombos = Enum.GetValues<CustomComboPreset>()
            .Distinct() // Prevent ArgumentExceptions from adding the same int twice, should not be seen anymore
            .ToDictionary(
                preset => preset,
                preset => preset.GetAttribute<ParentComboAttribute>()?.ParentPreset);
    }

    /// <summary>
    /// Gets or sets the configuration version.
    /// </summary>
    public int Version { get; set; } = 5;

    /// <summary>
    /// Gets or sets the collection of enabled combos.
    /// </summary>
    [JsonProperty("EnabledActionsV5")]
    public HashSet<CustomComboPreset> EnabledActions { get; set; } = new();

    /// <summary>
    /// Gets or sets the first time pop-up bool.
    /// </summary>
    public bool OneTimePopUp { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to enable the plugin or not.
    /// </summary>
    [JsonProperty("Plugin")]
    public bool EnablePlugin { get; set; } = true;

	/// <summary>
	/// Gets or sets a value indicating whether to allow and display he default theme.
	/// </summary>
	[JsonProperty("Theme")]
	public bool EnableTheme { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating which is the current tab.
    /// </summary>
    [JsonProperty("Tab")]
    public string CurrentJobTab { get; set; } = "Paladin";

    /// <summary>
    /// Gets or sets a value indicating whether the plugin automatically changes to the current job upon opening the GUI.
    /// </summary>
    public bool AutoJobChange { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to hide the Ko-Fi link.
    /// </summary>
    public bool HideKofi { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to hide the children of a feature if it is disabled.
    /// </summary>
    public bool HideChildren { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to hide the icons of a feature.
    /// </summary>
    public bool HideIcons { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to increase the icons of the jobs on the sidebar or not.
    /// </summary>
    public bool BigJobIcons { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether increase the icons featured in the combo lists or not.
    /// </summary>
    public bool BigComboIcons { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to tint hotbar icons based on combo state.
    /// </summary>
    [JsonProperty("EnableIconRecoloring")]
    public bool EnableIconRecoloring { get; set; } = true;

    /// <summary>
    /// Gets or sets the method used to apply combo tints to hotbar icons.
    /// </summary>
    [JsonProperty("IconColoringMethod")]
    public IconColoringMethod ColoringMethod { get; set; } = IconColoringMethod.Vibrant;

    /// <summary>
    /// Gets or sets the per-combo hotbar icon tints, keyed by preset.
    /// Colors are 0xAARRGGBB; the alpha component is the tint intensity (0 = disabled).
    /// </summary>
    [JsonProperty("ComboTintColors")]
    public Dictionary<CustomComboPreset, uint> ComboTints { get; set; } = new();

    /// <summary>
    /// Gets or sets the per-combo coloring method overrides, keyed by preset.
    /// Presets not in the dictionary use the global <see cref="ColoringMethod"/>.
    /// </summary>
    [JsonProperty("ComboTintMethods")]
    public Dictionary<CustomComboPreset, IconColoringMethod> ComboTintMethods { get; set; } = new();

    /// <summary>
    /// Save the configuration to disk.
    /// </summary>
    public void Save()
        => Service.Interface.SavePluginConfig(this);

    /// <summary>
    /// Gets a value indicating whether a preset is enabled.
    /// </summary>
    /// <param name="preset">Preset to check.</param>
    /// <returns>The boolean representation.</returns>
    public bool IsEnabled(CustomComboPreset preset)
        => this.EnabledActions.Contains(preset);

    /// <summary>
    /// Gets an array of conflicting combo presets.
    /// </summary>
    /// <param name="preset">Preset to check.</param>
    /// <returns>The conflicting presets.</returns>
    public CustomComboPreset[] GetConflicts(CustomComboPreset preset)
        => ConflictingCombos[preset];

    /// <summary>
    /// Gets the parent combo preset if it exists, or null.
    /// </summary>
    /// <param name="preset">Preset to check.</param>
    /// <returns>The parent preset.</returns>
    public CustomComboPreset? GetParent(CustomComboPreset preset)
        => ParentCombos[preset];

    /// <summary>
    /// Gets the configured hotbar icon tint for a combo, or null when none is set.
    /// </summary>
    /// <param name="preset">Preset to check.</param>
    /// <returns>The tint as 0xAARRGGBB (alpha = intensity), or null.</returns>
    public uint? GetComboTint(CustomComboPreset preset)
        => this.ComboTints.TryGetValue(preset, out var color) ? color : null;

    /// <summary>
    /// Sets the hotbar icon tint for a combo.
    /// </summary>
    /// <param name="preset">Preset to configure.</param>
    /// <param name="color">The tint as 0xAARRGGBB (alpha = intensity).</param>
    public void SetComboTint(CustomComboPreset preset, uint color)
        => this.ComboTints[preset] = color;

    /// <summary>
    /// Resets the hotbar icon tint for a combo back to its default (no tint, global method).
    /// </summary>
    /// <param name="preset">Preset to reset.</param>
    public void ResetComboTint(CustomComboPreset preset)
    {
        this.ComboTints.Remove(preset);
        this.ComboTintMethods.Remove(preset);
    }

    /// <summary>
    /// Gets the coloring method override for a combo, or null to use the global setting.
    /// </summary>
    /// <param name="preset">Preset to check.</param>
    /// <returns>The method override, or null.</returns>
    public IconColoringMethod? GetComboTintMethod(CustomComboPreset preset)
        => this.ComboTintMethods.TryGetValue(preset, out var method) ? method : null;

    /// <summary>
    /// Sets the coloring method override for a combo.
    /// </summary>
    /// <param name="preset">Preset to configure.</param>
    /// <param name="method">The method to use for this combo.</param>
    public void SetComboTintMethod(CustomComboPreset preset, IconColoringMethod method)
        => this.ComboTintMethods[preset] = method;

    /// <summary>
    /// Removes the coloring method override for a combo (back to the global setting).
    /// </summary>
    /// <param name="preset">Preset to reset.</param>
    public void ResetComboTintMethod(CustomComboPreset preset)
        => this.ComboTintMethods.Remove(preset);
}
