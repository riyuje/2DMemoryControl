using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Attributes;

public class MemoryHackYarnBridge : MonoBehaviour
{
    [SerializeField] private MemoryHackManager memoryHackManager;

    private MemoryHackManager Manager
    {
        get
        {
            if (memoryHackManager == null)
            {
                memoryHackManager = MemoryHackManager.Instance;
            }

            return memoryHackManager;
        }
    }

    [YarnCommand("set_segment")]
    public void SetSegment(string idText)
    {
        if (int.TryParse(idText, out int id))
        {
            Manager.SetCurrentSegment(id);
        }
    }

    [YarnCommand("hack_current")]
    public void HackCurrent()
    {
        Manager.HackCurrentSegment();
    }

    [YarnCommand("keep_current")]
    public void KeepCurrent()
    {
        Manager.KeepCurrentSegment();
    }

    [YarnCommand("evaluate_case")]
    public void EvaluateCase()
    {
        Manager.EvaluateResult();
    }

    [YarnCommand("reset_case")]
    public void ResetCase()
    {
        Manager.ResetCase();
    }

    [YarnFunction("can_hack")]
    public static bool CanHack()
    {
        return MemoryHackManager.Instance != null && MemoryHackManager.Instance.CanHack();
    }

    [YarnFunction("remaining_hacks")]
    public static int RemainingHacks()
    {
        return MemoryHackManager.Instance != null ? MemoryHackManager.Instance.GetRemainingHackCount() : 0;
    }

    [YarnFunction("current_text")]
    public static string CurrentText()
    {
        return MemoryHackManager.Instance != null ? MemoryHackManager.Instance.GetCurrentText() : "";
    }

    [YarnFunction("rank")]
    public static string Rank()
    {
        return MemoryHackManager.Instance != null ? MemoryHackManager.Instance.Rank : "C";
    }

    [YarnFunction("result_summary")]
    public static string ResultSummary()
    {
        return MemoryHackManager.Instance != null ? MemoryHackManager.Instance.GetResultSummary() : "";
    }
}