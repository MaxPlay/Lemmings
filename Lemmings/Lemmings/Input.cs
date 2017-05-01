using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lemmings
{
    public enum GamePadButtons
    {
        A,
        B,
        X,
        Y,
        DPadUp,
        DPadLeft,
        DPadDown,
        DPadRight,
        Start,
        Back,
        Big,
        RTrigger,
        RShoulder,
        RStick,
        LTrigger,
        LShoulder,
        LStick,
    }

    [Flags]
    public enum InputType : byte
    {
        None = 0x0,
        Keyboard = 0x1,
        Mouse = 0x2,
        GamePad = 0x4
    }

    public enum MouseButton
    {
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }

    public static class Input
    {
        #region Private Fields

        private static GamePadCapabilities[] capabilites;

        private static GamePadState[] currentGamePadState, previousGamePadState;

        private static KeyboardState currentKeyboardState, previousKeyboardState;

        private static MouseState currentMouseState, previousMouseState;

        private static float[] deadZone;

        private static InputType inputUpdates;

        #endregion Private Fields

        #region Public Delegates

        public delegate void CursorChangedEventHandler(bool newValue);

        public delegate void GamePadEvent(PlayerIndex index, GamePadState state);

        #endregion Public Delegates

        #region Public Events

        public static event CursorChangedEventHandler ChangedCursorVisiblity;

        public static event GamePadEvent PlayerConnected;

        public static event GamePadEvent PlayerDisconnected;

        #endregion Public Events

        #region Public Properties

        public static InputType InputUpdates
        {
            get
            {
                return inputUpdates;
            }
            set
            {
                inputUpdates = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public static void ChangeCursorVisiblity(bool newValue)
        {
            ChangedCursorVisiblity?.Invoke(newValue);
        }

        public static bool GamePadDown(PlayerIndex index, GamePadButtons button)
        {
            switch (button)
            {
                case GamePadButtons.A:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.A);

                case GamePadButtons.B:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.B);

                case GamePadButtons.X:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.X);

                case GamePadButtons.Y:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.Y);

                case GamePadButtons.DPadUp:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadUp);

                case GamePadButtons.DPadLeft:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadLeft);

                case GamePadButtons.DPadDown:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadDown);

                case GamePadButtons.DPadRight:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadRight);

                case GamePadButtons.Start:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.Start);

                case GamePadButtons.Back:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.Back);

                case GamePadButtons.Big:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.BigButton);

                case GamePadButtons.RTrigger:
                    return currentGamePadState[(int)index].Triggers.Right >= deadZone[(int)index];

                case GamePadButtons.RShoulder:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.RightShoulder);

                case GamePadButtons.RStick:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.RightStick);

                case GamePadButtons.LTrigger:
                    return currentGamePadState[(int)index].Triggers.Left >= deadZone[(int)index];

                case GamePadButtons.LShoulder:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.LeftShoulder);

                case GamePadButtons.LStick:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.LeftStick);

                default:
                    return false;
            }
        }

        public static Vector2 GamePadLeftStick(PlayerIndex index)
        {
            return currentGamePadState[(int)index].ThumbSticks.Left.Length() < deadZone[(int)index] ? Vector2.Zero : currentGamePadState[(int)index].ThumbSticks.Left;
        }

        public static float GamePadLeftTrigger(PlayerIndex index)
        {
            return currentGamePadState[(int)index].Triggers.Left < deadZone[(int)index] ? 0 : currentGamePadState[(int)index].Triggers.Left;
        }

        public static bool GamePadPressed(PlayerIndex index, GamePadButtons button)
        {
            switch (button)
            {
                case GamePadButtons.A:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.A) && previousGamePadState[(int)index].IsButtonUp(Buttons.A);

                case GamePadButtons.B:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.B) && previousGamePadState[(int)index].IsButtonUp(Buttons.B);

                case GamePadButtons.X:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.X) && previousGamePadState[(int)index].IsButtonUp(Buttons.X);

                case GamePadButtons.Y:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.Y) && previousGamePadState[(int)index].IsButtonUp(Buttons.Y);

                case GamePadButtons.DPadUp:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadUp) && previousGamePadState[(int)index].IsButtonUp(Buttons.DPadUp);

                case GamePadButtons.DPadLeft:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadLeft) && previousGamePadState[(int)index].IsButtonUp(Buttons.DPadLeft);

                case GamePadButtons.DPadDown:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadDown) && previousGamePadState[(int)index].IsButtonUp(Buttons.DPadDown);

                case GamePadButtons.DPadRight:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.DPadRight) && previousGamePadState[(int)index].IsButtonUp(Buttons.DPadRight);

                case GamePadButtons.Start:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.Start) && previousGamePadState[(int)index].IsButtonUp(Buttons.Start);

                case GamePadButtons.Back:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.Back) && previousGamePadState[(int)index].IsButtonUp(Buttons.Back);

                case GamePadButtons.Big:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.BigButton) && previousGamePadState[(int)index].IsButtonUp(Buttons.BigButton);

                case GamePadButtons.RTrigger:
                    return currentGamePadState[(int)index].Triggers.Right >= deadZone[(int)index] && previousGamePadState[(int)index].Triggers.Right < deadZone[(int)index];

                case GamePadButtons.RShoulder:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.RightShoulder) && previousGamePadState[(int)index].IsButtonUp(Buttons.RightShoulder);

                case GamePadButtons.RStick:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.RightStick) && previousGamePadState[(int)index].IsButtonUp(Buttons.RightStick);

                case GamePadButtons.LTrigger:
                    return currentGamePadState[(int)index].Triggers.Left >= deadZone[(int)index] && previousGamePadState[(int)index].Triggers.Left < deadZone[(int)index];

                case GamePadButtons.LShoulder:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.LeftShoulder) && previousGamePadState[(int)index].IsButtonUp(Buttons.LeftShoulder);

                case GamePadButtons.LStick:
                    return currentGamePadState[(int)index].IsButtonDown(Buttons.LeftStick) && previousGamePadState[(int)index].IsButtonUp(Buttons.LeftStick);

                default:
                    return false;
            }
        }

        public static bool GamePadReleased(PlayerIndex index, GamePadButtons button)
        {
            switch (button)
            {
                case GamePadButtons.A:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.A) && previousGamePadState[(int)index].IsButtonDown(Buttons.A);

                case GamePadButtons.B:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.B) && previousGamePadState[(int)index].IsButtonDown(Buttons.B);

                case GamePadButtons.X:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.X) && previousGamePadState[(int)index].IsButtonDown(Buttons.X);

                case GamePadButtons.Y:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.Y) && previousGamePadState[(int)index].IsButtonDown(Buttons.Y);

                case GamePadButtons.DPadUp:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadUp) && previousGamePadState[(int)index].IsButtonDown(Buttons.DPadUp);

                case GamePadButtons.DPadLeft:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadLeft) && previousGamePadState[(int)index].IsButtonDown(Buttons.DPadLeft);

                case GamePadButtons.DPadDown:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadDown) && previousGamePadState[(int)index].IsButtonDown(Buttons.DPadDown);

                case GamePadButtons.DPadRight:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadRight) && previousGamePadState[(int)index].IsButtonDown(Buttons.DPadRight);

                case GamePadButtons.Start:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.Start) && previousGamePadState[(int)index].IsButtonDown(Buttons.Start);

                case GamePadButtons.Back:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.Back) && previousGamePadState[(int)index].IsButtonDown(Buttons.Back);

                case GamePadButtons.Big:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.BigButton) && previousGamePadState[(int)index].IsButtonDown(Buttons.BigButton);

                case GamePadButtons.RTrigger:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.RightTrigger) && previousGamePadState[(int)index].IsButtonDown(Buttons.RightTrigger);

                case GamePadButtons.RShoulder:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.RightShoulder) && previousGamePadState[(int)index].IsButtonDown(Buttons.RightShoulder);

                case GamePadButtons.RStick:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.RightStick) && previousGamePadState[(int)index].IsButtonDown(Buttons.RightStick);

                case GamePadButtons.LTrigger:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.LeftTrigger) && previousGamePadState[(int)index].IsButtonDown(Buttons.LeftTrigger);

                case GamePadButtons.LShoulder:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.LeftShoulder) && previousGamePadState[(int)index].IsButtonDown(Buttons.LeftShoulder);

                case GamePadButtons.LStick:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.LeftStick) && previousGamePadState[(int)index].IsButtonDown(Buttons.LeftStick);

                default:
                    return false;
            }
        }

        public static Vector2 GamePadRightStick(PlayerIndex index)
        {
            return currentGamePadState[(int)index].ThumbSticks.Right.Length() < deadZone[(int)index] ? Vector2.Zero : currentGamePadState[(int)index].ThumbSticks.Right;
        }

        public static float GamePadRightTrigger(PlayerIndex index)
        {
            return currentGamePadState[(int)index].Triggers.Right < deadZone[(int)index] ? 0 : currentGamePadState[(int)index].Triggers.Right;
        }

        public static bool GamePadUp(PlayerIndex index, GamePadButtons button)
        {
            switch (button)
            {
                case GamePadButtons.A:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.A);

                case GamePadButtons.B:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.B);

                case GamePadButtons.X:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.X);

                case GamePadButtons.Y:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.Y);

                case GamePadButtons.DPadUp:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadUp);

                case GamePadButtons.DPadLeft:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadLeft);

                case GamePadButtons.DPadDown:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadDown);

                case GamePadButtons.DPadRight:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.DPadRight);

                case GamePadButtons.Start:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.Start);

                case GamePadButtons.Back:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.Back);

                case GamePadButtons.Big:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.BigButton);

                case GamePadButtons.RTrigger:
                    return currentGamePadState[(int)index].Triggers.Right <= deadZone[(int)index];

                case GamePadButtons.RShoulder:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.RightShoulder);

                case GamePadButtons.RStick:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.RightStick);

                case GamePadButtons.LTrigger:
                    return currentGamePadState[(int)index].Triggers.Left <= deadZone[(int)index];

                case GamePadButtons.LShoulder:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.LeftShoulder);

                case GamePadButtons.LStick:
                    return currentGamePadState[(int)index].IsButtonUp(Buttons.LeftStick);

                default:
                    return false;
            }
        }

        public static GamePadCapabilities GetCapabilities(PlayerIndex index)
        {
            return capabilites[(int)index];
        }

        public static void Initialize()
        {
            currentGamePadState = new GamePadState[4];
            previousGamePadState = new GamePadState[4];
            capabilites = new GamePadCapabilities[4];
            deadZone = new float[] { 0.1f, 0.1f, 0.1f, 0.1f };
            inputUpdates = InputType.GamePad | InputType.Keyboard | InputType.Mouse;
        }

        public static bool IsPlayerConnected(PlayerIndex index)
        {
            return currentGamePadState[(int)index].IsConnected;
        }

        public static bool KeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }

        public static bool KeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        public static bool MouseDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Pressed;

                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Pressed;

                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Pressed;

                case MouseButton.XButton1:
                    return currentMouseState.XButton1 == ButtonState.Pressed;

                case MouseButton.XButton2:
                    return currentMouseState.XButton2 == ButtonState.Pressed;

                default:
                    return false;
            }
        }

        public static Point MousePosition()
        {
            if ((inputUpdates & InputType.Mouse) == InputType.None)
                return new Point(-1, -1);
            return new Point(currentMouseState.X, currentMouseState.Y);
        }

        public static bool MousePressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;

                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;

                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released;

                case MouseButton.XButton1:
                    return currentMouseState.XButton1 == ButtonState.Pressed && previousMouseState.XButton1 == ButtonState.Released;

                case MouseButton.XButton2:
                    return currentMouseState.XButton2 == ButtonState.Pressed && previousMouseState.XButton2 == ButtonState.Released;

                default:
                    return false;
            }
        }

        public static bool MouseReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed;

                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed;

                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Released && previousMouseState.MiddleButton == ButtonState.Pressed;

                case MouseButton.XButton1:
                    return currentMouseState.XButton1 == ButtonState.Released && previousMouseState.XButton1 == ButtonState.Pressed;

                case MouseButton.XButton2:
                    return currentMouseState.XButton2 == ButtonState.Released && previousMouseState.XButton2 == ButtonState.Pressed;

                default:
                    return false;
            }
        }

        public static bool MouseUp(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Released;

                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Released;

                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Released;

                case MouseButton.XButton1:
                    return currentMouseState.XButton1 == ButtonState.Released;

                case MouseButton.XButton2:
                    return currentMouseState.XButton2 == ButtonState.Released;

                default:
                    return false;
            }
        }

        public static void Update()
        {
            if ((inputUpdates & InputType.GamePad) == InputType.GamePad)
                for (int i = 0; i < 4; i++)
                {
                    previousGamePadState[i] = currentGamePadState[i];
                    currentGamePadState[i] = GamePad.GetState((PlayerIndex)i);
                    if (previousGamePadState[i].IsConnected != currentGamePadState[i].IsConnected)
                    {
                        if (currentGamePadState[i].IsConnected)
                            OnPlayerConnected((PlayerIndex)i);
                        else
                            OnPlayerDisconnected((PlayerIndex)i);
                    }
                }

            if ((inputUpdates & InputType.Keyboard) == InputType.Keyboard)
            {
                previousKeyboardState = currentKeyboardState;
                currentKeyboardState = Keyboard.GetState();
            }

            if ((inputUpdates & InputType.Mouse) == InputType.Mouse)
            {
                previousMouseState = currentMouseState;
                currentMouseState = Mouse.GetState();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void OnPlayerConnected(PlayerIndex index)
        {
            PlayerConnected?.Invoke(index, currentGamePadState[(int)index]);
        }

        private static void OnPlayerDisconnected(PlayerIndex index)
        {
            PlayerDisconnected?.Invoke(index, currentGamePadState[(int)index]);
        }

        #endregion Private Methods
    }
}