﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;
using SharpDX;
using SharpDX.Direct3D9;
using Color = SharpDX.Color;
using Font = SharpDX.Direct3D9.Font;
using Menu = Ensage.Common.Menu.Menu;
using MenuItem = Ensage.Common.Menu.MenuItem;

namespace OverlayInformation
{
    internal static class Program
    {
        private static void Main()
        {
            /*Entity.OnParticleEffectAdded += (entity, eventArgs) =>
            {
                var partName = eventArgs.Name;
                var name = entity.Name;
                if (!partName.Contains("teleport"))
                    return;

                Printer.Print(entity.Name +": "+ eventArgs.Name);
            };*/
            Members.Menu.AddItem(new MenuItem("Enable", "Enable").SetValue(true));
            var topPanel = new Menu("Top Panel", "toppanel");
            var spellPanel = new Menu("Spell Panel", "spellPanel");
            var ultimate = new Menu("Ultimate", "ultimate");
            var health = new Menu("Health Panel", "health");
            var mana = new Menu("Mana Panel", "mana");
            var status = new Menu("Status panel", "status");
            var extraPos = new Menu("Extra Position", "extraPos");
            var itemPanel = new Menu("Item panel", "itempanel");

            var roshanTimer = new Menu("Roshan Timer", "roshanTimer");
            var showMeMore = new Menu("Show Me More", "showmemore");
            var showIllusion = new Menu("Show Illusion", "showillusion");
            var runevision = new Menu("Rune Vision", "runevision");

            var autoItems = new Menu("Auto Items", "autoitems");
            var settings = new Menu("Settings", "Settings");
            var page1 = new Menu("Page 1", "Page 1");
            var page2 = new Menu("Page 2", "Page 2");
            //===========================
            itemPanel.AddItem(new MenuItem("itempanel.Enable", "Enable").SetValue(true));
            itemPanel.AddItem(new MenuItem("itempanel.Stash.Enable", "Draw Stash Items").SetValue(true));
            itemPanel.AddItem(new MenuItem("itempanel.Button.Enable", "Draw Button for toggle").SetValue(true));
            itemPanel.AddItem(new MenuItem("itempanel.X", "Panel Position X").SetValue(new Slider(100, 0, 2000)));
            itemPanel.AddItem(new MenuItem("itempanel.Y", "Panel Position Y").SetValue(new Slider(200, 0, 2000)));
            itemPanel.AddItem(new MenuItem("itempanel.SizeX", "SizeX").SetValue(new Slider(255, 1, 255)));
            itemPanel.AddItem(new MenuItem("itempanel.SizeY", "SizeY").SetValue(new Slider(174, 1, 255)));
            itemPanel.AddItem(new MenuItem("itempanel.Red", "Red").SetValue(new Slider(141, 0, 255)).SetFontColor(Color.Red));
            itemPanel.AddItem(new MenuItem("itempanel.Green", "Green").SetValue(new Slider(182, 0, 255)).SetFontColor(Color.Green));
            itemPanel.AddItem(new MenuItem("itempanel.Blue", "Blue").SetValue(new Slider(98, 0, 255)).SetFontColor(Color.Blue));
            //===========================
            topPanel.AddItem(new MenuItem("toppanel.Enable", "Enable").SetValue(true));
            topPanel.AddItem(
                new MenuItem("toppanel.Targets", "Draw For: ").SetValue(
                    new StringList(new[] { "Both", "Ally Team", "Enemy Team" })));
            //===========================
            spellPanel.AddItem(new MenuItem("spellpanel.Enable", "Enable").SetValue(true));
            spellPanel.AddItem(
                new MenuItem("spellpanel.Targets", "Draw For: ").SetValue(
                    new StringList(new[] { "Both", "Ally Team", "Enemy Team" })));
            var oldMethod = new Menu("OldMethod", "Without Textures");
            oldMethod.AddItem(new MenuItem("spellpanel.OldMethod.Enable", "Enable").SetValue(true));
            oldMethod.AddItem(new MenuItem("spellPanel.distBetweenSpells", "Distance spells").SetValue(new Slider(36, 0, 200)));
            oldMethod.AddItem(new MenuItem("spellPanel.DistBwtweenLvls", "Distance lvls").SetValue(new Slider(6, 0, 200)));
            oldMethod.AddItem(new MenuItem("spellPanel.SizeSpell", "Level size").SetValue(new Slider(3, 1, 25)));
            oldMethod.AddItem(new MenuItem("spellPanel.ExtraPosX", "Extra Position X").SetValue(new Slider(25)));
            oldMethod.AddItem(new MenuItem("spellPanel.ExtraPosY", "Extra Position Y").SetValue(new Slider(125, 0, 400)));
            //---0-0-0-0-0-
            var newMethod = new Menu("New Method", "With Textures");
            newMethod.AddItem(new MenuItem("spellpanel.NewMethod.Enable", "Enable").SetValue(false));
            newMethod.AddItem(new MenuItem("spellpanel.NewMethod.IconSize", "Icon Size").SetValue(new Slider(25, 1, 100)));
            newMethod.AddItem(new MenuItem("spellpanel.NewMethod.SizeLevel", "Text Size (for level)").SetValue(new Slider(50, 0, 150)));
            newMethod.AddItem(new MenuItem("spellpanel.NewMethod.Size", "Text Size (for cooldown/mana)").SetValue(new Slider(50, 0, 150)));
            newMethod.AddItem(new MenuItem("spellpanel.NewMethod.ExtraX", "Extra position X").SetValue(new Slider(0, -150, 150)));
            newMethod.AddItem(new MenuItem("spellpanel.NewMethod.ExtraY", "Extra position Y").SetValue(new Slider(0, -150, 150)));
            //===========================
            ultimate.AddItem(new MenuItem("ultimate.Enable", "Enable").SetValue(true));
            ultimate.AddItem(new MenuItem("ultimate.Icon.Enable", "Draw Icon").SetValue(true));
            ultimate.AddItem(
                new MenuItem("ultimate.Icon.Extra.Enable", "Draw Mana req with cd").SetValue(true)
                    .SetTooltip("render lack of mana when spell on cooldown"));
            ultimate.AddItem(
                new MenuItem("ultimate.Type", "Type of drawing").SetValue(
                    new StringList(new[] { "Draw Icon", "Draw Line" }))).ValueChanged += (sender, args) =>
                    {
                        var newArg = args.GetNewValue<StringList>().SelectedIndex;
                        var newColor = newArg == 1 ? Color.DarkSlateGray : new Color(195, 186, 173, 255);
                        var newColor2 = newArg == 0 ? Color.DarkSlateGray : new Color(195, 186, 173, 255);
                        Members.Menu.Item("ultimate.Info").SetFontColor(newColor);
                        Members.Menu.Item("ultimate.InfoAlways").SetFontColor(newColor);
                        Members.Menu.Item("ultimate.Line.Size").SetFontColor(newColor2);
                    };
            ultimate.AddItem(
                new MenuItem("ultimate.Info", "Show details").SetValue(true)
                    .SetTooltip("show Ultimate's CD if u put ur mouse on icon"));
            ultimate.AddItem(
                new MenuItem("ultimate.InfoAlways", "Show details all time").SetValue(true)
                    .SetTooltip("Show Details should be enabled"));
            ultimate.AddItem(
                new MenuItem("ultimate.Line.Size", "Line Size").SetValue(new Slider(15, 7, 30)));

            //===========================
            health.AddItem(new MenuItem("toppanel.Health.Enable", "Enable").SetValue(true));
            //===========================
            mana.AddItem(new MenuItem("toppanel.Mana.Enable", "Enable").SetValue(true));
            //===========================
            status.AddItem(new MenuItem("toppanel.Status.Enable", "Enable").SetValue(true));
            status.AddItem(new MenuItem("toppanel.AllyVision.Enable", "Vision on Ally Heroes").SetValue(true));
            status.AddItem(new MenuItem("toppanel.EnemiesStatus.Enable", "Enemies status").SetValue(true));
            //===========================
            extraPos.AddItem(
                new MenuItem("extraPos.X", "Extra Position for top panel: X").SetValue(new Slider(0, -25, 25)));
            extraPos.AddItem(
                new MenuItem("extraPos.Y", "Extra Position for top panel: Y").SetValue(new Slider(0, -25, 25)));
            //===========================
            roshanTimer.AddItem(new MenuItem("roshanTimer.Enable", "Enable").SetValue(true));
            //===========================
            showMeMore.AddItem(new MenuItem("showmemore.Enable", "Enable").SetValue(true));
            var charge = new Menu("", "charge", false, "spirit_breaker_charge_of_darkness", true);
            charge.AddItem(new MenuItem("tooltip", "When Charge on your Main Hero").SetFontColor(Color.Red));
            charge.AddItem(new MenuItem("charge.Enable", "Enable").SetValue(true));
            charge.AddItem(new MenuItem("charge.Rect.Enable", "Draw red box").SetValue(false));
            charge.AddItem(new MenuItem("charge.Red", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            charge.AddItem(new MenuItem("charge.Green", "Green").SetValue(new Slider(0, 0, 255)).SetFontColor( Color.Green));
            charge.AddItem(new MenuItem("charge.Blue", "Blue").SetValue(new Slider(0, 0, 255)).SetFontColor(Color.Blue));
            charge.AddItem(new MenuItem("charge.Alpha", "Alpha").SetValue(new Slider(4, 0, 255)).SetFontColor( Color.WhiteSmoke));
            //===========================
            var blur = new Menu("", "blur", false, "phantom_assassin_blur", true);
            blur.AddItem(new MenuItem("blur.Enable", "Show PA on minimap").SetValue(true));
            //===========================
            var wr = new Menu("", "wr", false, "windrunner_powershot", true);
            wr.AddItem(new MenuItem("wr.Enable", "Enable").SetValue(true));
            wr.AddItem(new MenuItem("wr.Red", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            wr.AddItem(new MenuItem("wr.Green", "Green").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Green));
            wr.AddItem(new MenuItem("wr.Blue", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue));
            //===========================
            var mirana = new Menu("", "mirana", false, "mirana_arrow", true);
            mirana.AddItem(new MenuItem("mirana.Enable", "Enable").SetValue(true));
            mirana.AddItem(new MenuItem("mirana.Red", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            mirana.AddItem(new MenuItem("mirana.Green", "Green").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Green));
            mirana.AddItem(new MenuItem("mirana.Blue", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue));
            var apparition = new Menu("", "apparition", false, "ancient_apparition_ice_blast", true);
            apparition.AddItem(new MenuItem("apparition.Enable", "Enable").SetValue(true));
            var lina = new Menu("", "lina", false, "lina_light_strike_array", true);
            lina.AddItem(new MenuItem("lina.Enable", "Enable").SetValue(true));
            var invoker = new Menu("", "invoker", false, "invoker_sun_strike", true);
            invoker.AddItem(new MenuItem("invoker.Enable", "Enable").SetValue(true));
            invoker.AddItem(new MenuItem("invoker.Red", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            invoker.AddItem(new MenuItem("invoker.Green", "Green").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Green));
            invoker.AddItem(new MenuItem("invoker.Blue", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue));
            var lesh = new Menu("", "lesh", false, "leshrac_split_earth", true);
            lesh.AddItem(new MenuItem("lesh.Enable", "Enable").SetValue(true));
            var kunkka = new Menu("", "kunkka", false, "kunkka_torrent", true);
            kunkka.AddItem(new MenuItem("kunkka.Enable", "Enable").SetValue(true));
            kunkka.AddItem(new MenuItem("kunkka.Red", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            kunkka.AddItem(new MenuItem("kunkka.Green", "Green").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Green));
            kunkka.AddItem(new MenuItem("kunkka.Blue", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue));
            var tech = new Menu("", "tech", false, "npc_dota_hero_techies", true);
            tech.AddItem(new MenuItem("tech.Enable", "Enable").SetValue(true));
            var tinker = new Menu("", "tinker", false, "npc_dota_hero_tinker", true);
            tinker.AddItem(new MenuItem("tinker.Enable", "Enable").SetValue(true));
            var lifestealer = new Menu("", "life stealer", false, "life_stealer_infest", true);
            lifestealer.AddItem(new MenuItem("lifestealer.Enable", "Enable").SetValue(true));
            lifestealer.AddItem(new MenuItem("lifestealer.Icon.Enable", "Draw icon near hero").SetValue(false));
            lifestealer.AddItem(new MenuItem("lifestealer.creeps.Enable", "Draw icon near creep").SetValue(false));
            var arc = new Menu("", "arc",textureName: "arc_warden_spark_wraith",showTextWithTexture:true);
            arc.AddItem(new MenuItem("arc.Enable", "Enable").SetValue(true));
            var scan = new Menu("Enemy Scanning Ability", "Scan");
            scan.AddItem(new MenuItem("scan.Enable", "Enable").SetValue(true));
            var courEsp = new Menu("Courier on Minimap", "Cour");
            courEsp.AddItem(new MenuItem("Cour.Enable", "Enable").SetValue(true));
            //var cour = new Menu("Courier", "Courier");
            //cour.AddItem(new MenuItem("Courier.Enable", "Enable").SetValue(true)).SetTooltip("draw courier position on minimap");
            //===========================
            showIllusion.AddItem(new MenuItem("showillusion.Enable", "Enable").SetValue(true));
            showIllusion.AddItem(
                new MenuItem("showillusion.Type", "Type").SetValue(new StringList(new[] {"Smoke", "new 1" , "new 2" , "balloons" }, 2)));
            showIllusion.AddItem(new MenuItem("showillusion.X", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            showIllusion.AddItem(new MenuItem("showillusion.Y", "Green").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Green));
            showIllusion.AddItem(new MenuItem("showillusion.Z", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue));
            showIllusion.AddItem(new MenuItem("showillusion.Alpha", "Alpha").SetValue(new Slider(40,0,255)));
            showIllusion.AddItem(new MenuItem("showillusion.Size", "Size").SetValue(new Slider(120,1,250)));
            //===========================
            runevision.AddItem(new MenuItem("runevision.Enable", "Enable").SetValue(true));
            runevision.AddItem(new MenuItem("runevision.PrintText.Enable", "Print text on rune-spawning").SetValue(true));
            runevision.AddItem(new MenuItem("runevision.DrawOnMinimap.Enable", "Draw rune on minimap").SetValue(true));
            //===========================
            var itemOverlay = new Menu("Item overlay", "itemOverlay");
            itemOverlay.AddItem(new MenuItem("itemOverlay.Enable", "Enable").SetValue(false)).SetTooltip("will show all items on heroes");
            itemOverlay.AddItem(new MenuItem("itemOverlay.DrawCharges", "Draw Charges").SetValue(true));
            itemOverlay.AddItem(new MenuItem("itemOverlay.Size", "Size").SetValue(new Slider(100,1,200)));
            itemOverlay.AddItem(new MenuItem("itemOverlay.Extra", "Extra").SetValue(new Slider(26, 1, 100)));
            itemOverlay.AddItem(new MenuItem("itemOverlay.Ally", "Enable for ally").SetValue(true));
            itemOverlay.AddItem(new MenuItem("itemOverlay.Enemy", "Enable for enemy").SetValue(true));
            itemOverlay.AddItem(new MenuItem("itemOverlay.Cour", "Enable for couriers").SetValue(true)).SetTooltip("only for enemy");
            var tpCatcher = new Menu("TP Catcher", "TpCather");
            tpCatcher.AddItem(new MenuItem("TpCather.Enable", "Enable").SetValue(true));
            tpCatcher.AddItem(new MenuItem("TpCather.Ally", "For Ally").SetValue(true));
            tpCatcher.AddItem(new MenuItem("TpCather.Enemy", "For Enemy").SetValue(true));
            tpCatcher.AddItem(new MenuItem("TpCather.Map", "Draw on Map").SetValue(true));
            tpCatcher.AddItem(new MenuItem("TpCather.MiniMap", "Draw on MiniMap").SetValue(true));
            tpCatcher.AddItem(
                new MenuItem("TpCather.MiniMap.Type", "Draw on MiniMap Hero Icon or Rectangle").SetValue(true))
                .SetTooltip("true=icon; false=rectangle");
            tpCatcher.AddItem(new MenuItem("TpCather.MiniMap.Size", "MiniMap Size").SetValue(new Slider(20,5,30)));
            tpCatcher.AddItem(new MenuItem("TpCather.Map.Size", "Map Size").SetValue(new Slider(50,5,50)));
            var manaBars = new Menu("Manabars", "manaBars");
            manaBars.AddItem(new MenuItem("manaBars.Enable", "Enable").SetValue(true));
            manaBars.AddItem(new MenuItem("manaBars.Nums.Enable", "Enable digital values").SetValue(true));
            manaBars.AddItem(new MenuItem("manaBars.Nums.Size", "Dig Size").SetValue(new Slider(75, 1, 150)));
            manaBars.AddItem(new MenuItem("manaBars.Size", "Size").SetValue(new Slider(75, 1, 150)));
            
            manaBars.AddItem(new MenuItem("manaBars.Red", "Red").SetValue(new Slider(65, 0, 255)).SetFontColor(Color.Red));
            manaBars.AddItem(new MenuItem("manaBars.Green", "Green").SetValue(new Slider(105, 0, 255)).SetFontColor(Color.Green));
            manaBars.AddItem(new MenuItem("manaBars.Blue", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue));

            //var dangItem = new Menu("Dangerous items", "dangitems");
            itemOverlay.AddItem(new MenuItem("itemOverlay.DangItems", "Draw only dangerous items").SetValue(false)).SetTooltip("show if enemy has Dangerous items. Working only for enemy heroes");
            itemOverlay.AddItem(new MenuItem("itemOverlay.OldMethod", "Use old method for drawing dangItems").SetValue(false));
            
            var dict = new Dictionary<string, bool>
            {
                {"item_gem", true},
                {"item_dust", true},
                {"item_sphere", true},
                {"item_blink", true},
                {"item_ward_observer", true},
                {"item_ward_sentry", true},
                {"item_black_king_bar", true},
                {"item_invis_sword", true},
                {"item_silver_edge", true},
                {"item_ward_dispenser", true}
            };
            itemOverlay.AddItem(new MenuItem("itemOverlay.List", "Items: ").SetValue(new AbilityToggler(dict)));
            //===========================
            autoItems.AddItem(new MenuItem("autoitems.Enable", "Enable").SetValue(true));
            autoItems.AddItem(new MenuItem("autoItems.Percent", "Health(%) for auto stick").SetValue(new Slider(15)));
            autoItems.AddItem(new MenuItem("autoItems.Percent2", "Mana(%) for auto stick").SetValue(new Slider(15)));
            var autoitemlist = new Dictionary<string, bool>
            {
                {"item_magic_wand", true},
                {"item_phase_boots", true},
                {"item_hand_of_midas", true}
            };
            autoItems.AddItem(new MenuItem("autoitems.List", "Items:").SetValue(new AbilityToggler(autoitemlist)));
            //===========================
            var lastPosition = new Menu("Last position", "lastPosition");
            lastPosition.AddItem(new MenuItem("lastPosition.Enable", "Enable").SetValue(true)).SetTooltip("show last positions of enemies");
            lastPosition.AddItem(new MenuItem("lastPosition.Enable.Prediction", "Enable Prediction").SetValue(true));
            lastPosition.AddItem(new MenuItem("lastPosition.Enable.Map", "on Map").SetValue(false));
            lastPosition.AddItem(new MenuItem("lastPosition.Map.X", "icon size").SetValue(new Slider(50,10,150)));
            lastPosition.AddItem(new MenuItem("lastPosition.Enable.Minimap", "on Minimap").SetValue(true));
            lastPosition.AddItem(new MenuItem("lastPosition.Minimap.X", "icon size").SetValue(new Slider(20, 10, 150)));

            //===========================
            var netWorth = new Menu("NetWorth Graph", "netWorth");
            netWorth.AddItem(new MenuItem("netWorth.Enable", "Enable").SetValue(true)).SetTooltip("draw networth graph based on item cost");
            netWorth.AddItem(new MenuItem("netWorth.Order", "Sort Players").SetValue(true));
            netWorth.AddItem(
                new MenuItem("netWorth.X", "Position: X").SetValue(new Slider(0, 0, 2000)));
            netWorth.AddItem(
                new MenuItem("netWorth.Y", "Position: Y").SetValue(new Slider(0, 0, 2000)));
            netWorth.AddItem(new MenuItem("netWorth.SizeX", "SizeX").SetValue(new Slider(255, 1, 255)));
            netWorth.AddItem(new MenuItem("netWorth.SizeY", "SizeY").SetValue(new Slider(174, 1, 255)));
            netWorth.AddItem(new MenuItem("netWorth.Red", "Red").SetValue(new Slider(141, 0, 255)).SetFontColor(Color.Red));
            netWorth.AddItem(new MenuItem("netWorth.Green", "Green").SetValue(new Slider(182, 0, 255)).SetFontColor(Color.Green));
            netWorth.AddItem(new MenuItem("netWorth.Blue", "Blue").SetValue(new Slider(98, 0, 255)).SetFontColor(Color.Blue));
            //===========================
            var dmgCalc = new Menu("Damage Calculation", "dmgCalc");
            dmgCalc.AddItem(new MenuItem("dmgCalc.Enable", "Enable").SetValue(true)).SetTooltip("showing dmg from ur abilities");
            dmgCalc.AddItem(new MenuItem("dmgCalc.Red", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            dmgCalc.AddItem(new MenuItem("dmgCalc.Green", "Green").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Green));
            dmgCalc.AddItem(new MenuItem("dmgCalc.Blue", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue));
            //===========================
            var devolper = new Menu("Developer", "Developer");
            devolper.AddItem(new MenuItem("Dev.Hax.enable", "Hax in lobby").SetValue(false));
            devolper.AddItem(new MenuItem("Dev.Text.enable", "Debug messages").SetValue(false));
            devolper.AddItem(new MenuItem("Dev.CreepsDisabler.enable", "Disable spawn creeps").SetValue(false))
                .ValueChanged +=
                (sender, args) =>
                {
                    var type = args.GetNewValue<bool>() ? "enable" : "disable";
                    Game.ExecuteCommand($"dota_creeps_no_spawning_{type}");
                };
            //===========================
            spellPanel.AddSubMenu(oldMethod);
            spellPanel.AddSubMenu(newMethod);
            topPanel.AddSubMenu(ultimate);
            topPanel.AddSubMenu(health);
            topPanel.AddSubMenu(mana);
            topPanel.AddSubMenu(status);
            topPanel.AddSubMenu(extraPos);
            page1.AddSubMenu(topPanel);
            page1.AddSubMenu(spellPanel);
            page1.AddSubMenu(roshanTimer);
            page1.AddSubMenu(showMeMore);
            showMeMore.AddSubMenu(charge);
            showMeMore.AddSubMenu(blur);
            showMeMore.AddSubMenu(wr);
            showMeMore.AddSubMenu(mirana);
            showMeMore.AddSubMenu(apparition);
            showMeMore.AddSubMenu(lina);
            showMeMore.AddSubMenu(invoker);
            showMeMore.AddSubMenu(kunkka);
            showMeMore.AddSubMenu(lesh);
            showMeMore.AddSubMenu(lifestealer);
            showMeMore.AddSubMenu(tech);
            showMeMore.AddSubMenu(tinker);
            showMeMore.AddSubMenu(arc);
            showMeMore.AddSubMenu(scan);
            showMeMore.AddSubMenu(courEsp);
            page1.AddSubMenu(showIllusion);
            page1.AddSubMenu(runevision);
            //settings.AddSubMenu(dangItem);
            settings.AddSubMenu(page1);
            settings.AddSubMenu(page2);
            page1.AddSubMenu(itemPanel);
            page1.AddSubMenu(itemOverlay);
            page2.AddSubMenu(manaBars);
            page2.AddSubMenu(autoItems);
            page2.AddSubMenu(lastPosition);
            page2.AddSubMenu(netWorth);
            page2.AddSubMenu(dmgCalc);
            page2.AddSubMenu(tpCatcher);

            Members.Menu.AddSubMenu(settings);
            Members.Menu.AddSubMenu(devolper);

            Members.HeroesList = new HeroesList();
            Members.Manabars = new Manabars();
            Members.ItemOverlay = new ItemOverlay();
            Members.DamageCalculation = new DamageCalculation();
            Members.AbilityOverlay = new AbilityOverlay();

            if (Drawing.Direct3DDevice9 != null)
                Members.RoshanFont = new Font(
                    Drawing.Direct3DDevice9,
                    new FontDescription
                    {
                        FaceName = "Tahoma",
                        Height = 15,
                        OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default
                    });
            Events.OnLoad += (sender, args) =>
            {
                Members.MyHero = ObjectManager.LocalHero;
                Members.MyPlayer = ObjectManager.LocalPlayer;
                
                Members.AbilityDictionary = new Dictionary<uint, List<Ability>>();
                Members.ItemDictionary = new Dictionary<uint, List<Item>>();
                Members.StashItemDictionary = new Dictionary<uint, List<Item>>();
                Members.NetWorthDictionary = new Dictionary<string, long>();

                Members.Heroes = new List<Hero>();
                Members.AllyHeroes = new List<Hero>();
                Members.EnemyHeroes = new List<Hero>();
                Members.Players = new List<Player>();
                Members.AllyPlayers = new List<Player>();
                Members.EnemyPlayers = new List<Player>();
                Members.BaseList = new List<Unit>();

                Members.PAisHere = null;
                Members.BaraIsHere = false;
                Members.Apparition = false;
                Members.Mirana = null;
                Members.Windrunner = null;
                Updater.HeroList.Flush();
                Updater.BaseList.Flush();
                Updater.PlayerList.Flush();
                Game.OnUpdate += Updater.HeroList.Update;
                //Game.OnUpdate += Updater.PlayerList.Update;
                Game.OnUpdate += Updater.BaseList.Update;
                Game.OnUpdate += Devolp.ConsoleCommands;
                RoshanAction.Flush();
                AutoItems.Flush();
                Game.OnUpdate += RoshanAction.Roshan;
                Game.OnUpdate += Game_OnUpdate;

                Drawing.OnDraw += DrawHelper.Overlay;

                Drawing.OnDraw += ItemPanel.Draw;
                ShowMeMore.Flush();
                Drawing.OnDraw += ShowMeMore.Draw;
                Entity.OnParticleEffectAdded += ShowMeMore.Entity_OnParticleEffectAdded;
                Unit.OnModifierAdded += ShowMeMore.OnModifierAdded;
                Unit.OnModifierRemoved += ShowMeMore.OnModifierRemoved;
                Runes.Flush();
                Drawing.OnDraw += Runes.Draw;
                

                Drawing.OnPreReset += DrawHelper.Render.Drawing_OnPreReset;
                Drawing.OnPostReset += DrawHelper.Render.Drawing_OnPostReset;
                Drawing.OnEndScene += DrawHelper.Render.Drawing_OnEndScene;
                Game.OnWndProc += Game_OnWndProc;
                AppDomain.CurrentDomain.DomainUnload += DrawHelper.Render.CurrentDomainDomainUnload;
                Game.OnFireEvent += FireEvent.Game_OnGameEvent;
                
                Game.PrintMessage(
                    "<font face='Comic Sans MS, cursive'><font color='#00aaff'>" + Members.Menu.DisplayName +
                    " By Jumpering" +
                    " loaded!</font> <font color='#aa0000'>v" + Assembly.GetExecutingAssembly().GetName().Version,
                    MessageType.LogMessage);
                Printer.PrintSuccess("> " + Members.Menu.DisplayName + " loaded v" +
                                     Assembly.GetExecutingAssembly().GetName().Version);

                /*Entity.OnParticleEffectAdded += Entity_OnParticleEffectAdded;
                Drawing.OnDraw += Drawing_OnDraw;*/
                try
                {
                    Members.Menu.AddToMainMenu();
                    if (Members.Menu.Item("Dev.CreepsDisabler.enable").GetValue<bool>())
                        Game.ExecuteCommand("dota_creeps_no_spawning_enable");
                }
                catch (Exception)
                {
                    Printer.Print("Members.Menu.AddToMainMenu();");
                }

            };
            Events.OnClose += (sender, args) =>
            {
                Game.OnUpdate -= Updater.HeroList.Update;
                //Game.OnUpdate += Updater.PlayerList.Update;
                Game.OnUpdate -= Updater.BaseList.Update;
                Game.OnUpdate -= Devolp.ConsoleCommands;
                Game.OnUpdate -= RoshanAction.Roshan;
                Game.OnUpdate -= Game_OnUpdate;

                Drawing.OnDraw -= DrawHelper.Overlay;
                Drawing.OnDraw -= ItemPanel.Draw;
                Drawing.OnDraw -= ShowMeMore.Draw;
                Entity.OnParticleEffectAdded -= ShowMeMore.Entity_OnParticleEffectAdded;
                Unit.OnModifierAdded -= ShowMeMore.OnModifierAdded;
                Unit.OnModifierRemoved -= ShowMeMore.OnModifierRemoved;
                Drawing.OnDraw -= Runes.Draw;
                Drawing.OnPreReset -= DrawHelper.Render.Drawing_OnPreReset;
                Drawing.OnPostReset -= DrawHelper.Render.Drawing_OnPostReset;
                Drawing.OnEndScene -= DrawHelper.Render.Drawing_OnEndScene;
                Game.OnWndProc -= Game_OnWndProc;
                AppDomain.CurrentDomain.DomainUnload -= DrawHelper.Render.CurrentDomainDomainUnload;
                Game.OnFireEvent -= FireEvent.Game_OnGameEvent;
                Members.TopPanelPostiion.Clear();
                Members.Heroes.Clear();
                Members.EnemyHeroes.Clear();
                Members.AllyHeroes.Clear();
                Printer.PrintInfo("> " + Members.Menu.DisplayName + " unloaded");
                try
                {
                    Members.Menu.RemoveFromMainMenu();
                }
                catch (Exception)
                {
                    Printer.PrintError("Members.Menu.RemoveFromMainMenu();");
                }
                
            };
        }

        private static List<Vector3> Points=new List<Vector3>();
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Game.IsKeyDown(9))
            {
                Points.Clear();
            }
            foreach (var p in Points)
            {
                var pos = Helper.WorldToMinimap(p);
                Drawing.DrawCircle(pos, 2, 50, Color.White);
                pos = Drawing.WorldToScreen(p);
                Drawing.DrawCircle(pos, 10, 50, Color.White);
            }
        }

        private static void Entity_OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            if (args.Name.Contains("ui_"))
                return;
            /*if (args.Name.Contains("base"))
                return;*/
            if (sender.Name.Contains("port"))
                return;
            /*if (sender.Name.Contains("sven"))
                Printer.PrintInfo($"{sender.Name}: {args.Name}");*/
            /*if (args.Name.Contains("flame"))
            {
                return;
            }
            if (args.Name.Contains("head_fire"))
            {
                return;
            }*/
            if (!args.ParticleEffect.Position.IsZero)
            Points.Add(args.ParticleEffect.Position);
            Printer.Print($"{sender.Name}: {args.Name}");
            //effect322 = new ParticleEffect("particles/units/heroes/hero_spirit_breaker/spirit_breaker_charge_target.vpcf", v, ParticleAttachment.OverheadFollow);
            //effect322[num++] = new ParticleEffect("particles/units/heroes/hero_spirit_breaker/spirit_breaker_charge_target.vpcf",args.ParticleEffect.Position);
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (!Members.Menu.Item("itempanel.Button.Enable").GetValue<bool>() /*|| args.WParam != 1*/ || Game.IsChatOpen)
            {
                return;
            }
            switch (args.Msg)
            {
                case (uint) Utils.WindowsMessages.WM_LBUTTONUP:
                    Members.IsClicked = false;
                    break;
                case (uint) Utils.WindowsMessages.WM_LBUTTONDOWN:
                    Members.IsClicked = true;
                    break;
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (!Checker.IsActive()) return;
            ShowMeMore.ShowIllustion();
            ShowMeMore.ShowMeMoreSpells();
            AutoItems.Action();
            Runes.Action();
        }
    }
}