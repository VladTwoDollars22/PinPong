using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace PinPong
{
    public struct Player
    {
        public GameObject playerObj;
        public GameObject outObj;
        public int wins;

        private string playerTexture = PathUtilite.CalculatePath("textures\\player.jpg");
        private string finishTexture = PathUtilite.CalculatePath("textures\\out.jpg");

        public Player(Vector2f playerSpawnPoint, Vector2f outObjPosition)
        {
            playerObj = new(playerSpawnPoint, new Vector2f(0.1f, 0.1f), playerTexture);
            outObj = new(outObjPosition, new Vector2f(0.1f, 0.9f), finishTexture);
            wins = 0;
        }
    }
}
