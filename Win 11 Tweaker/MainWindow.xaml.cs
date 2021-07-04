using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Win_11_Tweaker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow
    {
        public string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Meepure_Files/";

        public string GetTweakItem(string TweakName)
        {
            RegistryKey TweaksGlobalRegistryCreate = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            RegistryKey TweaksGlobalRegistryOpen = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            RegistryKey TweaksGlobalRegistryExpOpen = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer");

            var NewTweakName = TweaksGlobalRegistryOpen.GetValue(TweakName);

            if (TweakName == "EnableAutoTray")
            {
                var NewEATTweakName = TweaksGlobalRegistryExpOpen.GetValue(TweakName);

                if (NewEATTweakName != null)
                {
                    string StringedTweakName = NewEATTweakName.ToString();
                    return StringedTweakName;
                }
                else
                {
                    string StringedTweakName = "null";
                    return StringedTweakName;
                }
            }

            if (NewTweakName != null)
            {
                string StringedTweakName = NewTweakName.ToString();
                return StringedTweakName;
            }

            else
            {
                string StringedTweakName = "null";
                return StringedTweakName;
            }
        }

        private void CheckRegistryOnChanges()
        {
            RegistryKey TweaksGlobalRegistryExpCreate = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer");
            RegistryKey TweaksGlobalRegistryCreate = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");

            var a = GetTweakItem("Win11Tweaker_DeleteFolders");

            if (GetTweakItem("TaskbarSi") == "null")
            {
                TweaksGlobalRegistryCreate.SetValue("TaskbarSi", "1", RegistryValueKind.DWord);
            }

            if (GetTweakItem("Start_ShowClassicMode") == "null")
            {
                TweaksGlobalRegistryCreate.SetValue("Start_ShowClassicMode", "0", RegistryValueKind.DWord);
            }

            if (GetTweakItem("EnableAutoTray") == "null")
            {
                TweaksGlobalRegistryExpCreate.SetValue("EnableAutoTray", "1", RegistryValueKind.DWord);
            }

            TweaksGlobalRegistryCreate.Close();

            if (GetTweakItem("TaskbarSi") == "0")
            {
                UseSmallIconsToggle.IsChecked = true;
            }

            else
            {
                UseSmallIconsToggle.IsChecked = false;
            }

            if (GetTweakItem("Start_ShowClassicMode") == "1")
            {
                ReturnWin10StartmenuToggle.IsChecked = true;
            }

            else
            {
                ReturnWin10StartmenuToggle.IsChecked = false;
            }

            if (GetTweakItem("EnableAutoTray") == "1")
            {
                HideTrayIconsToggle.IsChecked = true;
            }

            else
            {
                HideTrayIconsToggle.IsChecked = false;
            }

        }

        public MainWindow()
        {
            InitializeComponent();
            CheckRegistryOnChanges();

            var baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Meepure_Files/";

            if (Directory.Exists(baseFolder) == false)
            {
                Directory.CreateDirectory(baseFolder);
            }
        }

        // Buttons below tweak descriptions

        private void Additional_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow SettingsWindow = new SettingsWindow();
            SettingsWindow.Show();
        }

        private void ExplorerReset_Click(object sender, RoutedEventArgs e)
        {
            if (Process.GetProcessesByName("explorer").Any())
            {
                try
                {
                    foreach (Process proc in Process.GetProcessesByName("explorer"))
                    {
                        proc.Kill();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        //Tweaks ToggleButtons

        private void UseSmallIconsToggle_Checked(object sender, RoutedEventArgs e)
        {
            RegistryKey saveKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            saveKey.SetValue("TaskbarSi", "0", RegistryValueKind.DWord);
            saveKey.Close();
        }

        private void UseSmallIconsToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            RegistryKey saveKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            saveKey.SetValue("TaskbarSi", "1", RegistryValueKind.DWord);
            saveKey.Close();
        }

        private void ReturnWin10StartmenuToggle_Checked(object sender, RoutedEventArgs e)
        {
            RegistryKey saveKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            saveKey.SetValue("Start_ShowClassicMode", "1", RegistryValueKind.DWord);
            saveKey.Close();
        }

        private void ReturnWin10StartmenuToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            RegistryKey saveKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
            saveKey.SetValue("Start_ShowClassicMode", "0", RegistryValueKind.DWord);
            saveKey.Close();
        }

        private void HideTrayIconsToggle_Checked(object sender, RoutedEventArgs e)
        {
            RegistryKey saveKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer");
            saveKey.SetValue("EnableAutoTray", "1", RegistryValueKind.DWord);
            saveKey.Close();
        }

        private void HideTrayIconsToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            RegistryKey saveKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer");
            saveKey.SetValue("EnableAutoTray", "0", RegistryValueKind.DWord);
            saveKey.Close();
        }

        //Descriptions for labels

        private void DescriptionToDefault()
        {
            DescriprionTextBlock.Text = @"Welcome to Windows 11 Tweaker!

Put your cursor on the name of the tweak
to get description and scroll down to see
more tweaks.

Author: Tinkott";
        }

        private void UseSmallIconsLabel_MouseMove(object sender, MouseEventArgs e)
        {
            DescriprionTextBlock.Text = @"This option make your taskbar and icons 
smaller. If you need more workspace, 
this will be useful for you.

(Restart Explorer.exe process needed)";
        }

        private void ReturnStartMenuLabel_MouseMove(object sender, MouseEventArgs e)
        {
            DescriprionTextBlock.Text = @"This option returns Start Menu from
Windows 10 to Windows 11.

(Restart Explorer.exe process needed)";
        }

        private void HideTrayIconsLabel_MouseMove(object sender, MouseEventArgs e)
        {
            DescriprionTextBlock.Text = @"This option hides the icons of 
applications running in the 
background in the system tray.

(Restart Explorer.exe process needed)
";
        }

        private void ForAllTweakLabels_MouseLeave(object sender, MouseEventArgs e)
        {
            DescriptionToDefault();
        }



        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }

}
