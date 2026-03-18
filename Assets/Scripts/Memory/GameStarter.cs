using UnityEngine;
using Yarn.Unity;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private bool startOnPlay = true;
    [SerializeField] private string startNodeName = "Start";

    private void Start()
    {
        if (!startOnPlay) return;
        StartDialogue();
    }

    [ContextMenu("Start Dialogue")]
    public void StartDialogue()
    {
        if (dialogueRunner == null) return;
        if (dialogueRunner.IsDialogueRunning) return;

        dialogueRunner.StartDialogue(startNodeName);
    }
}