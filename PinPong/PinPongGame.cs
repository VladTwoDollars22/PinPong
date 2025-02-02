﻿using System;
using System.IO;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PinPong
{
    public class PinPongGame
    {
        private Player _player1;
        private Player _player2;

        private RenderWindow window;

        private GameObject _border1;
        private GameObject _border2;

        private GameObject _projectile;

        private List<GameObject> movableObjects;
        private List<GameObject> drawableObjects;

        private Font _font;
        private Text _text;

        private float _updateTrigger;

        private float deltaTime;

        private int targetFPS;

        public PinPongGame()
        {
            window = new RenderWindow(new VideoMode(1600, 900), "Game window");

            targetFPS = 120;

            _updateTrigger = 1f / targetFPS;
        }
        public void GameProcess()
        {
            Initialisation();

            Clock clock = new Clock();
            long lastFrameTime = clock.ElapsedTime.AsMilliseconds();

            while (window.IsOpen)
            {
                long currentTime = clock.ElapsedTime.AsMilliseconds();
                deltaTime = (currentTime - lastFrameTime) / 1000f;

                InputProcess();

                if (deltaTime > _updateTrigger)
                {
                    lastFrameTime = currentTime;

                    Logic();
                    Render();
                }
            }
        }

        private void Initialisation()
        {
            InitializeWindowEvents();
            InitializePlayers();
            InitializeGameObjects();
        }

        private void InitializeWindowEvents()
        {
            window.Closed += WindowClosed;
        }

        private void InitializeGameObjects()
        {
            _font = new Font(PathUtilite.CalculatePath("fonts\\font.ttf"));

            _projectile = new(Config.projectilePosition, new Vector2f(0.05f, 0.05f), PathUtilite.CalculatePath("textures\\asteroid.png"));
            _border1 = new(Config.border1Position, new Vector2f(5f, 0.1f), PathUtilite.CalculatePath("textures\\barrier.jpg"));
            _border2 = new(Config.border2Position, new Vector2f(5f, 0.1f), PathUtilite.CalculatePath("textures\\barrier.jpg"));

            _text = new Text(GetTextFilling(), _font, 50)
            {
                Position = new Vector2f(750, 100),
                FillColor = Color.White
            };

            movableObjects = new List<GameObject> { _projectile, _player1.playerObj, _player2.playerObj };
            drawableObjects = new List<GameObject> { _projectile, _border1, _border2, _player1.playerObj, _player2.playerObj };
        }

        private void InitializePlayers()
        {
            _player1 = new Player(Config.player1Position, Config.out1Position);
            _player2 = new Player(Config.player2Position, Config.out2Position);
        }

        private void Logic()
        {
            FacesLogic();
            MovingLogic();
        }

        private void MovingLogic()
        {
            foreach(GameObject obj in movableObjects)
            {
                obj.Move(deltaTime);
            }
        }

        private void FacesLogic()
        {
            if (_projectile.IsFasedWith(_player1.playerObj) || _projectile.IsFasedWith(_player2.playerObj))
            {
                _projectile.Reflect(false, true);
            }

            if (_projectile.IsFasedWith(_border1) || _projectile.IsFasedWith(_border2))
            {
                _projectile.Reflect(true, false);
            }

            if (_projectile.IsFasedWith(_player1.outObj))
            {
                _player2.wins++;

                UpdateTextFilling();

                ResetProjectile();
            }

            if (_projectile.IsFasedWith(_player2.outObj))
            {
                _player1.wins++;

                UpdateTextFilling();

                ResetProjectile();
            }
        }

        static void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }

        private void Render()
        {
            window.Clear(Color.Black);

            DrawObjects();

            DrawText();

            window.Display();
        }

        private void DrawObjects()
        {
            foreach (GameObject obj in drawableObjects)
            {
                window.Draw(obj.GetSprite());
            }
        }

        private void DrawText()
        {
            window.Draw(_text);
        }

        private void UpdateTextFilling()
        {
            _text.DisplayedString = GetTextFilling();
        }

        private string GetTextFilling()
        {
            return _player1.wins.ToString() + ":" + _player2.wins.ToString();
        }

        private void InputProcess()
        {
            window.DispatchEvents();

            _player1.playerObj.SetVelocity(new Vector2f(0, 0));
            _player2.playerObj.SetVelocity(new Vector2f(0, 0));

            if (_projectile.GetVelocity() == new Vector2f(0, 0) && Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                _projectile.SetVelocity(GetRandomVelocity());
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                _player1.playerObj.SetVelocity(new Vector2f(0, -1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _player1.playerObj.SetVelocity(new Vector2f(0, 1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                _player2.playerObj.SetVelocity(new Vector2f(0, -1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                _player2.playerObj.SetVelocity(new Vector2f(0, 1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                ResetProjectile();
            }
        }

        private Vector2f GetRandomVelocity()
        {
            Random rand = new Random();

            float randomX = (float)(rand.NextDouble() * 1.0 - 0.5);
            float randomY = (float)(rand.NextDouble() * 1.0 - 0.5);

            return new Vector2f(randomX, randomY);
        }

        private void ResetProjectile()
        {
            _projectile.SetVelocity(new Vector2f(0, 0));
            _projectile.SetPosition(new Vector2f(800, 450));
        }
    }
}
