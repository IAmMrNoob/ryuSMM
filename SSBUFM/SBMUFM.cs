using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace shittyFileManager/*
    i keep forgeting that mod managers are just kinda like file managers for mods 
*/
{
    public class SBMUFM
    {
        private DirectoryInfo ryuRoot;
        private DirectoryInfo ryuSDCardUltimate;
        private DirectoryInfo smashAtmos;
        private DirectoryInfo ryuSDCard;

        private DirectoryInfo skylinePlugins;
        private DirectoryInfo ultimateMods;
        private DirectoryInfo rootDisabled;
        /*
         * should try to find save data location of ssbu
         * \Roaming\Ryujinx\bis\user\save\
         * somewher in there probably try to find a folder named save_data
         * or try to find the file named system_data.bin
         * to load like save datas quicker probably get from web
         * type shit
         * maybe scan is a better word than find
         */
        public SBMUFM() {
            RyuSlightSetup();/*
                              * ive been thinking since when i learned about hard links and how they worked
                              * should use it so then i could have my mods in a seperate folder and like yeah
                              * idk
                              * i think it would be a good idea but then i have to change lots of stuff i already made
                              * then i could probably just delete the mod instead of moving it maybe
                              * unsure i havent thought the whole thing through yet but i probably wont
                              * 
                              * i also discovered like a few mins ago (12:59AM AST 11/16/2025)
                              * that there is a mod manager made around the time i first started making this around the end of 2024 as i thought there wasnt any for ryujinx
                              * its called fightplanner i forgot but lowkey i dont think i should continue this if theres something better yk
                              * but i started this i wanna atleast finish it to a usable extent but instead of working on ui i just keep stalling finishing this class file
                              * and starting on propper UI not that i am busy its just that im lazy icl and my coding time consists of watching long youtube videos and writing like probably on avg 4 lines maybe
                              * which makes me waste like a whole like 1/3 of my day but i do finish it at somepoint probably at the next day at like rn
                              * this is probably like a little of inside of my mind thing but the reason i opened it today (or yesterday) was because i felt some motivation to finsihing stuff i havent finished which is alot but ill just do the ones i find more important to me
                              * so ill probably be working on it the next few days
                              * comments have no date or order i should porbably start adding those at the begining or somethings
                              */
        }
        private void RyuSlightSetup()// a long function of creating folders and making sure it exists ig
        {
            //auto add like the sdcard stuff
            string RyuPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Ryujinx";
            ryuRoot = new DirectoryInfo(RyuPath);
            if (!ryuRoot.Exists)
            {
                ryuRoot.Create();
            }
            ryuSDCard = new DirectoryInfo(ryuRoot.FullName + @"\sdcard");
            if (!ryuSDCard.Exists)
            {
                ryuSDCard.Create();
            }
            ryuSDCardUltimate = new DirectoryInfo(ryuSDCard.FullName + @"\ultimate");
            if (!ryuSDCardUltimate.Exists)
            {
                ryuSDCardUltimate.Create();
                ryuSDCardUltimate.CreateSubdirectory("mods");
                ultimateMods = new DirectoryInfo(ryuSDCardUltimate.FullName + @"\mods");
            }
            else
            {
                ultimateMods = new DirectoryInfo(ryuSDCardUltimate.FullName + @"\mods");
                if (!ultimateMods.Exists)
                {
                    ultimateMods.Create();
                }
            }
            rootDisabled = new DirectoryInfo(ryuSDCardUltimate.FullName + @"\_fisabled");
            FileInfo ModChanges = new(rootDisabled.FullName + @"\ModChanges.json");
            if (!rootDisabled.Exists)
            {
                rootDisabled.Create();
                rootDisabled.CreateSubdirectory("nro");
                rootDisabled.CreateSubdirectory("mods");
                var ws = ModChanges.CreateText();
                ws.WriteLine("{}");
                ws.Close();
            }
            else
            {
                DirectoryInfo nro = new DirectoryInfo(rootDisabled.FullName + @"\nro");
                DirectoryInfo mods = new DirectoryInfo(rootDisabled.FullName + @"\mods");
                if (!nro.Exists)
                {
                    nro.Create();
                }
                if (!mods.Exists)
                {
                    mods.Create();
                }
                if (!ModChanges.Exists)
                {
                    var ws = ModChanges.CreateText();
                    ws.WriteLine("{}");
                    ws.Close();
                }
            }
            //auto add like the atmosphere folder stuff
            DirectoryInfo _atmosphere = new DirectoryInfo(ryuSDCard.FullName + @"\atmosphere");
            if (!_atmosphere.Exists)
            {
                _atmosphere.Create();
            }
            DirectoryInfo _contents = new DirectoryInfo(_atmosphere.FullName + @"\contents");
            if (!_contents.Exists)
            {
                _contents.Create();
            }
            smashAtmos = new DirectoryInfo(_contents.FullName + @"\01006a800016e000");
            if (!smashAtmos.Exists)
            {
                smashAtmos.Create();
                smashAtmos.CreateSubdirectory("romfs");
                smashAtmos.CreateSubdirectory("exefs");
            }
            else
            {
                DirectoryInfo _romfs = new DirectoryInfo(smashAtmos.FullName + @"\romfs");
                DirectoryInfo _exefs = new DirectoryInfo(smashAtmos.FullName + @"\exefs");
                if (!_romfs.Exists)
                {
                    _romfs.Create();
                }
                if (!_exefs.Exists)
                {
                    _exefs.Create();
                }
            }
            DirectoryInfo _skyline = new DirectoryInfo(smashAtmos.FullName + @"\romfs\skyline");
            if (!_skyline.Exists)
            {
                _skyline.Create();
                _skyline.CreateSubdirectory("plugins");
                skylinePlugins = new DirectoryInfo(_skyline.FullName + @"\plugins");//do i think theres a better way than to paste this line twice? mayhaps but am i gonna try and figure it out?? nah.
            }
            else
            {
                skylinePlugins = new DirectoryInfo(_skyline.FullName + @"\plugins");
                if (!skylinePlugins.Exists)
                {
                    skylinePlugins.Create();
                }
            }
        }
        public Dictionary<// disabled{}, enalbled{}, ..etc
            string, Dictionary<string, string>/*
                mod name and mod full filepath -- or maybe from the root of the sdcard idk what i decided yet
            */
            > getAllMods(string want = ""){// all of ts looks so ugly
            Dictionary<string, Dictionary<string, string>> returnValue = new();/*
                just realized after chaing it so i can pick whichever i want 
                that i can structure this better and use a single function to do all the repeating code but im not gonna!
            */
            if (want == "" || want.Contains('a') )
            {
                DirectoryInfo[] activeMods = ultimateMods.GetDirectories();
                Dictionary<string, string> emods = new();
                foreach (DirectoryInfo dir in activeMods) { emods.Add(dir.Name, dir.FullName); }
                returnValue.Add("emods", emods);
            }
            if (want == "" || want.Contains('b') )
            {
                FileInfo[] activePlugins = skylinePlugins.GetFiles();
                Dictionary<string, string> eplugins = new();
                foreach (FileInfo dir in activePlugins) { eplugins.Add(dir.Name, dir.FullName); }
                returnValue.Add("eplugins", eplugins);
            }
            if (want == "" || want.Contains('c') )
            {
                DirectoryInfo[] inactiveMods = new DirectoryInfo(rootDisabled.FullName + @"\mods").GetDirectories();
                Dictionary<string, string> imods = new();
                foreach (DirectoryInfo dir in inactiveMods) { imods.Add(dir.Name, dir.FullName); }
                returnValue.Add("imods", imods);
            }
            if (want == "" || want.Contains('d') ) 
            {
                FileInfo[] inactivePlugins = new DirectoryInfo(rootDisabled.FullName + @"\nro").GetFiles();
                Dictionary<string, string> iplugins = new();
                foreach (FileInfo dir in inactivePlugins) { iplugins.Add(dir.Name, dir.FullName); }
                returnValue.Add("iplugins", iplugins);
            }
            return returnValue;
        }
        public bool modState(string modpath)// just checks if file/dir path is in the disabled folder basicly a isDisabled variable
        {
            if (modpath == null || string.IsNullOrEmpty(modpath))
            {
                return false;
            }
            bool isFile = modpath.EndsWith(".nro");
            if (!isFile) {
                DirectoryInfo modfold = new DirectoryInfo(modpath);
                string modfoldN = modfold.Name;
                if (Directory.Exists(rootDisabled.FullName + @"\mods\" + modfoldN) && Directory.Exists(ultimateMods.FullName + @"\" + modfoldN))
                {
                    DirectoryInfo modfold0 = new DirectoryInfo(rootDisabled.FullName + @"\mods\" + modfoldN);
                    DirectoryInfo modfold1 = new DirectoryInfo(ultimateMods.FullName + @"\" + modfoldN);
                    long modDis = modfold0.CreationTime.Ticks;
                    long modEn = modfold1.CreationTime.Ticks;
                    if ((modEn - modDis) > 0)
                    {
                        modfold0.Delete(true);
                    }
                    else
                    {
                        modfold1.Delete(true);
                    }
                } 
            } else
            {
                //later maybe
            }
            return modpath.Contains(rootDisabled.FullName);
        }
        public void Toggle(string _thingToMove)
        {
            // i couldnt use var cause of implicity of something why couldnt it be like js </3
            bool isDisabled = modState(_thingToMove);
            bool isFile = _thingToMove.EndsWith(".nro");// terrible but honestly easier
            if (isFile)
            {
                FileInfo itemFile = new FileInfo(_thingToMove);
                try{
                    if (isDisabled)
                    {
                        File.Move(_thingToMove, skylinePlugins.FullName + "\\" + itemFile.Name, true);
                    }
                    else
                    {
                        File.Move(_thingToMove, rootDisabled.FullName + @"\nro\" + itemFile.Name, true);
                    }
                } catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            } else
            {
                try{
                    DirectoryInfo itemDir = new DirectoryInfo(_thingToMove);
                    if (isDisabled)
                    {
                        Directory.Move(_thingToMove, ultimateMods.FullName + "\\" + itemDir.Name);
                    }
                    else
                    {
                        Directory.Move(_thingToMove, rootDisabled.FullName + @"\mods\" + itemDir.Name);
                    }
                } catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }
        private string getZip(string name)// took ts(this shit) from the microsoft docs about ZipFile
        {
            string path = @".\" + name + ".zip";

            using (OpenFileDialog gay = new OpenFileDialog())
            {
                gay.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
                gay.Filter = name+"latest zip Archive |*.zip";

                if (gay.ShowDialog() == DialogResult.OK)
                {
                    path = gay.FileName;
                }
            }
            return path;
        }
        private bool checkFileInZip(string zipPath,string[] names)//practically to check if the zipfile is worth it kindof a hardcoded way to check if its the reall ARCropolis or skyline but what works works
        {
            bool returnValue = false;
            using (ZipArchive zip = ZipFile.OpenRead(zipPath))
            {
                int found = 0;
                foreach (ZipArchiveEntry probablyafile in zip.Entries)
                {
                    foreach (string fileName in names)
                    {
                        if (probablyafile.Name.Contains(fileName))
                        {
                            found++;
                            break;
                        }
                    }
                }
                if (found == names.Length)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public void addArc()
        {
            string thepath = getZip("ARCropolis");
            FileInfo israel = new(thepath);
            if (!israel.Exists)
            {
                return;
            }
            bool theOne = checkFileInZip(thepath, ["libarcropolis.nro"]);
            if (!theOne)
            {
                return;
            }
            ZipFile.ExtractToDirectory(thepath, ryuSDCard.FullName, true);
        }
        public void addSkyline()
        {
            string thepath = getZip("Skyline");
            FileInfo israel = new(thepath);
            if (!israel.Exists)
            {
                return;
            }
            bool theOne = checkFileInZip(thepath, ["main.npdm", "subsdk9"]);// for all i care they could rebuild it for another game and i wouldnt know!
            if (!theOne)
            {
                return;
            }
            ZipFile.ExtractToDirectory(thepath, smashAtmos.FullName, true);
        }
        public object infoToml(string modPath, int mode = 0,
                string displayName = "Mod name goes here", 
                string own = "author(s) go here", 
                string ver = "1.0", 
                string desc = "Description for mod goes here",
                string cat = "Misc",
                string src = "https://gamebanana.com/games/6498")/*
                category = "Fighter, Stage, Effects, UI, Param, Audio, Misc"
            */
        {
            FileInfo toml = new FileInfo(modPath + @"\info.toml");
            if (!toml.Exists && mode == 0) {
                return "Doesnt exist";
            }
            if (mode == 0) //just get from the file
            {
                ///toml.
                string wT = File.ReadAllText(toml.FullName);
                string[] probablyvars = File.ReadAllLines(toml.FullName);
                //
                Dictionary<string, string> test = new();
                string descr = "";
                foreach (var s in probablyvars)
                {
                    var f = s.Split(" = ");
                    if (f.Length == 2 && f[0] != "description")
                    {
                        string key = f[0];
                        string value = f[1].Replace("\"", "");
                        if (key == "version")
                        {
                            key = "ver";
                        }else if(key == "category")
                        {
                            key = "cat";
                        }else if(key == "authors")
                        {
                            key = "own";
                        }
                        else if (key == "display_name")
                        {
                            key = "name";
                        }
                        else if (key == "source")
                        {
                            key = "src";
                        }
                        test.Add(key, value);
                    }
                }
                var startdesc = wT.IndexOf("description = \"\"\"");
                var enddesc = wT.LastIndexOf("\"\"\"");
                descr = wT.Substring(startdesc, (enddesc - startdesc)).Replace("description = \"\"\"", "").Replace("\"\"\"", "").TrimStart().TrimEnd();
                test.Add("desc", descr);

                return test;
            }
            else if (mode == 1)//overwrite to the file or create it
            {
                StreamWriter wa = File.CreateText(toml.FullName);
                wa.WriteLine($"display_name = \"{displayName}\"");
                wa.WriteLine($"authors = \"{own}\"");
                wa.WriteLine($"version = \"{ver}\"");
                wa.WriteLine($"description = \"\"\"{desc}\"\"\"");
                wa.WriteLine($"category = \"{cat}\"");
                //wa.WriteLine($"source = \"{src}\"");// mod download source or site whateva
                wa.Close();
            }
            return "No valid modes selected";
        }
        public object CheckConflicts(int modes = 0)//checks for recent conflicts and adds them
        /*
         possible you could probably run through the mod folders files and see/search4 the layout.arc or stuff liwe that and see if another mod also has that but idk and im unsure so ill just do this*/
        {
            FileInfo conflictsfilejson = new(ryuSDCardUltimate + @"\arcropolis\conflicts.json");
            if (modes == 0)
            {
                if (!conflictsfilejson.Exists)
                {
                    return $"Conflict file doesnt exist in {ryuSDCardUltimate + @"\arcropolis\conflicts.json"}";
                }
                Dictionary<string, string[]> conflictsjson =
                    JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                            File.ReadAllText(conflictsfilejson.FullName)
                        );
                if (conflictsjson != null)
                {
                    Dictionary<string, string[]> ModChanges =
                        JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                                File.ReadAllText(rootDisabled.FullName + @"\ModChanges.json")
                            );
                    foreach (var item in conflictsjson)
                    {
                        string problemAt = item.Key;
                        string[] hatred = item.Value;
                        foreach (string Modpath in hatred)
                        {
                            string modFolderName = Modpath.Split("/mods/")[1];
                            List<string> maybenew = new();
                            bool problemExists = false;
                            if (ModChanges.ContainsKey(modFolderName))
                            {
                                foreach (var modUsesPath in ModChanges[modFolderName])
                                {
                                    maybenew.Add(modUsesPath);
                                    if (problemAt == modUsesPath)
                                    {
                                        problemExists = true;
                                    }
                                }
                            }
                            if (!problemExists)
                            {
                                maybenew.Add(problemAt);
                            }
                            if (!ModChanges.ContainsKey(modFolderName))
                            {
                                ModChanges.Add(modFolderName, maybenew.ToArray());
                            }
                            else
                            {
                                ModChanges[modFolderName] = maybenew.ToArray();
                            }
                        }
                        /*notes
                         * 
                         * after this it adds to a dict where it has the mod as key and the conflict in side an array as value
                         * and should then save to a file to remembeer which changes which
                         * and when a mod is enabled it checks the dict for mods that are enabled
                         * and cross refferences things that a mod changes with the other
                         * but the changes is only known when the mistake is done once
                         * 
                         * something like that
                         */
                    }
                    File.WriteAllText(rootDisabled.FullName + @"\ModChanges.json",
                            JsonSerializer.Serialize(ModChanges)
                        );
                }
            }
            else if(modes == 1)
            {
                Dictionary<string, string[]> ModChanges =
                        JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                                File.ReadAllText(rootDisabled.FullName + @"\ModChanges.json")
                            );
                if(!(ModChanges.Count > 0)) 
                {
                    return "Valid mode but empty no conflicts to match";
                }
                DirectoryInfo[] activeMods = ultimateMods.GetDirectories();
                List<string> modsFound = new List<string>();
                List<string> allModsFound = new List<string>();
                List < Dictionary<string, string> > Problems = new List<Dictionary<string, string>>();
                foreach (var mod in activeMods)
                {
                    allModsFound.Add(mod.Name);
                    if (ModChanges.ContainsKey(mod.Name))
                    {
                        modsFound.Add(mod.Name);
                    }
                }
                //{hatedmod:"${hatedmod}",reason:"both mods change ${path}",offendedmod:"${mod}"}
                foreach (string mod in modsFound)
                {
                    string[] modpaths_A = ModChanges[mod];
                    foreach (string path_A in modpaths_A)
                    {
                        foreach (var hatingmods in ModChanges)
                        {
                            string hatedMod = hatingmods.Key;
                            if (mod == hatedMod) continue; // lowkey forgot about being able to do this now im gonna spam checks like this everywhere :osakastare:(the smile stare yk)
                            string[] modpaths_B = hatingmods.Value;
                            foreach (string path_B in modpaths_B)
                            {
                                if (path_B != path_A) continue;
                                if (!allModsFound.Contains(hatedMod)) continue;
                                Dictionary<string,string> conclusion = new();
                                conclusion.Add("mod", mod);
                                conclusion.Add("hated", hatedMod);
                                conclusion.Add("path", path_A);
                                conclusion.Add("reason", $"Both {mod} and {hatedMod} modify the same path: {path_A}");
                                Problems.Add(conclusion);
                            }
                        }
                    }
                }
                return Problems;
                /*notes 3:49AM 11/16/2025
                 *
                 * Should either handle everything on this side by checking in the active folder see if mods that are in the active folder exists in the list
                 * then should check what they change and if they change the same thing it should return a table with mods(string[]) and reason(string arc paththing) index
                 * {problem:["mod1","mod2"],reason:"modifies path blahblahblah"}
                 * something like that
                 *
                 */
            }
            return "Invalid mode";
        }
    }
}
