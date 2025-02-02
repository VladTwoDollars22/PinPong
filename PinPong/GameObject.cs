﻿using SFML.Graphics;
using SFML.System;

namespace PinPong
{
    public class GameObject
    {
        private Texture _texture;
        private Sprite _sprite;
        private Vector2f _velocity;
        private float speed;

        public Sprite GetSprite() => _sprite;
        public Vector2f GetVelocity() => _velocity;

        public GameObject(Vector2f spawnPosition,Vector2f scale,string textureName)
        {
            _texture = new Texture(textureName);
            _sprite = new Sprite(_texture);

            _sprite.Position = spawnPosition;
            _sprite.Scale = scale;

            speed = 340;
        }
        public void SetVelocity(Vector2f direction)
        {
            _velocity = Normalize(direction) * speed;
        }
        public void Move(float deltaTime)
        {
            _sprite.Position += _velocity * deltaTime;
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
        public bool IsFasedWith(GameObject obj)
        {
            FloatRect spriteBounds = GetBounds();
            FloatRect facedObjBounds = obj.GetBounds();

            return spriteBounds.Intersects(facedObjBounds);
        }
        public void SetPosition(Vector2f pos)
        {
            _sprite.Position = pos;
        }
        private Vector2f Normalize(Vector2f vector)
        {
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            if (length > 0)
            {
                return new Vector2f(vector.X / length, vector.Y / length);
            }
            else
            {
                return new Vector2f(0, 0); 
            }
        }
    }
}