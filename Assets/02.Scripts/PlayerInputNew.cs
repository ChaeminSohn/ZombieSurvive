using UnityEngine;

public class PlayerInputNew : MonoBehaviour
{
    public string moveVInput = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string moveHInput = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름

    // 값 할당은 내부에서만 가능
    public float moveVertical { get; private set; }
    public float moveHorizontal { get; private set; } // 감지된 움직임 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값

    // 매프레임 사용자 입력을 감지
    private void Update()
    {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            moveVertical = 0;
            moveHorizontal = 0;
            fire = false;
            reload = false;
            return;
        }

        // move에 관한 입력 감지
        moveVertical = Input.GetAxis(moveVInput);
        // rotate에 관한 입력 감지
        moveHorizontal = Input.GetAxis(moveHInput);
        // fire에 관한 입력 감지
        fire = Input.GetButton(fireButtonName);
        // reload에 관한 입력 감지
        reload = Input.GetButtonDown(reloadButtonName);
    }
}
