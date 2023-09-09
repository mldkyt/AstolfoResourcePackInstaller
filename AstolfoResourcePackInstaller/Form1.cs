using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
using Timer = System.Windows.Forms.Timer;

namespace AstolfoResourcePackInstaller
{
    public partial class Form1 : Form
    {
        private string _iconFile;
        private string _status = "[IDLE]";
        private string _currentlyDownloadingFile = "";
        private LanguageData _languageData;

        public Form1()
        {
            InitializeComponent();
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            button4.Enabled = Directory.Exists(Path.Combine(appdata, ".minecraft", "resourcepacks"));
            _languageData = new LanguageData()
            {
                GameTitle = true,
                CherryToFemboy = true,
                MenuButtons = true
            };
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button5.Enabled = checkBox2.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = radioButton2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Enabled = false;
            var loadingForm = new FormProgress();
            loadingForm.Show();
            var loadingFormUpdater = new Timer();
            loadingFormUpdater.Tick += delegate
            {
                if (_status == "[FINISH]")
                {
                    Enabled = true;
                    loadingForm.Dispose();
                    loadingFormUpdater.Stop();
                    loadingFormUpdater.Dispose();
                    return;
                }

                if (_status.StartsWith("[ERROR]"))
                {
                    MessageBox.Show(_status.Substring(6), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Enabled = true;
                    loadingForm.Dispose();
                    loadingFormUpdater.Stop();
                    loadingFormUpdater.Dispose();
                    return;
                }
                loadingForm.SetText(_status);
                loadingForm.Update();
            };
            loadingFormUpdater.Interval = 1;
            loadingFormUpdater.Start();
            var t = new Thread(() =>
            {
                MakeResourcePack().Wait();
            });
            t.Start();
        }

        private async Task MakeResourcePack()
        {
            try
            {
                Directory.CreateDirectory("ResourcePack");
                File.WriteAllText("ResourcePack\\pack.mcmeta",
                    $@"{{
  ""pack"": {{
    ""pack_format"": 15,
    ""description"": ""{textBox1.Text}""
  }}
}}");
                _status = "Saved pack.mcmeta";
                var client = new WebClient();
                var stopwatch = Stopwatch.StartNew();
                client.DownloadProgressChanged += delegate(object sender, DownloadProgressChangedEventArgs args)
                {
                    if (stopwatch.ElapsedMilliseconds < 50) return;
                    stopwatch.Restart();
                    _status = $"Downloading {_currentlyDownloadingFile}, {args.ProgressPercentage:N0}%...";
                };
                client.Headers.Add("User-Agent", "AstolfoResourcePackInstaller");

                #region Icon

                if (radioButton1.Checked)
                {
                    _status = "Downloading icon...";
                    _currentlyDownloadingFile = "icon";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/pack.png?raw=true",
                        "ResourcePack\\pack.png");
                    _status = "Downloaded icon.";
                }
                else
                {
                    _status = "Copying icon...";
                    if (string.IsNullOrEmpty(_iconFile) || !File.Exists(_iconFile))
                    {
                        _status = "Icon not found, downloading default icon...";
                        _currentlyDownloadingFile = "icon";
                        await client.DownloadFileTaskAsync(
                            "https://github.com/Astolph0/AstolfoResourcePack/blob/master/pack.png?raw=true",
                            "ResourcePack\\pack.png");
                    }
                    else
                    {
                        _status = "Copying icon...";
                        File.Copy(_iconFile, "ResourcePack\\pack.png");
                        _status = "Copied icon";
                    }
                }

                #endregion

                #region Panorama

                if (checkBox3.Checked)
                {
                    _status = "Downloading panorama...";
                    Directory.CreateDirectory("ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background");
                    _status = "Downloading 1/7 files for panorama...";
                    _currentlyDownloadingFile = "Panorama 1/7";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/background/panorama_0.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background\\panorama_0.png");
                    _status = "Downloading 2/7 files for panorama...";
                    _currentlyDownloadingFile = "Panorama 2/7";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/background/panorama_1.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background\\panorama_1.png");
                    _status = "Downloading 3/7 files for panorama...";
                    _currentlyDownloadingFile = "Panorama 3/7";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/background/panorama_2.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background\\panorama_2.png");
                    _status = "Downloading 4/7 files for panorama...";
                    _currentlyDownloadingFile = "Panorama 4/7";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/background/panorama_3.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background\\panorama_3.png");
                    _status = "Downloading 5/7 files for panorama...";
                    _currentlyDownloadingFile = "Panorama 5/7";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/background/panorama_4.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background\\panorama_4.png");
                    _status = "Downloading 6/7 files for panorama...";
                    _currentlyDownloadingFile = "Panorama 6/7";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/background/panorama_5.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background\\panorama_5.png");
                    _status = "Downloading 7/7 files for panorama...";
                    _currentlyDownloadingFile = "Panorama 7/7";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/background/panorama_overlay.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\background\\panorama_overlay.png");
                    _status = "Downloaded panorama.";
                }

                #endregion

                #region Menu Music

                if (checkBox4.Checked)
                {
                    _status = "Downloading menu music...";
                    Directory.CreateDirectory("ResourcePack\\assets\\minecraft\\sounds\\music\\menu");
                    _status = "Downloading 1/4 files for menu music...";
                    _currentlyDownloadingFile = "Menu music 1/4";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/raw/master/assets/minecraft/sounds/music/menu/menu1.ogg",
                        "ResourcePack\\assets\\minecraft\\sounds\\music\\menu\\menu1.ogg");
                    _status = "Downloading 2/4 files for menu music...";
                    _currentlyDownloadingFile = "Menu music 2/4";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/raw/master/assets/minecraft/sounds/music/menu/menu2.ogg",
                        "ResourcePack\\assets\\minecraft\\sounds\\music\\menu\\menu2.ogg");
                    _status = "Downloading 3/4 files for menu music...";
                    _currentlyDownloadingFile = "Menu music 3/4";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/raw/master/assets/minecraft/sounds/music/menu/menu3.ogg",
                        "ResourcePack\\assets\\minecraft\\sounds\\music\\menu\\menu3.ogg");
                    _status = "Downloading 4/4 files for menu music...";
                    _currentlyDownloadingFile = "Menu music 4/4";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/raw/master/assets/minecraft/sounds/music/menu/menu4.ogg",
                        "ResourcePack\\assets\\minecraft\\sounds\\music\\menu\\menu4.ogg");
                    _status = "Downloaded menu music.";
                }

                #endregion

                #region Menu Logo & Edition

                if (checkBox6.Checked)
                {
                    _status = "Downloading menu logo & edition...";
                    Directory.CreateDirectory("ResourcePack\\assets\\minecraft\\textures\\gui\\title");
                    _status = "Downloading 1/3 files for menu logo & edition...";
                    _currentlyDownloadingFile = "Menu logo 1/3";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/minecraft.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\minecraft.png");
                    _status = "Downloading 2/3 files for menu logo & edition...";
                    _currentlyDownloadingFile = "Menu logo 2/3";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/minceraft.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\minceraft.png");
                    _status = "Downloading 3/3 files for menu logo & edition...";
                    _currentlyDownloadingFile = "Menu logo 3/3";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/edition.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\gui\\title\\edition.png");
                    _status = "Downloaded menu logo & edition.";
                }

                #endregion

                #region Default Server & Default Resource Pack Icon

                if (checkBox7.Checked)
                {
                    _status = "Downloading default server & default resource pack icon...";
                    Directory.CreateDirectory("ResourcePack\\assets\\minecraft\\textures\\misc");
                    _status = "Downloading 1/2 files for default server & default resource pack icon...";
                    _currentlyDownloadingFile = "Default server & resource pack icon 1/2";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/misc/unknown_pack.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\misc\\unknown_pack.png");
                    _status = "Downloading 2/2 files for default server & default resource pack icon...";
                    _currentlyDownloadingFile = "Default server & resource pack icon 2/2";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/misc/unknown_server.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\misc\\unknown_server.png");
                    _status = "Downloaded default server & default resource pack icon.";
                }

                #endregion

                #region Astolfo of Undying

                if (checkBox5.Checked)
                {
                    _status = "Downloading Astolfo of Undying...";
                    Directory.CreateDirectory("ResourcePack\\assets\\minecraft\\textures\\item");
                    _status = "Downloading a file for Astolfo of Undying...";
                    _currentlyDownloadingFile = "Astolfo of Undying";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/item/totem_of_undying.png?raw=true",
                        "ResourcePack\\assets\\minecraft\\textures\\item\\totem_of_undying.png");

                    _status = "Writing language file for Astolfo of Undying...";
                    Directory.CreateDirectory("ResourcePack\\assets\\minecraft\\lang");
                    File.WriteAllText("ResourcePack\\assets\\minecraft\\lang\\en_us.json",
                        @"{
  ""advancements.adventure.totem_of_undying.title"": ""Astolfo Saves The Day Again"",
  ""advancements.adventure.totem_of_undying.description"": ""Use an Astolfo of Undying to undie the death and make it cute at the same time."",
  ""item.minecraft.totem_of_undying"": ""Astolfo of Undying""
}");
                    _status = "Downloaded Astolfo of Undying.";
                }

                #endregion

                #region Change Loading Screen

                if (checkBox1.Checked)
                {
                    _status = "Downloading loading screen...";
                    Directory.CreateDirectory("AstolfoJarMod\\assets\\minecraft\\textures\\gui\\title");
                    _status = "Downloading file for loading screen...";
                    _currentlyDownloadingFile = "Loading screen";
                    await client.DownloadFileTaskAsync(
                        "https://github.com/Astolph0/AstolfoResourcePack/blob/master/assets/minecraft/textures/gui/title/mojangstudios.png?raw=true",
                        "AstolfoJarMod\\assets\\minecraft\\textures\\gui\\title\\mojangstudios.png");
                    _status = "Downloaded loading screen.";
                }

                #endregion

                #region Language

                if (checkBox2.Checked)
                {
                    _status = "Creating language file...";
                    Directory.CreateDirectory("ResourcePack\\assets\\minecraft\\lang");
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("{");
                    if (_languageData.GameTitle)
                    {
                        stringBuilder.AppendLine("  \"title.multiplayer.lan\": \"Femboy Server\",");
                        stringBuilder.AppendLine("  \"title.multiplayer.other\": \"Femboy Server\",");
                        stringBuilder.AppendLine("  \"title.multiplayer.realms\": \"Tomboy Server\",");
                        stringBuilder.AppendLine("  \"title.singleplayer\": \"Femboy Edition\",");
                    }

                    if (_languageData.MenuButtons)
                    {
                        stringBuilder.AppendLine("  \"menu.singleplayer\": \"play alone :c\",");
                        stringBuilder.AppendLine("  \"menu.multiplayer\": \"play with femboys :3\",");
                        stringBuilder.AppendLine("  \"menu.online\": \"play with tomboys :3\",");
                        stringBuilder.AppendLine("  \"menu.quit\": \"NOOOOOOO DONT LEAVE ME ALONE >:3\",");
                    }

                    if (_languageData.CherryToFemboy)
                    {
                        stringBuilder.AppendLine("  \"biome.minecraft.cherry_grove\": \"Femboy Grove\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_button\": \"Femboy Button\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_door\": \"Femboy Door\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_fence\": \"Femboy Fence\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_fence_gate\": \"Femboy Fence Gate\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_hanging_sign\": \"Femboy Hanging Sign\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_leaves\": \"Femboy Leaves\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_log\": \"Femboy Log\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_planks\": \"Femboy Planks\",");
                        stringBuilder.AppendLine(
                            "  \"block.minecraft.cherry_pressure_plate\": \"Femboy Pressure Plate\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_sapling\": \"Femboy Sapling\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_sign\": \"Femboy Sign\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_slab\": \"Femboy Slab\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_stairs\": \"Femboy Stairs\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_trapdoor\": \"Femboy Trapdoor\",");
                        stringBuilder.AppendLine(
                            "  \"block.minecraft.cherry_wall_hanging_sign\": \"Femboy Wall Hanging Sign\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_wall_sign\": \"Femboy Wall Sign\",");
                        stringBuilder.AppendLine("  \"block.minecraft.cherry_wood\": \"Femboy Wood\",");
                        stringBuilder.AppendLine(
                            "  \"block.minecraft.potted_cherry_sapling\": \"Potted Femboy Sapling\",");
                        stringBuilder.AppendLine("  \"block.minecraft.stripped_cherry_log\": \"Stripped Femboy Log\",");
                        stringBuilder.AppendLine(
                            "  \"block.minecraft.stripped_cherry_wood\": \"Stripped Femboy Wood\",");
                        stringBuilder.AppendLine("  \"item.minecraft.cherry_boat\": \"Femboy Boat\",");
                        stringBuilder.AppendLine("  \"item.minecraft.cherry_chest_boat\": \"Femboy Boat with Chest\",");
                    }

                    if (checkBox5.Checked)
                    {
                        stringBuilder.AppendLine(
                            "  \"advancements.adventure.totem_of_undying.title\": \"Postmortal\",");
                        stringBuilder.AppendLine(
                            "  \"advancements.adventure.totem_of_undying.description\": \"Use an Astolfo of Undying to undie the death and make it cute at the same time.\",");
                        stringBuilder.AppendLine("  \"item.minecraft.totem_of_undying\": \"Astolfo of Undying\",");
                    }

                    stringBuilder.AppendLine("  \"menu.modded\": \" (Femboy Edition)\"");
                    _status = "Writing language file...";
                    File.WriteAllText("ResourcePack\\assets\\minecraft\\lang\\en_us.json", stringBuilder.ToString());
                    _status = "Created language file.";
                }

                #endregion

                _status = "Creating zip file(s)...";
                
                var zip1 = new ZipFile("ResourcePack.zip");
                zip1.AddFile("ResourcePack\\pack.mcmeta", "");
                zip1.AddFile("ResourcePack\\pack.png", "");
                zip1.AddDirectory("ResourcePack\\assets", "assets");
                zip1.Save("ResourcePack.zip");
                zip1.Dispose();

                _status = "Zip file created";

                if (checkBox1.Checked)
                {
                    _status = "Creating JAR file for JAR mod...";
                    var zip2 = new ZipFile("AstolfoJarMod.jar");
                    zip2.AddDirectory("AstolfoJarMod\\assets", "assets");
                    zip2.Save("AstolfoJarMod.jar");
                    zip2.Dispose();
                    _status = "JAR file created";
                }


                _status = "[FINISH]";
                client.Dispose();
            }
            catch (Exception e)
            {
                _status = "[ERROR] " + e;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"PNG|*.png";
            if (dialog.ShowDialog() != DialogResult.OK) return;
            _iconFile = dialog.FileName;
            button1.Text = Path.GetFileName(_iconFile);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var form = new LanguageSettings(_languageData);
            form.OKClicked += (o, args) => { _languageData = args; };
            form.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MessageBox.Show(
                    @"NOTE: This requires you to add a JAR mod into your game, which requires you to use a third-party launcher such as PrismLauncher or PolyMC.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var resourcepacks = Path.Combine(appdata, ".minecraft", "resourcepacks");
            Process.Start("explorer.exe", resourcepacks);
        }
    }
}