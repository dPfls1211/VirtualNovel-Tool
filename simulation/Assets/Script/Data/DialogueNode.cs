using UnityEngine;
using System.Collections.Generic;

// 선택지 하나하나의 정보를 담는 클래스
[System.Serializable]
public class ChoiceOption
{
    public string text;           // 선택지 텍스트 (예: "도와준다")
    public DialogueNode targetNode; // 선택했을 때 이동할 노드
}

[CreateAssetMenu(fileName = "NewNode", menuName = "VN System/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [Header("Node Info")]
    public string nodeId;

    [Header("Dialogue Content")]
    public string speakerName;
    [TextArea(3, 5)]
    public string dialogueText;

    [Header("Branching")]
    // 선택지가 있으면 이 리스트를 사용
    // 데이터 기반(Data-Driven) 구조로 짜면, 기획자가 코드 수정 없이 에디터에서 선택지를 4개, 5개로 늘려도 로직은 완벽하게 작동함
    public List<ChoiceOption> choices = new List<ChoiceOption>();

    // 선택지가 없을 때(선형 진행) 사용할 기본 다음 노드
    public DialogueNode nextNode;

    // 헬퍼 프로퍼티: 선택지가 있는지 여부 확인
    public bool HasChoices => choices != null && choices.Count > 0;
}