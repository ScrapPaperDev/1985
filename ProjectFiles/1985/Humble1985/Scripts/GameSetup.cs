using Disparity;

namespace Humble1985
{
    public class GameSetup
    {

        public IInstantiater instantiater;

        public IGameObject playerPlane;
        public IInstantiatable enemyPlane1;
        public IInstantiatable enemyPlane2;
        public IInstantiatable enemyPlane3;
        public IInstantiatable enemyPlane4;

        public ITextureUpdatable waterBG;
        public ITextureUpdatable island1;
        public ITextureUpdatable island2;
        public ITextureUpdatable island3;

        public IGameObject playerBullet;
        public ITextureUpdatable enemyBullet1;
        public ITextureUpdatable enemyBullet2;

        public ITextureUpdatable hud;
        public ITextureUpdatable gameOverPanel;


        public GameSetup()
        {

        }


        public void SetupGame()
        {
            instantiater.Instantiate(playerPlane, FakeVector3.Empty);
            Game.game.UpdateEnemiesForGameMode(enemyPlane1, enemyPlane2, enemyPlane3, enemyPlane4);
            waterBG.UpdateTexture();
            island1.UpdateTexture();
            island2.UpdateTexture();
            island3.UpdateTexture();
            hud.UpdateTexture();
            gameOverPanel.UpdateTexture();
        }




    }

    internal enum Modes
    {
        Classic,
        Modern
    }
}