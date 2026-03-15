using System.Collections;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    private enum SequenceActionType
    {
        DialogueBlock,
        MoveToTarget,
        Wait,
        FaceDirection,
        FaceTarget,
        FacePlayer,
        PlayAnimation
    }

    [System.Serializable]
    private class SequenceAction
    {   
        [Header("Action data")]
        public SequenceActionType actionType;

        public int dialogueBlockIndex;

        public Transform target;

        public float waitDuration;

        public NPCController.Direction direction;

        public string animationStateName;
        public float animationWaitDuration;
        public bool returnToIdleAfterAnimation = true;
    }

    [Header("Dependencies")]
    [SerializeField] private string npcID;
    [SerializeField] private NPCDialogue dialogue;
    [SerializeField] private NPCController controller;

    [Header("Sequences")]
    [SerializeField] private SequenceAction[] firstInteractionSequence;
    [SerializeField] private SequenceAction[] repeatInteractionSequence;

    [Header("Optionals")]
    [SerializeField] private bool lockPlayerDuringInteraction = true;
    [SerializeField] private bool facePlayerAtStart = true;
    [SerializeField] private GameObject objectToDisableAfterFirstInteraction;
    [SerializeField] private string completeTaskIDAfterFirstInteraction;

    private bool isRunning;
    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Interact()
    {
        if (isRunning)
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        isRunning = true;

        bool hasTalkedBefore = ProgressionManager.Instance.HasTalkedToNpc(npcID);
        SequenceAction[] sequenceToRun = hasTalkedBefore ? repeatInteractionSequence : firstInteractionSequence;

        if (lockPlayerDuringInteraction)
        {
            GameStateManager.SetState(GameState.Cutscene);
        }

        if (facePlayerAtStart && player != null && controller != null)
        {
            controller.LookAtTarget(player.transform);
        }

        for (int i = 0; i < sequenceToRun.Length; i++)
        {
            yield return RunAction(sequenceToRun[i]);
        }

        if (!hasTalkedBefore)
        {
            ProgressionManager.Instance.RegisterNpcTalk(npcID);

            if (!string.IsNullOrWhiteSpace(completeTaskIDAfterFirstInteraction))
            {
                TaskManager.Instance.CompleteTask(completeTaskIDAfterFirstInteraction);
            }

            if (objectToDisableAfterFirstInteraction != null)
            {
                objectToDisableAfterFirstInteraction.SetActive(false);
            }
        }

        if (lockPlayerDuringInteraction)
        {
            GameStateManager.SetState(GameState.Gameplay);
        }

        isRunning = false;
    }

    private IEnumerator RunAction(SequenceAction action)
    {
        switch (action.actionType)
        {
            case SequenceActionType.DialogueBlock:
                if (dialogue != null)
                {
                    yield return dialogue.PlayDialogueBlock(action.dialogueBlockIndex);
                }
                break;

            case SequenceActionType.MoveToTarget:
                if (controller != null && action.target != null)
                {
                    yield return controller.WalkTo(action.target);
                }
                break;

            case SequenceActionType.Wait:
                if (action.waitDuration > 0f)
                {
                    yield return new WaitForSecondsRealtime(action.waitDuration);
                }
                break;

            case SequenceActionType.FaceDirection:
                if (controller != null)
                {
                    controller.FaceDirection(action.direction);
                }
                break;

            case SequenceActionType.FaceTarget:
                if (controller != null && action.target != null)
                {
                    controller.LookAtTarget(action.target);
                }
                break;

            case SequenceActionType.FacePlayer:
                if (controller != null && player != null)
                {
                    controller.LookAtTarget(player.transform);
                }
                break;

            case SequenceActionType.PlayAnimation:
                if (controller != null && !string.IsNullOrWhiteSpace(action.animationStateName))
                {
                    controller.PlayAnimationState(action.animationStateName);

                    if (action.animationWaitDuration > 0f)
                    {
                        yield return new WaitForSecondsRealtime(action.animationWaitDuration);
                    }

                    if (action.returnToIdleAfterAnimation)
                    {
                        controller.ResetToIdle();
                    }
                }
                break;
        }
    }
}