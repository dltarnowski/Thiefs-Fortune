
public class RangedTutorialNPC : rangedEnemyAI
{
    public void OnDestroy()
    {
        TutorialManager.instance.rangedEnemiesLeft--;
    }
}
