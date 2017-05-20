using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemmings.Rendering
{
    public class Camera
    {
        #region Private Fields

        private static Camera mainCamera;
        private Color backgroundColor;
        private Rectangle bounds;
        private Vector2 position;
        private float rotation;
        private Matrix transformMatrix;
        private Viewport viewport;
        private Rectangle visbounds;
        private float zoom;

        #endregion Private Fields

        #region Public Constructors

        public Camera(GraphicsDeviceManager graphics = null)
        {
            Reset(graphics);
        }

        #endregion Public Constructors

        #region Public Properties

        public static Camera Main
        {
            get
            {
                if (mainCamera == null) mainCamera = new Camera(); return mainCamera;
            }
            set
            {
                mainCamera = value;
            }
        }

        /// <summary>
        /// The color that will be used as Clear-Color.
        /// </summary>
        public Color BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; } }

        public Rectangle Bounds
        {
            get
            {
                return visbounds;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; updateMatrix(); }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; updateMatrix(); }
        }

        /// <summary>
        /// The Transformation Matrix of the 2D-Camera
        /// </summary>
        public Matrix TransformMatrix
        {
            get
            {
                return transformMatrix;
            }
        }

        /// <summary>
        /// The Viewport in the real world
        /// </summary>
        public Viewport Viewport
        {
            get
            {
                return viewport;
            }
        }

        /// <summary>
        /// The current Zoomvalue
        /// </summary>
        public float Zoom { get { return zoom; } set { zoom = value; updateMatrix(); } }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Resets the camera to its default values.
        /// </summary>
        public void Reset(GraphicsDeviceManager graphics = null)
        {
            if (graphics == null)
                bounds = new Rectangle(0, 0, Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight);
            else
                bounds = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            zoom = 1;
            updateMatrix();
        }

        /// <summary>
        /// This method is used to get the Mouseposition on the world, translated by the camera. If
        /// the camera is moved, the position of the mouse in the world will change as well.
        /// </summary>
        /// <returns>A Vector2 for the translated MousePosition.</returns>
        public Vector2 TranslatedCursorPosition()
        {
            return Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(transformMatrix));
        }

        #endregion Public Methods

        #region Private Methods

        private void updateMatrix()
        {
            transformMatrix = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(zoom) *
                    Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0)
                    );

            viewport = new Viewport(
                        (int)(bounds.Width * -0.5f * (1f / zoom) - Position.X),
                        (int)(bounds.Height * -0.5f * (1f / zoom) - Position.Y),
                        (int)(bounds.Width * (1f / zoom)),
                        (int)(bounds.Height * (1f / zoom))
                        );

            visbounds = new Rectangle(
                    -(viewport.Bounds.X + viewport.Bounds.Width),
                    -(viewport.Bounds.Y + viewport.Bounds.Height),
                    viewport.Width,
                    viewport.Height
                    );
        }

        #endregion Private Methods
    }
}