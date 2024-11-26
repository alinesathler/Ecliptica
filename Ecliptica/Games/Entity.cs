using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
    public abstract class Entity
    {
		#region Fields
		protected Texture2D image;
        protected Color color = Color.White;

        private Vector2 _velocity;
		private Vector2 _position;
		private Rectangle _boundingBox;

		private int _life;
		private int _maxLife;

		private float _scale = 1.0f;

		private SoundEffect _soundPicked;

		public float Radius = 20;
		public float Orientation;
		public bool IsActive = true;
		public bool IsExpired = false;
		#endregion

		#region Properties
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

		public Rectangle BoundingBox
		{
			get
			{
				return _boundingBox;
			}
		}

		public int Life
        {
			get { return _life; }
			set { _life = value; }
		}

        public int MaxLife
		{
			get { return _maxLife; }
			set { _maxLife = value; }
		}

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

		public SoundEffect SoundPicked
		{
			get { return _soundPicked; }
			set { _soundPicked = value; }
		}

		public Vector2 Size
		{
			get
			{
				return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to calculate the bounding box of the entity to use in the collision detection
		/// </summary>
		protected void CalculateBoundingBox()
        {
            _boundingBox = new Rectangle(
		        (int)(_position.X - image.Width / 2f),
		        (int)(_position.Y - image.Height / 2f),
				image.Width,
				image.Height
			);
		}

		/// <summary>
		/// Method to play the sound when the entity is picked
		/// </summary>
		public void PlaySoundPicked()
		{
            if (SoundPicked != null)
			{
				Sounds.PlaySound(SoundPicked, 1.0f);
			}
		}

		/// <summary>
		/// Method to update the entity
		/// </summary>
		/// <param name="gametime"></param>
		public abstract void Update(GameTime gametime);

		/// <summary>
		/// Method to draw the entity
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, Orientation, Size / 2f, Scale, 0, 0);
        }
		#endregion
	}
}
