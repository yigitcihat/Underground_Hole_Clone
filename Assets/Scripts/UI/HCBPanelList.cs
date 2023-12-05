using System.Collections.Generic;
using System.Linq;


public static class HcbPanelList
{
    public static string GameStartPanel = "GameStartPanel";
    public static string InGamePanel = "InGamePanel";
    public static string TutorialPanel = "TutorialPanel";
    public static string CharacterUpgradePanel = "CharacterUpgradePanel";
    public static string HoleUpgradePanel = "HoleUpgradePanel";
    public static string LevelLoadingPanel = "LevelLoadingPanel";

    public static Dictionary<string, HcbPanel> HcbPanels = new Dictionary<string, HcbPanel>();

    private static string[] _panelIDs = new string[]
    {
        "None",
        GameStartPanel,
        InGamePanel,
        TutorialPanel,
        CharacterUpgradePanel,
        HoleUpgradePanel,
        LevelLoadingPanel,
    };

    public static List<string> PanelIDs
    {
        get { return _panelIDs.ToList(); }
    }
}