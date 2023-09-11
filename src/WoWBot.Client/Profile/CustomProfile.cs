using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using AdvancedQuester.FSM.States;
using Custom_Profile;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using wManager;
using wManager.Events;
using wManager.Wow.Bot.States;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using WoWBot.Client;
using WoWBot.Client.Database;
using WoWBot.Client.Helpers;
using WoWBot.Client.Models;

public class CustomProfile : ICustomProfile
{
    private static readonly Engine Fsm = new Engine();
    public static bool NeedsGearCheck = false;
    private MainWindow _logWindowDisposable;
    public static Thread Thread;

    public void Pulse()
    {
        try
        {
            //Launch Logging Window
            if (_logWindowDisposable == null)
            {
                Thread = new Thread(() =>
                {
                    _logWindowDisposable = new MainWindow(); // Replace with your new window class name
                    _logWindowDisposable.Show();

                    // Start the dispatcher processing
                    System.Windows.Threading.Dispatcher.Run();
                });

                Thread.SetApartmentState(ApartmentState.STA);
                Thread.Start();
            }
            // Update spell list
            SpellManager.UpdateSpellBook();

            // Load CC:
            CustomClass.LoadCustomClass();

            MangosDb.Initialize();
            PopulateNPCDatabase();

            wManagerSetting.CurrentSetting.PathFinderFromServer = true;
            wManagerSetting.CurrentSetting.EquipAvailableBagIfFreeContainerSlot = true;
            wManagerSetting.CurrentSetting.TrainNewSkills = true;
            wManagerSetting.CurrentSetting.AvoidWallWithRays = false;
            wManagerSetting.CurrentSetting.MinFreeBagSlotsToGoToTown = 2;
            wManagerSetting.CurrentSetting.Save();

            ConfigWowForThisBot.ConfigWow();

            LootingEvents.OnLootSuccessful += (unit) =>
            {
                InventoryHelper.UpgradeToLatestEquipment();
            };

            Reevaluate();
        }
        catch (Exception e)
        {
            try
            {
                Dispose();
            }
            catch
            {
            }
            Logging.WriteError("CustomProfile > Pulse(): " + e);
            Logging.WriteError("CustomProfile > Cause: " + e.Message);
            Logging.WriteError("CustomProfile > : " + e.StackTrace);
        }
    }

    private void PopulateNPCDatabase()
    {
        List<CreatureTemplate> classTrainers = MangosDb.GetAllClassTrainers();

        foreach (CreatureTemplate creatureTemplate in classTrainers)
        {
            Npc.NpcType npcType = GetNpcTypeFromCreatureTemplate(creatureTemplate);

            if (npcType != Npc.NpcType.None)
            {
                Creature creature = MangosDb.GetCreatureById(creatureTemplate.Entry);

                if (creature != null && NpcDB.ListNpc.FindAll(x => x.Entry == creature.Id).Count == 0)
                {
                    Npc npc = new Npc()
                    {
                        Entry = creatureTemplate.Entry,
                        Name = creatureTemplate.Name,
                        GossipOption = -1,
                        Active = true,
                        ContinentId = (wManager.Wow.Enums.ContinentId)creature.Map,
                        Position = new Vector3(creature.PositionX, creature.PositionY, creature.PositionZ),
                        PosX = creature.PositionX,
                        PosY = creature.PositionY,
                        PosZ = creature.PositionZ,
                        CurrentProfileNpc = true,
                        Save = true,
                        CanFlyTo = false,
                        Faction = Npc.FactionType.Neutral,
                        Type = npcType,
                        VendorItemClass = Npc.NpcVendorItemClass.None,
                    };

                    NpcDB.AddNpc(npc);
                }
            }
        }

        List<CreatureTemplate> vendors = MangosDb.GetAllVendors();
    }

    private Npc.NpcVendorItemClass GetNpcVendorItemClassFromCreatureTemplate(CreatureTemplate creatureTemplate)
    {
        int vendorCheckBit = 128;
        int foodCheckBit = 512;

        if ((creatureTemplate.NpcFlags & vendorCheckBit) == vendorCheckBit)
        {
            if ((creatureTemplate.NpcFlags & foodCheckBit) == foodCheckBit)
            {
                return Npc.NpcVendorItemClass.Food;
            }
        }

        return Npc.NpcVendorItemClass.None;
    }
    private Npc.NpcType GetNpcTypeFromCreatureTemplate(CreatureTemplate creatureTemplate)
    {
        int trainerCheckBit = 16;
        int vendorCheckBit = 128;
        int repairCheckBit = 4096;

        if ((creatureTemplate.NpcFlags & trainerCheckBit) == trainerCheckBit)
        {
            switch (creatureTemplate.TrainerClass)
            {
                case 1:
                    return Npc.NpcType.WarriorTrainer;
                case 2:
                    return Npc.NpcType.PaladinTrainer;
                case 3:
                    return Npc.NpcType.HunterTrainer;
                case 4:
                    return Npc.NpcType.RogueTrainer;
                case 5:
                    return Npc.NpcType.PriestTrainer;
                case 7:
                    return Npc.NpcType.ShamanTrainer;
                case 8:
                    return Npc.NpcType.MageTrainer;
                case 9:
                    return Npc.NpcType.WarlockTrainer;
                case 11:
                    return Npc.NpcType.DruidTrainer;
            }
        }
        else if ((creatureTemplate.NpcFlags & vendorCheckBit) == vendorCheckBit)
        {
            return Npc.NpcType.Vendor;
        }
        else if ((creatureTemplate.NpcFlags & repairCheckBit) == repairCheckBit)
        {
            return Npc.NpcType.Repair;
        }

        return Npc.NpcType.None;
    }
    public void Dispose()
    {
        try
        {
            MovementManager.StopMove();
            Fight.StopFight();
            Fsm.StopEngine();
            CustomClass.DisposeCustomClass();
            _logWindowDisposable?.Dispatcher.InvokeShutdown();
        }
        catch (Exception e)
        {
            Logging.WriteError("CustomProfile > Dispose(): " + e);
        }
    }

    public static void Reevaluate()
    {
        Logging.Write("[CustomProfile] Reevaluate");
        Task.Run(() =>
        {
            Fsm.StopEngine();

            Thread.Sleep(1000);

            Fsm.States.Clear();

            Fsm.AddState(new Resurrect { Priority = 15 });
            Fsm.AddState(new MyMacro { Priority = 14 });
            Fsm.AddState(new FightHostileTarget { Priority = 13 });
            Fsm.AddState(new IsAttacked { Priority = 12 });
            Fsm.AddState(new Regeneration { Priority = 11 });
            Fsm.AddState(new Looting { Priority = 10 });
            Fsm.AddState(new ToTown { Priority = 6 });
            Fsm.AddState(new Talents { Priority = 5 });
            Fsm.AddState(new Trainers { Priority = 4 });
            Fsm.AddState(new Questing { Priority = 2 });

            Fsm.AddState(new Idle { Priority = 0 });

            Logging.Write("[CustomProfile] Starting engine");

            Fsm.States.Sort();
            Fsm.StartEngine(10);
        });
    }
}


