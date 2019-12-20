using UnityEngine.Events;
using SimpleClicker.Events.Components;
using SimpleClicker.Extensions;

namespace SimpleClicker.Events
{
    public static class EventHandler
    {
        public static UnityEvent enemyDead = new UnityEvent();
        public static JsonDownloaded jsonDownloaded = new JsonDownloaded();
    }
}

namespace SimpleClicker.Events.Components
{
    public class JsonDownloaded : UnityEvent<Leader> { };
}
