using SFML.Graphics;
using SFML.System;

namespace PinPong
{
    public class GameObject
    {
        private Texture _texture;
        private Sprite _sprite;
        private Vector2f _velocity;
        public Sprite GetSprite() => _sprite;
        public GameObject(Vector2f spawnPosition,Vector2f scale,string textureName)
        {
            _texture = new Texture(textureName);
            _sprite = new Sprite(_texture);

            _sprite.Position = spawnPosition;
            _sprite.Scale = scale;
        }
        public void SetVelocity(Vector2f velocity)
        {
            _velocity = velocity;
        }
        public void Move()
        {
            _sprite.Position += _velocity;
        }
        public void Reflect(bool isHorizontalWall, bool isWerticalWall)
        {
            if (isWerticalWall)
            {
                _velocity.X = -_velocity.X;
            }
            if (isHorizontalWall)
            {
                _velocity.Y = -_velocity.Y;
            }
        }
        public FloatRect GetBounds()
        {
            return _sprite.GetGlobalBounds();
        }
        public bool isFasedWith(GameObject obj)
        {
            FloatRect spriteBounds = GetBounds();
            FloatRect facedObjBounds = obj.GetBounds();

            return spriteBounds.Intersects(facedObjBounds);
        }
    }
}