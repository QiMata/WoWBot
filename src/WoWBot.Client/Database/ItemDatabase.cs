using System;
using System.IO;
using Microsoft.Data.Sqlite;
using robotManager.Helpful;
using WoWBot.Client.Models;

namespace WoWBot.Client.Database
{
    public class ItemDatabase
    {
        private static readonly string ConnectionString = "Data Source=database.db";
        private static readonly string ItemsSqlFile = @"all-items-filtered.sql";
        //public static ItemDatabase Instance = new ItemDatabase();

        //private ItemDatabase()
        //{
        //    using (var db = new SqliteConnection(ConnectionString))
        //    {
        //        db.Open();

        //        var createTable = new SqliteCommand(File.ReadAllText(ItemsSqlFile), db);

        //        createTable.ExecuteReader();
        //    }
        //}
        public static Item GetItemById(int id)
        {
            Item item = null;
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                                            SELECT *
                                            FROM items
                                            WHERE item_id = $id
                                        ";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new Item()
                        {
                            ItemId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Quality = reader.GetInt32(2),
                            Patch = reader.GetInt32(3),
                            Class = reader.GetInt32(4),
                            Subclass = reader.GetInt32(5),
                            Description = reader.GetString(6),
                            DisplayId = reader.GetInt32(7),
                            Flags = reader.GetInt32(8),
                            BuyCount = reader.GetInt32(9),
                            BuyPrice = reader.GetInt32(10),
                            SellPrice = reader.GetInt32(11),
                            InventoryType = reader.GetInt32(12),
                            AllowableClass = reader.GetInt32(13),
                            AllowableRace = reader.GetInt32(14),
                            ItemLevel = reader.GetInt32(15),
                            RequiredLevel = reader.GetInt32(16),
                            RequiredSkill = reader.GetInt16(17),
                            RequiredSkillRank = reader.GetInt16(18),
                            RequiredSpell = reader.GetInt32(19),
                            RequiredHonorRank = reader.GetInt32(20),
                            RequiredCityRank = reader.GetInt32(21),
                            RequiredReputationFaction = reader.GetInt32(22),
                            RequiredReputationRank = reader.GetInt32(23),
                            MaxCount = reader.GetInt32(24),
                            Stackable = reader.GetInt32(25),
                            ContainerSlots = reader.GetInt32(26),

                            StatType1 = reader.GetInt32(27),
                            StatValue1 = reader.GetInt32(28),
                            StatType2 = reader.GetInt32(29),
                            StatValue2 = reader.GetInt32(30),
                            StatType3 = reader.GetInt32(31),
                            StatValue3 = reader.GetInt32(32),
                            StatType4 = reader.GetInt32(33),
                            StatValue4 = reader.GetInt32(34),
                            StatType5 = reader.GetInt32(35),
                            StatValue5 = reader.GetInt32(36),
                            StatType6 = reader.GetInt32(37),
                            StatValue6 = reader.GetInt32(38),
                            StatType7 = reader.GetInt32(39),
                            StatValue7 = reader.GetInt32(40),
                            StatType8 = reader.GetInt32(41),
                            StatValue8 = reader.GetInt32(42),
                            StatType9 = reader.GetInt32(43),
                            StatValue9 = reader.GetInt32(44),
                            StatType10 = reader.GetInt32(45),
                            StatValue10 = reader.GetInt32(46),

                            Delay = reader.GetInt32(47),

                            RangeMod = reader.GetFloat(48),
                            AmmoType = reader.GetInt32(49),
                            DmgMin1 = reader.GetFloat(50),
                            DmgMax1 = reader.GetFloat(51),
                            DmgType1 = reader.GetInt32(52),
                            DmgMin2 = reader.GetFloat(53),
                            DmgMax2 = reader.GetFloat(54),
                            DmgType2 = reader.GetInt32(55),
                            DmgMin3 = reader.GetFloat(56),
                            DmgMax3 = reader.GetFloat(57),
                            DmgType3 = reader.GetInt32(58),
                            DmgMin4 = reader.GetFloat(59),
                            DmgMax4 = reader.GetFloat(60),
                            DmgType4 = reader.GetInt32(61),
                            DmgMin5 = reader.GetFloat(62),
                            DmgMax5 = reader.GetFloat(63),
                            DmgType5 = reader.GetInt32(64),

                            Block = reader.GetInt32(65),
                            Armor = reader.GetInt32(66),
                            HolyRes = reader.GetInt32(67),
                            FireRes = reader.GetInt32(68),
                            NatureRes = reader.GetInt32(69),
                            FrostRes = reader.GetInt32(70),
                            ShadowRes = reader.GetInt32(71),
                            ArcaneRes = reader.GetInt32(72),
                            SpellId1 = reader.GetInt32(73),
                            SpellTrigger1 = reader.GetInt32(74),
                            SpellCharges1 = reader.GetInt32(75),
                            SpellPpmRate1 = reader.GetFloat(76),
                            SpellCooldown1 = reader.GetInt32(77),
                            SpellCategory1 = reader.GetInt32(78),
                            SpellCategoryCooldown1 = reader.GetInt32(79),
                            SpellId2 = reader.GetInt32(80),
                            SpellTrigger2 = reader.GetInt32(81),
                            SpellCharges2 = reader.GetInt32(82),
                            SpellPpmRate2 = reader.GetFloat(83),
                            SpellCooldown2 = reader.GetInt32(84),
                            SpellCategory2 = reader.GetInt32(85),
                            SpellCategoryCooldown2 = reader.GetInt32(86),
                            SpellId3 = reader.GetInt32(87),
                            SpellTrigger3 = reader.GetInt32(88),
                            SpellCharges3 = reader.GetInt32(89),
                            SpellPpmRate3 = reader.GetFloat(90),
                            SpellCooldown3 = reader.GetInt32(91),
                            SpellCategory3 = reader.GetInt32(92),
                            SpellCategoryCooldown3 = reader.GetInt32(93),
                            SpellId4 = reader.GetInt32(94),
                            SpellTrigger4 = reader.GetInt32(95),
                            SpellCharges4 = reader.GetInt32(96),
                            SpellPpmRate4 = reader.GetFloat(97),
                            SpellCooldown4 = reader.GetInt32(98),
                            SpellCategory4 = reader.GetInt32(99),
                            SpellCategoryCooldown4 = reader.GetInt32(100),
                            SpellId5 = reader.GetInt32(101),
                            SpellTrigger5 = reader.GetInt32(102),
                            SpellCharges5 = reader.GetInt32(103),
                            SpellPpmRate5 = reader.GetFloat(104),
                            SpellCooldown5 = reader.GetInt32(105),
                            SpellCategory5 = reader.GetInt32(106),
                            SpellCategoryCooldown5 = reader.GetInt32(107),
                            Bonding = reader.GetInt32(108),
                            PageText = reader.GetInt32(109),
                            PageLanguage = reader.GetInt32(110),
                            PageMaterial = reader.GetInt32(111),
                            StartQuest = reader.GetInt32(112),
                            LockId = reader.GetInt32(113),
                            Material = reader.GetInt32(114),
                            Sheath = reader.GetInt32(115),
                            RandomProperty = reader.GetInt32(116),
                            SetId = reader.GetInt32(117),
                            MaxDurability = reader.GetInt32(118),
                            AreaBound = reader.GetInt32(119),
                            MapBound = reader.GetInt32(120),
                            Duration = reader.GetInt32(121),
                            BagFamily = reader.GetInt32(122),
                            DisenchantId = reader.GetInt32(123),
                            FoodType = reader.GetInt32(124),
                            MinMoneyLoot = reader.GetInt32(125),
                            MaxMoneyLoot = reader.GetInt32(126),
                            ExtraFlags = reader.GetInt32(127),
                            OtherTeamEntry = reader.GetInt32(128),
                        };
                    }
                }
            }
            return item;
        }
        public static void GetSampleItems()
        {
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        using (var connection = new SqliteConnection(ConnectionString))
                        {
                            connection.Open();

                            var command = connection.CreateCommand();
                            command.CommandText = @"
                                            SELECT *
                                            FROM items
                                            WHERE inventory_type = $id
                                            AND class = $class
                                            AND subclass = $subclass
                                            LIMIT 2
                                        ";
                            command.Parameters.AddWithValue("$id", i);
                            command.Parameters.AddWithValue("$class", j);
                            command.Parameters.AddWithValue("$subclass", k);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Logging.WriteDebug(reader.GetString(1) + " " + reader.GetInt32(12) + " " + reader.GetInt32(4) + " " + reader.GetInt32(5));
                                };
                            }
                        }
                    }
                }
            }
        }
    }
}
