using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

/// <summary>
/// Manager of all User Input including Key combinations
/// </summary>
namespace mercenary_guild.input
{
    public class GameInput : MonoBehaviour
    {
        private const string PLAYER_PREFS_BINDINGS = "InputBindings";

        public static GameInput Instance;

        public event EventHandler OnInteractAction;//E
        public event EventHandler OnInteractSecondAction; //Q
        public event EventHandler OnPauseAction; // esc
        public event EventHandler OnPrimaryClickActionStart;//left click
        public event EventHandler OnPrimaryClickActionCancel;
        public event EventHandler OnSecondaryClickActionStart;//right click
        public event EventHandler OnSecondaryClickActionCancel;

        public event EventHandler OnAlternativeActionStart;//middle mouse
        public event EventHandler OnALternativeActionCancel;

        public event EventHandler OnPrimaryAlternativeAction; // middle + left
        public event EventHandler OnSecondaryAlternativeAction; // middle + right
        public event EventHandler OnInteractAlternativeAction;
        public event EventHandler OnInteractSecondAlternativeAction;
        public event Action<Vector2> MoveChanged;

        public enum Binding
        {
            Move_up,
            Move_down,
            Move_left,
            Move_right,
            Interact,
            Pause,
            InteractSecond,
            primary,
            secondary,
            Alternative,
        }
        private PlayerInputActions playerActions;

        bool alternativeButtonHold = false;

        private bool StopCameraMovement = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            this.SetStopCameraMovement(true);

            playerActions = new PlayerInputActions();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                playerActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }

            playerActions.Player.Enable();

            playerActions.Player.Interact.performed += Interact_performed;
            playerActions.Player.InteractSecond.performed += InteractSecond_performed;
            playerActions.Player.Pause.performed += Pause_performed;
            playerActions.Player.Primary.performed += Primary_performed;
            playerActions.Player.Primary.canceled += Primary_canceled;
            playerActions.Player.Secondary.performed += Secondary_performed;
            playerActions.Player.Secondary.canceled += Secondary_canceled;

            playerActions.Player.Alternative.performed += Alternative_performed;
            playerActions.Player.Alternative.canceled += Alternative_canceled;
            playerActions.Player.movement.performed += Movement_performed;
            playerActions.Player.movement.canceled += Movement_canceled;
        }

        private void Movement_canceled(InputAction.CallbackContext obj)
        {
            MoveChanged.Invoke(GetMovementVectorNormalized());
        }

        private void Movement_performed(InputAction.CallbackContext obj)
        {
            MoveChanged.Invoke(GetMovementVectorNormalized());
        }

        private void OnDestroy()
        {
            playerActions.Player.Interact.performed -= Interact_performed;
            playerActions.Player.InteractSecond.performed -= InteractSecond_performed;
            playerActions.Player.Pause.performed -= Pause_performed;
            playerActions.Player.Primary.performed -= Primary_performed;
            playerActions.Player.Primary.canceled -= Primary_canceled;
            playerActions.Player.Secondary.performed -= Secondary_performed;
            playerActions.Player.Secondary.canceled -= Secondary_canceled;
            playerActions.Player.Alternative.performed -= Alternative_performed;
            playerActions.Player.Alternative.canceled -= Alternative_canceled;
            playerActions.Player.movement.performed -= Movement_performed;

            playerActions.Dispose();
        }

        private void Alternative_canceled(InputAction.CallbackContext obj)
        {
            alternativeButtonHold = false;
        }

        private void Alternative_performed(InputAction.CallbackContext obj)
        {
            alternativeButtonHold = true;
        }

        private void Primary_performed(InputAction.CallbackContext obj)
        {
            if (alternativeButtonHold)
            {
                OnPrimaryAlternativeAction?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPrimaryClickActionStart?.Invoke(this, EventArgs.Empty);
            }

        }
        private void Primary_canceled(InputAction.CallbackContext obj)
        {
            OnPrimaryClickActionCancel?.Invoke(this, EventArgs.Empty);
        }

        private void Secondary_performed(InputAction.CallbackContext obj)
        {
            if (alternativeButtonHold)
            {
                OnSecondaryAlternativeAction?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnSecondaryClickActionStart?.Invoke(this, EventArgs.Empty);
            }

        }
        private void Secondary_canceled(InputAction.CallbackContext obj)
        {
            OnSecondaryClickActionCancel?.Invoke(this, EventArgs.Empty);
        }

        private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (alternativeButtonHold)
            {
                OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnInteractAction?.Invoke(this, EventArgs.Empty);
            }
        }
        private void InteractSecond_performed(InputAction.CallbackContext obj)
        {
            if (alternativeButtonHold)
            {
                OnInteractSecondAlternativeAction?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnInteractSecondAction?.Invoke(this, EventArgs.Empty);
            }
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = playerActions.Player.movement.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }

        public Vector2 GetMouseDeltaMovement()
        {
            if (StopCameraMovement) return Vector2.zero;
            return playerActions.Player.Mouse.ReadValue<Vector2>();
        }


        public void SetStopCameraMovement(bool set)
        {
            StopCameraMovement = set;
            if (set)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        public string GetBindingText(Binding binding)
        {
            switch (binding)
            {
                default:
                case Binding.Move_up:
                    return playerActions.Player.movement.bindings[1].ToDisplayString();
                case Binding.Move_down:
                    return playerActions.Player.movement.bindings[2].ToDisplayString();
                case Binding.Move_left:
                    return playerActions.Player.movement.bindings[3].ToDisplayString();
                case Binding.Move_right:
                    return playerActions.Player.movement.bindings[4].ToDisplayString();
                case Binding.Interact:
                    return playerActions.Player.Interact.bindings[0].ToDisplayString();
                case Binding.Pause:
                    return playerActions.Player.Pause.bindings[0].ToDisplayString();

            }
        }

        public void RebindBinding(Binding binding, Action onActionRebound)
        {
            //playerActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                default:
                case Binding.Move_up:
                    inputAction = playerActions.Player.movement;
                    bindingIndex = 1;
                    break;
                case Binding.Move_down:
                    inputAction = playerActions.Player.movement;
                    bindingIndex = 2;
                    break;
                case Binding.Move_left:
                    inputAction = playerActions.Player.movement;
                    bindingIndex = 3;
                    break;
                case Binding.Move_right:
                    inputAction = playerActions.Player.movement;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = playerActions.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = playerActions.Player.Pause;
                    bindingIndex = 0;
                    break;

            }

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    playerActions.Player.Enable();
                    onActionRebound();

                    playerActions.SaveBindingOverridesAsJson();
                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                })
            .Start();
        }
    }

}