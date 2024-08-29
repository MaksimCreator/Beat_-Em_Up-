using View;

namespace Model
{
    public static class ServiceInitialized
    {
        public static void RegisterService(ServiceLocator locator,EnemyRegistry allEnemy,CoordinateGenerator genirator,EnemyViewFactory enemyFactory
            ,Mediator mediator, PlayerConffig playerConffig, EnemySpawnerConffig enemySpawnerConffig,InputRouter input)
        {
            RegisterConffig(locator, playerConffig, enemySpawnerConffig);
            RegisterFactory(locator, enemyFactory);
            RegisterPhysics(locator);
            RegisterInputRouter(locator, input);
            RegisterMediator(locator, mediator);
            RegisterAllEnemy(locator, allEnemy);
            RegisterCoordinateGenerator(locator, genirator);
        }

        private static void RegisterPhysics(ServiceLocator locator)
        { 
            CollisionRecord record = new CollisionRecord();
            locator.RegisterService(new PhysicsRouter(record.Values));
        }

        private static void RegisterConffig(ServiceLocator locator, PlayerConffig playerConffig, EnemySpawnerConffig enemySpawnerConffig)
        {
            RegisterPlayerConffig(locator, playerConffig);
            RegisterEnemySpawnerConffig(locator, enemySpawnerConffig);
        }

        private static void RegisterFactory(ServiceLocator locator,EnemyViewFactory factory)
        { 
            locator.RegisterService(factory);
        } 

        private static void RegisterPlayerConffig(ServiceLocator locator, PlayerConffig playerConffig)
        => locator.RegisterService(playerConffig);

        private static void RegisterEnemySpawnerConffig(ServiceLocator locator, EnemySpawnerConffig enemySpawnerConffig)
        => locator.RegisterService(enemySpawnerConffig);

        private static void RegisterInputRouter(ServiceLocator locator,InputRouter input)
        => locator.RegisterService(input);

        private static void RegisterEnemyFactory(ServiceLocator locator,EnemyViewFactory factory)
        => locator.RegisterService(factory);

        private static void RegisterMediator(ServiceLocator locator,Mediator mediator)
        => locator.RegisterService(mediator);

        private static void RegisterAllEnemy(ServiceLocator locator,EnemyRegistry allEnemy)
        => locator.RegisterService(allEnemy);

        private static void RegisterCoordinateGenerator(ServiceLocator locator,CoordinateGenerator generator)
        => locator.RegisterService(generator);
    }
}