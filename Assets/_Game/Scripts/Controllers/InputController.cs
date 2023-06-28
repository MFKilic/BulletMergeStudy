using UnityEngine;
using System.Collections;


namespace TemplateFx.Controller
{
    public class InputController : MonoBehaviour
    {
        public delegate void LevelManagingActions();
        public static event LevelManagingActions GameStarted;

        public delegate void TouchStartActions();
        public static event TouchStartActions OnTouchStarted;
        public delegate void InputActions(float horizontalInput, float verticalInput);
        public static event InputActions OnInputChanged;

        public delegate void TouchActions();
        public static event TouchActions OnTouchFinished;


        private Vector2 joystickDirection;
        private float joystickMaxMove = 500f; 
        private Vector2 startingJoystickPos;



        private bool isGameEnded;
        private bool isGameStarted;


        private bool isTouchStarted;
    

        private float plusYPos;
        private float verticalInput;
        private float horizontalInput;
        private bool isLeftStarted;
        private int halfOfScreenWidth;


        private void Start()
        {
         
            joystickMaxMove = joystickMaxMove.Remap(0, 1080, 0, Screen.width);
            halfOfScreenWidth = Mathf.FloorToInt(Screen.width / 2f);
        }



        void Update()
        {
            if ((Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)))
            {
                if (Input.GetMouseButtonDown(0))
                    TouchStarted();

                if (!isGameEnded & isGameStarted) Touching();

                if (Input.GetMouseButtonUp(0))
                    TouchFinished();
            }
            if ((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) && Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    TouchStarted();

                if (!isGameEnded & isGameStarted) Touching();

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    TouchFinished();
            }
        }



        private void TouchStarted()
        {
            if (!isGameEnded)
            {
                isTouchStarted = true;
                if (!isGameStarted)
                {
                    isGameStarted = true;
                    GameStarted?.Invoke();
                }
                startingJoystickPos = Input.mousePosition;
                isLeftStarted = Input.mousePosition.x < halfOfScreenWidth;
                OnTouchStarted?.Invoke();
                GetInputAccordingToJoystick();


            }

        }

        private void Touching()
        {
            GetInputAccordingToJoystick();
        }

        private void GetInputAccordingToJoystick()
        {

            Vector2 currentJoystickPos = Input.mousePosition;
            Vector2 offset = (currentJoystickPos - startingJoystickPos) / joystickMaxMove;



            OnInputChanged?.Invoke(offset.x, offset.y);

            startingJoystickPos = Vector2.Lerp(startingJoystickPos, Input.mousePosition, Time.deltaTime * 5);



        }



        private void TouchFinished()
        {

            plusYPos = 0;
            isTouchStarted = false;
            OnInputChanged?.Invoke(0, verticalInput);
            OnTouchFinished?.Invoke();
        }


    }
}


