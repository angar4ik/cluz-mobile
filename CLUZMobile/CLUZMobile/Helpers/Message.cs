using CLUZ.Models;
using CLUZMobile.Interfaces;
using Xamarin.Forms;

namespace CLUZMobile.Helpers
{
    class Message
    {
        internal static void ShowWhoIsVoting(Player p)
        {
            if (p.AllowedToVote)
            {
                DependencyService.Get<IMessage>().ShortAlert($"{p.Name} is voting");
            }
        }
    }
}
