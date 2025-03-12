using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using System.Text.RegularExpressions;

public class CustomChatPlugin : BasePlugin
{
    public override string ModuleName => "Rainbow Chat Colors";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "Numele Tău";

    public override void Load(bool hotReload)
{
    RegisterEventHandler<EventPlayerChat>((@event, info) =>
    {
        var player = @event.Player;  // Înlocuim `Sender` cu `Player` sau un alt câmp adecvat
        if (player == null || !player.IsValid)
            return HookResult.Continue;

        string message = @event.Text;
        string playerName = player.PlayerName;

        // Aplicăm efectul Rainbow dacă mesajul conține {RAINBOW}
        if (message.Contains("{RAINBOW}"))
        {
            playerName = Rainbow(playerName);
            message = message.Replace("{RAINBOW}", ""); // Eliminăm tag-ul
        }

        Server.PrintToChatAll($" {playerName}: {message}");

        return HookResult.Handled; // Prevenim dublarea mesajului default
    });
}

    // Funcție pentru efect Rainbow
    private string Rainbow(string text)
    {
        string[] colors = { "FF0000", "FF7F00", "FFFF00", "00FF00", "0000FF", "4B0082", "8B00FF" }; // Culori curcubeu
        char[] letters = text.ToCharArray();
        string coloredText = "";
        int colorIndex = 0;

        foreach (char letter in letters)
        {
            if (letter != ' ') // Evităm colorarea spațiilor
            {
                coloredText += $"<font color='#{colors[colorIndex]}'>{letter}</font>";
                colorIndex = (colorIndex + 1) % colors.Length; // Trecem la următoarea culoare
            }
            else
            {
                coloredText += " ";
            }
        }
        return coloredText;
    }
}
