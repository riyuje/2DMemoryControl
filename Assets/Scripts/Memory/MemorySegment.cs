using System;

[Serializable]
public class MemorySegment
{
    public int id;                  // 節番号
    public string originalText;     // 元の記憶
    public string hackedText;       // 改竄後の記憶
    public bool shouldHack;         // 本来ここは改竄すべきか
    public bool isHacked;           // 実際に改竄したか
    public bool isProcessed;        // この節の処理を終えたか

    public string GetCurrentText()
    {
        return isHacked ? hackedText : originalText;
    }

    public bool IsCorrectChoice()
    {
        return (shouldHack && isHacked) || (!shouldHack && !isHacked);
    }
}