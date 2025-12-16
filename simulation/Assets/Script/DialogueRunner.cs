using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    [SerializeField] private DialogueNode startNode;

    private DialogueNode _currentNode;
    private bool _isDialogueActive = false;
    private bool _isWaitingForChoice = false; // 선택지 입력 대기 상태 플래그

    private void Start()
    {
        if (startNode != null) StartDialogue(startNode);
    }

    private void Update()
    {
        if (!_isDialogueActive) return;

        // 1. 선택지 대기 상태일 때
        if (_isWaitingForChoice)
        {
            HandleChoiceInput();
        }
        // 2. 일반 대화 상태일 때 (스페이스바로 다음)
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ProceedToNext();
            }
        }
    }

    // 숫자 키 입력 처리 (1~9번)
    private void HandleChoiceInput()
    {
        // 1번부터 선택지 개수만큼 키 입력 체크
        for (int i = 0; i < _currentNode.choices.Count; i++)
        {
            // Alpha1은 숫자 1키, Alpha2는 숫자 2키...
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // 선택한 노드로 이동
                DialogueNode selectedNode = _currentNode.choices[i].targetNode;
                _isWaitingForChoice = false; // 대기 상태 해제

                // 선택 결과 로그
                Debug.Log($"---> [{i + 1}번 선택함]: 다음 대사로 이동");

                StartDialogue(selectedNode);
                return;
            }
        }
    }

    public void StartDialogue(DialogueNode node)
    {
        _currentNode = node;
        _isDialogueActive = true;
        DisplayNode(node);
    }

    private void DisplayNode(DialogueNode node)
    {
        Debug.Log($"-----------------------------------------");
        Debug.Log($"[화자]: {node.speakerName}");
        Debug.Log($"[내용]: {node.dialogueText}");

        // 선택지가 있는지 확인
        if (node.HasChoices)
        {
            _isWaitingForChoice = true; // 유저 입력 대기 모드 진입
            Debug.Log("[선택지 발생!] 숫자 키를 눌러 선택하세요:");

            for (int i = 0; i < node.choices.Count; i++)
            {
                Debug.Log($"   ({i + 1}) {node.choices[i].text}");
            }
        }
        else
        {
            _isWaitingForChoice = false;
            Debug.Log(">> 스페이스바를 눌러 계속하기...");
        }
        Debug.Log($"-----------------------------------------");
    }

    private void ProceedToNext()
    {
        // 선택지가 없는 노드라면 nextNode로 이동
        if (_currentNode.nextNode != null)
        {
            StartDialogue(_currentNode.nextNode);
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        _isDialogueActive = false;
        Debug.Log("<< 대화가 종료되었습니다. >>");
    }
}