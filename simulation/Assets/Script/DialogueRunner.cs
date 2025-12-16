using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    [Header("Start Node")]
    [SerializeField] private DialogueNode startNode; // 인스펙터에서 첫 대사를 넣어줄 곳

    private DialogueNode _currentNode;
    private bool _isDialogueActive = false;

    private void Start()
    {
        // 게임 시작 시 바로 대화 시작
        if (startNode != null)
        {
            StartDialogue(startNode);
        }
        else
        {
            Debug.LogWarning("시작 노드가 설정되지 않았습니다!");
        }
    }

    private void Update()
    {
        // 대화 중일 때 스페이스바를 누르면 다음 대사로 진행
        if (_isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            ProceedToNext();
        }
    }

    // 대화 시스템 시작
    public void StartDialogue(DialogueNode node)
    {
        _currentNode = node;
        _isDialogueActive = true;
        DisplayNode(_currentNode);
    }

    // 현재 노드의 내용을 출력 (나중에 UI 연결할 부분)
    private void DisplayNode(DialogueNode node)
    {
        Debug.Log($"-----------------------------------------");
        Debug.Log($"[화자]: {node.speakerName}");
        Debug.Log($"[내용]: {node.dialogueText}");
        Debug.Log($"-----------------------------------------");
        Debug.Log(">> 스페이스바를 눌러 계속하기...");
    }

    // 다음 노드로 넘어가기
    private void ProceedToNext()
    {
        // 다음 노드가 있는지 확인
        if (_currentNode.nextNode != null)
        {
            _currentNode = _currentNode.nextNode;
            DisplayNode(_currentNode);
        }
        else
        {
            EndDialogue();
        }
    }

    // 대화 종료 처리
    private void EndDialogue()
    {
        _isDialogueActive = false;
        Debug.Log("<< 대화가 종료되었습니다. >>");
    }
}