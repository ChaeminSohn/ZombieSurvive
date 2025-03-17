using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도

    private Camera mainCam;


    private PlayerInputNew playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInputNew>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        LookAtMouseCursor();
        Move();

        playerAnimator.SetFloat("MoveV", playerInput.moveVertical);
        playerAnimator.SetFloat("MoveH", playerInput.moveHorizontal);
    }


    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        //카메라가 바라보는 기준(게임 뷰 기준)으로 캐릭터 움직임
        Vector3 heading = mainCam.transform.localRotation * Vector3.forward;
        heading.y = 0;
        Vector3 directionForward = heading.normalized;
        Vector3 directioRight = Quaternion.Euler(0, 90, 0) * directionForward;


        Vector3 moveDistance =
            ((playerInput.moveVertical * directionForward)
                + (playerInput.moveHorizontal * directioRight)).normalized
                * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);

    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate()
    {
        //float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;

        //playerRigidbody.rotation *= Quaternion.Euler(0, turn, 0);


    }

    public void LookAtMouseCursor()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mouseDir = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(mouseDir);
        }
    }
}