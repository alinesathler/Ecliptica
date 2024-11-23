using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
    public abstract class Entity
    {
        protected Texture2D image;
        protected Color color = Color.White;

        public Vector2 _velocity;
        protected Vector2 _position;

        public int life;

		public Vector2 Velocity
		{
			get { return _velocity; }
			set { _velocity = value; }
		}

		public Vector2 Position
        {
            get { return _position; }
            set { _position = value;
                CalculateBoundingBox();
            }
        }

        public int Life
        {
			get { return life; }
			set { life = value; }
		}

		protected float _scale = 1f;

		protected float _speed = 0.3f;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float Orientation;
        public float Radius = 20; //used for circular collision detection
        public bool IsExpired = false; //true if the entity was destroyed and should be deleted

        public bool IsActive = true;

        public Vector2 Size
        {
            get
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
        }

        protected Rectangle _boundingBox;

        public Rectangle BoundingBox
        {
            get
            {
                return _boundingBox;
            }
        }

        protected void CalculateBoundingBox()
        {
            _boundingBox = new Rectangle(
		        (int)(_position.X - image.Width / 2f),
		        (int)(_position.Y - image.Height / 2f),
				image.Width,
				image.Height
			);
		}

        public abstract void Update(GameTime gametime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }
    }
}
