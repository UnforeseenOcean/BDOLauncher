using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDOLauncher.Properties;
using Microsoft.Win32;

namespace BDOLauncher
{
    public partial class LauncherWindow : Form
    {
        private bool _isAutoStarting;
        private int _autoStartIn;
        private bool _isOverlayEnabled;
        private string _workingDirectory;
        private string _executablePath;

        public LauncherWindow()
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(Settings.Default.Email)) emailTextBox.Text = Settings.Default.Email;
            var pass = MakePassword(Settings.Default.Pcrypt);
            if (!string.IsNullOrWhiteSpace(pass))
            {
                passwordTextBox.Text = pass;
                passwordCheckbox.Checked = true;
            }
            if (Settings.Default.AutoStart)
            {
                autoStartCheckbox.Checked = true;
                autoStartCheckbox.Text = "Autostart in 3";
                _isAutoStarting = true;
                _autoStartIn = 3;
                autoStartTimer.Start();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                // Check for Steam Environmental variable. Won't be set if steam isn't running as admin.
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("SteamGameId")))
                {
                    _isOverlayEnabled = true;
                    steamOverlayRadio.Checked = true;
                }
            }
            catch (Exception)
            {
            }

            try
            {
                // Read InstallLocation from Registry
                var installLocation = (string)Registry.GetValue(
                    "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{C1F96C92-7B8C-485F-A9CD-37A0708A2A60}", "InstallLocation", "");
                if (string.IsNullOrWhiteSpace(installLocation))
                {
                    installLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                            "Black Desert Online");
                    if (!Directory.Exists(installLocation))
                        throw new FileNotFoundException("Black Desert install location could not be found. (" + installLocation + ")");
                }
                _workingDirectory = Path.Combine(installLocation, "bin64");
                _executablePath = Path.Combine(_workingDirectory, "BlackDesert64.exe");
                // Check for 32 bit installation
                if (!File.Exists(_executablePath))
                {
                    _workingDirectory = Path.Combine(installLocation, "bin");
                    _executablePath = Path.Combine(_workingDirectory, "BlackDesert32.exe");
                    if (!File.Exists(_executablePath)) throw new FileNotFoundException("BlackDesert executable missing from install location.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Failed to find a Black Desert installation, please report this problem in the reddit thread. The Launcher won't work.\n" + ex,
                    "Error");
            }
        }

        // A modified version of the javascript function from the Launcher webpage
        private Response Login(string email, string password)
        {
            try
            {
                var json = new JsonParser();
                var data = Encoding.UTF8.GetBytes("email=" + WebUtility.UrlEncode(email) + "&password=" + WebUtility.UrlEncode(password));

                var req = WebRequest.CreateHttp("https://www.blackdesertonline.com/launcher/l/api/Login.json");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.ContentLength = data.Length;
                using (var rstream = req.GetRequestStream())
                {
                    rstream.Write(data, 0, data.Length);
                }
                using (var res = req.GetResponse())
                {
                    using (var rstream = res.GetResponseStream())
                    {
                        return json.Parse<Response>(rstream);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "An error occurred while logging in:\n" + e, "Error");
                return null;
            }
        }

        // A modified version of the javascript function from the Launcher webpage
        private void AuthError(int type, string code, string msg)
        {
            switch (type)
            {
                case 0:
                    MessageBox.Show(this, "A network error occurred.", "Error");
                    break;
                case 1:
                    switch (code)
                    {
                        case "904": MessageBox.Show(this, "Your country is restricted from accessing our service.", "Error"); break;
                        case "700": MessageBox.Show(this, "You have to accept the Terms of Use first", "Error"); break;
                        case "701": MessageBox.Show(this, "You have to accept the Terms of Use first", "Error"); break;
                        case "702": MessageBox.Show(this, "Head start access only possible with Pre - Order Package! Regular Packages grant access at Official Launch: 3rd March.", "Error"); break;
                        default: MessageBox.Show(this, "Authentication is temporary unavailable. (" + code + ")", "Error"); break;
                    }
                    break;
                case 2:
                    var errmsg = "Unexpected error. (" + code + ").";
                    switch (code)
                    {
                        case "211": // AUTH_ERROR__USER_TOKEN_UNKNOWN
                        case "212": // AUTH_ERROR__USER_TOKEN_EXPIRED
                        case "213": // AUTH_ERROR__USER_TOKEN_RESTRICT_IP
                            errmsg = "Your session has expired. (" + code + ").";
                            Logout();
                            break;
                        case "221": // AUTH_ERROR__EMAIL_UNKNOWN
                        case "231": // AUTH_ERROR__PASSWORD_MISMATCHED
                            errmsg = "Your email or password is incorrect. Please try again.";
                            break;
                        case "402":
                            errmsg = "Email not verified. Please check your inbox and confirm your e-mail.";
                            break;
                        case "412": // CLIENT_INFO_ERROR__SERVICE_DENIED_ACTION
                        case "413": // CLIENT_INFO_ERROR__SERVICE_DENIED_COUNTRY
                        case "414": // CLIENT_INFO_ERROR__SERVICE_DENIED_IP
                        case "415": // CLIENT_INFO_ERROR__SERVICE_DENIED_USER
                            errmsg = "The game service is on maintenance now. (" + code + ").";
                            break;
                    }
                    MessageBox.Show(this, errmsg, "Error");
                    break;
            }
        }

        private void Logout()
        {
            startButton.Text = "Start";
            startButton.Enabled = true;
            emailTextBox.Enabled = true;
            passwordTextBox.Enabled = true;
        }

        private static string MakePassword(string p)
        {
            try
            {
                return string.IsNullOrWhiteSpace(p) ? "" : Encrypter.Decrypt(p, "XnJCh0H7dLFoP23dx7");
            }
            catch
            {
                return "";
            }
        }

        private void Remember(string email, string pass)
        {
            Settings.Default.Email = email;
            if (!string.IsNullOrWhiteSpace(pass))
            {
                try
                {
                    Settings.Default.Pcrypt = Encrypter.Encrypt(pass, "XnJCh0H7dLFoP23dx7");
                }
                catch
                {
                    Settings.Default.Pcrypt = "";
                }
            }
            else Settings.Default.Pcrypt = "";
            Settings.Default.AutoStart = autoStartCheckbox.Checked;
            Settings.Default.Save();
        }

        private void ApiCall(Response response, Action<Result> onSuccess)
        {
            if (response?.api == null || response.result == null) AuthError(0, "", "");
            else switch (response.api.code)
                {
                    case 100:
                        onSuccess(response.result);
                        break;
                    case 801:
                        AuthError(2, response.api.additionalInfo.code, response.api.additionalInfo.msg);
                        break;
                    default:
                        AuthError(1, response.api.code.ToString(), response.api.codeMsg);
                        break;
                }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // Disable autostart just in case it's still running
            autoStartTimer.Stop();
            _isAutoStarting = false;
            autoStartCheckbox.Text = "Autostart";

            var email = emailTextBox.Text;
            var password = passwordTextBox.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(this, "Please enter your email address.", "Error");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(this, "Please enter your password.", "Error");
                return;
            }

            startButton.Enabled = false;
            emailTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            startButton.Text = "Wait..";

            try
            {
                ApiCall(Login(email, password), data =>
                {
                    Remember(email, passwordCheckbox.Checked ? password : "");
                    ApiCall(CreatePlayToken(data.token), result =>
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = _executablePath,
                            UseShellExecute = true,
                            Arguments = data.token,
                            WorkingDirectory = _workingDirectory
                        };
                        Process.Start(psi);
                        Application.Exit();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to login and start Black Desert:\n" + ex, "Error");
            }

            Logout();
        }

        // A modified version of the javascript function from the Launcher webpage
        private Response CreatePlayToken(string loginToken)
        {
            try
            {
                var json = new JsonParser();
                var data = Encoding.UTF8.GetBytes("token=" + WebUtility.UrlEncode(loginToken));

                var req = WebRequest.CreateHttp("https://www.blackdesertonline.com/launcher/l/api/CreatePlayToken.json");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.ContentLength = data.Length;
                using (var rstream = req.GetRequestStream())
                {
                    rstream.Write(data, 0, data.Length);
                }
                using (var res = req.GetResponse())
                {
                    using (var rstream = res.GetResponseStream())
                    {
                        return json.Parse<Response>(rstream);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "An error occurred while fetching the play token:\n" + e, "Error");
                return null;
            }
        }

        private void autoStartCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (_isAutoStarting)
            {
                autoStartCheckbox.Text = "Autostart";
                autoStartTimer.Stop();
                _isAutoStarting = false;
                Settings.Default.AutoStart = false;
                Settings.Default.Save();
            }
        }

        private void autoStartTimer_Tick(object sender, EventArgs e)
        {
            if (--_autoStartIn < 0) startButton.PerformClick();
            else autoStartCheckbox.Text = "Autostart in " + _autoStartIn;
        }

        private void steamOverlayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (steamOverlayRadio.Checked != _isOverlayEnabled) steamOverlayRadio.Checked = _isOverlayEnabled;
        }

        private void overlayHelpButton_Click(object sender, EventArgs e)
        {
            new OverlayHelpWindow().ShowDialog(this);
        }
    }
}
