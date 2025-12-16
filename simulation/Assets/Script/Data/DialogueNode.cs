using UnityEngine;

// 파일 생성 메뉴에 추가
[CreateAssetMenu(fileName = "NewNode", menuName = "VN System/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [Header("Node Info")]
    public string nodeId; // 나중에 Firebase 저장용 고유 ID (예: "node_001")

    [Header("Dialogue Content")]
    public string speakerName; // 화자 이름
    [TextArea(3, 5)]
    public string dialogueText; // 대사 내용

    [Header("Connections")]
    // 다음으로 이어질 노드. 
    // 나중에는 이 직접 참조 대신 'ID'를 통해 연결하는 방식으로 고도화할 예정입니다.
    public DialogueNode nextNode;

    // 이번 테스트에서는 선택지 없이 '선형 대화'부터 확인합니다.
}