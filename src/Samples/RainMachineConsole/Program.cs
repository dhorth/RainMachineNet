using Newtonsoft.Json;
using RainMachineNet;
using RainMachineNet.Support;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RainMachineConsole
{
    public class RainMachineConsole
    {
        IRainMaker _ranMan;
        static async Task<int> Main(string[] args)
        {
            var test = new RainMachineConsole();
            await test.Login();
            await test.Start();

            return 1;
        }

        private async Task Start()
        {
            while (true)
            {
                var zone = await GetZone();
                var minutes = GetTime();
                if (minutes == 0)
                    continue;

                var evt = new WateringEventTest();
                Console.Clear();
                await StopAll();
                await _ranMan.Subscribe(evt);
                await _ranMan.ZoneStart(zone, minutes * 60);
                while (evt.IsWatering)
                {
                    Console.SetCursorPosition(0, 0);
                    WriteLine("Press 'Y' to Queue up another Zone or 'X' to stop watering");
                    var cmd = Reader.ReadLine(1000).ToUpper();
                    if (cmd == "Y")
                        await Start();

                    if (cmd == "X")
                        break;

                }
                Console.Clear();
                Console.WriteLine("Watering completed!");
                await StopAll();
                await _ranMan.UnSubscribe(evt);
                Thread.Sleep(1000);
            }
        }
        private async Task StopAll()
        {
            try
            {
                var zoneResponse = await _ranMan.GetAllZones();
                if (zoneResponse.Zones.Any(a => a.active))
                    await _ranMan.ZoneStopAll();
            }
            catch (Exception ex)
            {
            }
        }

        private async Task Login()
        {
            var login = LoginCredentials.Load();
            WriteLine("Enter IP/NetName of device or 'X' to exit");
            Console.Write(login.NetName);
            var cmd = Console.ReadLine();
            if (cmd == "X")
                Exit();
            if (!string.IsNullOrWhiteSpace(cmd))
                login.NetName = cmd;

            WriteLine("Enter UserName for device or 'X' to exit");
            Console.Write(login.UserId);
            cmd = Console.ReadLine();
            if (cmd == "X")
                Exit();
            if (!string.IsNullOrWhiteSpace(cmd))
                login.UserId = cmd;

            WriteLine("Enter Password for device or 'X' to exit");
            Console.Write(login.Password);
            cmd = Console.ReadLine();
            if (cmd == "X")
                Exit();
            if (!string.IsNullOrWhiteSpace(cmd))
                login.Password = cmd;

            _ranMan = new RainMaker();
            bool loggedIn = false;
            try
            {
                loggedIn = await _ranMan.LoginAsync(login.NetName, login.UserId, login.Password);
            }
            catch (RainMakerLoginException ex)
            {
            }
            catch (Exception ex)
            {
            }


            if (!loggedIn)
            {
                Console.Clear();
                Console.WriteLine("Failed to Login with supplied credentials, try again");
                await Login();
            }
            WriteLine("Login successful");
            Thread.Sleep(2000);
            login.Save();
        }

        private async Task<int> GetZone()
        {
            int zone = 0;
            var zoneResponse = await _ranMan.GetAllZones();
            var zones = zoneResponse.Zones;
            while (true)
            {
                Console.Clear();
                WriteLine("Enter zone number to start or 0 to exit");
                var cmd = Console.ReadLine();
                if (cmd == "0")
                    Exit();

                if (!int.TryParse(cmd, out zone))
                {
                    WriteLine("Zone must be a numeric, try again ...");
                    continue;
                }

                //make sure is a valid zone
                var valid = zones.FirstOrDefault(a => a.uid == zone);
                if (valid == null)
                {
                    WriteLine($"Zone {zone} is not a recongized zone number, try again ...");
                    continue;
                }

                if (!valid.active)
                {
                    WriteLine($"Zone {zone} is not active, only active zone numbers can be turned on, try again ...");
                    continue;
                }

                break;
            }
            return zone;
        }
        private int GetTime()
        {
            int min = 0;
            while (true)
            {
                WriteLine("Enter number to minutes to run or 0 to exit");
                var cmd = Console.ReadLine();
                if (cmd == "0")
                    return 0;

                if (!int.TryParse(cmd, out min))
                {
                    WriteLine("minutes must be a numeric, try again ...");
                    continue;
                }
                break;
            }
            return min;
        }

        private void WriteLine(string msg)
        {
            Console.WriteLine($"\x1b[1m{msg}\x1b[0m");
        }


        private void Exit(int ret = 0)
        {
            Environment.Exit(ret);
        }
    }

    internal class Reader
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string input;

        static Reader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadLine();
                gotInput.Set();
            }
        }

        // omit the parameter to read a line without a timeout
        public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return input;
            else
                return string.Empty;
        }
    }

    public class LoginCredentials
    {
        private const string _fileName = "creds.json";
        public string NetName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public LoginCredentials() { }
        public LoginCredentials(string netName, string user, string pwd)
        {
            NetName = netName;
            UserId = user;
            Password = pwd;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(_fileName, json);
        }
        public static LoginCredentials Load()
        {
            var ret = new LoginCredentials();
            if (File.Exists(_fileName))
            {
                var json = File.ReadAllText(_fileName);
                if (!string.IsNullOrWhiteSpace(json))
                    ret = JsonConvert.DeserializeObject<LoginCredentials>(json);
            }
            return ret;
        }
    }
}
