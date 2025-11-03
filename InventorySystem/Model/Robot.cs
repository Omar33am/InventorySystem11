using System;

namespace InventorySystem
{
    public class Robot
    {
        public const int urscriptPort = 30002;
        public const int dashboardPort = 29999;
        public string IpAddress = "localhost";

        public void SendString(int port, string message)
        {
            Console.WriteLine($"[SIMULERET] Sender besked til port {port}:");
            Console.WriteLine(message);
        }

        public void SendUrscript(string urscript)
        {
            Console.WriteLine("[SIMULERET] Starter URScript-program...");
            Console.WriteLine(urscript);
            Console.WriteLine("[SIMULERET] Bevægelse udført (ingen rigtig forbindelse).");
        }
    }
}