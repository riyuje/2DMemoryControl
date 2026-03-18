using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryHackManager : MonoBehaviour
{
    public static MemoryHackManager Instance { get; private set; }

    [Header("初期設定")]
    [SerializeField] private bool setupSampleOnAwake = true;
    [SerializeField] private int defaultHackCount = 2;

    [Header("現在のケース")]
    [SerializeField] private EmployeeMemoryCase currentCase = new EmployeeMemoryCase();

    [Header("現在見ている節ID")]
    [SerializeField] private int currentSegmentId = 1;

    [Header("結果")]
    [SerializeField] private int score = 0;
    [SerializeField] private string rank = "C";

    public int Score => score;
    public string Rank => rank;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (setupSampleOnAwake)
        {
            SetupSampleCase();
        }
    }

    [ContextMenu("Setup Sample Case")]
    public void SetupSampleCase()
    {
        currentCase = new EmployeeMemoryCase
        {
            employeeId = "employeeA",
            remainingHackCount = defaultHackCount,
            segments = new List<MemorySegment>
            {
                new MemorySegment
                {
                    id = 1,
                    originalText = "新人が書類を投げてきたんです。",
                    hackedText = "新人が急いでいて、書類を落としてしまったんです。",
                    shouldHack = true
                },
                new MemorySegment
                {
                    id = 2,
                    originalText = "その書類が額に当たって怪我をしました。",
                    hackedText = "棚にぶつかっただけで、大した怪我ではありませんでした。",
                    shouldHack = true
                },
                new MemorySegment
                {
                    id = 3,
                    originalText = "でも事故として処理されました。",
                    hackedText = "でも誰かが故意に隠したんです。",
                    shouldHack = false
                }
            }
        };

        currentSegmentId = 1;
        score = 0;
        rank = "C";
    }

    public void ResetCase()
    {
        currentCase.remainingHackCount = defaultHackCount;

        foreach (var segment in currentCase.segments)
        {
            segment.isHacked = false;
            segment.isProcessed = false;
        }

        currentSegmentId = 1;
        score = 0;
        rank = "C";
    }

    public void SetCurrentSegment(int id)
    {
        currentSegmentId = id;
    }

    public MemorySegment GetCurrentSegment()
    {
        return currentCase.segments.FirstOrDefault(s => s.id == currentSegmentId);
    }

    public bool CanHack()
    {
        return currentCase.remainingHackCount > 0;
    }

    public int GetRemainingHackCount()
    {
        return currentCase.remainingHackCount;
    }

    public string GetCurrentText()
    {
        var segment = GetCurrentSegment();
        if (segment == null) return "";
        return segment.GetCurrentText();
    }

    public void HackCurrentSegment()
    {
        var segment = GetCurrentSegment();
        if (segment == null) return;
        if (segment.isProcessed) return;
        if (!CanHack()) return;

        segment.isHacked = true;
        segment.isProcessed = true;
        currentCase.remainingHackCount--;

        Debug.Log($"節{segment.id}を改竄。残り回数: {currentCase.remainingHackCount}");
    }

    public void KeepCurrentSegment()
    {
        var segment = GetCurrentSegment();
        if (segment == null) return;
        if (segment.isProcessed) return;

        segment.isHacked = false;
        segment.isProcessed = true;

        Debug.Log($"節{segment.id}はそのまま");
    }

    public void EvaluateResult()
    {
        score = 0;

        foreach (var segment in currentCase.segments)
        {
            if (segment.IsCorrectChoice())
            {
                score += 10;
            }
            else
            {
                score -= 5;
            }
        }

        if (score >= 25) rank = "S";
        else if (score >= 15) rank = "A";
        else if (score >= 5) rank = "B";
        else rank = "C";

        Debug.Log($"score={score}, rank={rank}");
    }

    public string GetResultSummary()
    {
        int correctCount = currentCase.segments.Count(s => s.IsCorrectChoice());
        int wrongCount = currentCase.segments.Count - correctCount;
        return $"正解数: {correctCount} / 不正解数: {wrongCount} / スコア: {score} / ランク: {rank}";
    }
}