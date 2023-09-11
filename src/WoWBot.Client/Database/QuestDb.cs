using robotManager.Helpful;
using System.Collections.Generic;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Models;
using WoWBot.Client.Quest;

namespace WoWBot.Client.Database
{
    public static class QuestDb
    {
        public static QuestTask GetQuestTaskById(int id)
        {
            QuestTemplate questTemplate = MangosDb.GetQuestTemplateByID(id);
            List<int> relatedNpcIds = MangosDb.GetQuestRelatedNPCsByQuestId(id);
            List<QuestPoiPoint> questPoiPoints = MangosDb.GetQuestPoiPointsByQuestId(id);

            List<Creature> relatedNpcs = new List<Creature>();

            QuestTask questTask = new QuestTask
            {
                QuestId = questTemplate.Entry,
                Name = questTemplate.Title,
            };

            for (int i = 0; i < relatedNpcIds.Count; i++)
            {
                relatedNpcs.AddRange(MangosDb.GetCreaturesById(relatedNpcIds[i]));
            }

            if (relatedNpcs.Count > 0)
            {
                relatedNpcs.Sort((x, y) =>
                    new Vector3(x.PositionX, x.PositionY, x.PositionZ).DistanceTo(ObjectManager.Me.Position)
                        .CompareTo(new Vector3(y.PositionX, y.PositionY, y.PositionZ).DistanceTo(ObjectManager.Me.Position)));

                questTask.TurnInNpc = new NpcBase.QuestGiver()
                {
                    NpcId = relatedNpcs[0].Id,
                    Position = new Vector3(relatedNpcs[0].PositionX, relatedNpcs[0].PositionY, relatedNpcs[0].PositionZ)
                };
            }

            // Requires killing a mob (ReqCreatureOrGOId > 0), looting a game object (ReqCreatureOrGOId < 0), or requires an item looted from
            // a mob or a game object (not an issued by the quest giver)
            if (questTemplate.ReqCreatureOrGOId1 != 0 || (questTemplate.ReqItemId1 != 0 && questTemplate.SrcItemId != questTemplate.ReqItemId1))
            {
                questTask.QuestObjectives.Add(GetQuestObjective(1, questTemplate));
                if (questTemplate.ReqCreatureOrGOId2 != 0 || (questTemplate.ReqItemId2 != 0 && questTemplate.SrcItemId != questTemplate.ReqItemId2))
                {
                    questTask.QuestObjectives.Add(GetQuestObjective(2, questTemplate));
                    if (questTemplate.ReqCreatureOrGOId3 != 0 || (questTemplate.ReqItemId3 != 0 && questTemplate.SrcItemId != questTemplate.ReqItemId3))
                    {
                        questTask.QuestObjectives.Add(GetQuestObjective(3, questTemplate));
                        if (questTemplate.ReqCreatureOrGOId4 != 0 || (questTemplate.ReqItemId4 != 0 && questTemplate.SrcItemId != questTemplate.ReqItemId4))
                        {
                            questTask.QuestObjectives.Add(GetQuestObjective(4, questTemplate));
                        }
                    }
                }
            }
            else
            {
                questTask.QuestObjectives.Add(GetQuestObjective(1, questTemplate));

                if (questTask.QuestObjectives.Count > 0)
                {
                    questTask.QuestObjectives[0].HotSpots.Add(questTask.TurnInNpc.Position);
                }
            }

            if (questTemplate.RewChoiceItemId1 != 0)
            {
                questTask.RewardItem1 = MangosDb.GetItemById(questTemplate.RewChoiceItemId1);
                if (questTemplate.RewChoiceItemId2 != 0)
                {
                    questTask.RewardItem2 = MangosDb.GetItemById(questTemplate.RewChoiceItemId2);
                    if (questTemplate.RewChoiceItemId3 != 0)
                    {
                        questTask.RewardItem3 = MangosDb.GetItemById(questTemplate.RewChoiceItemId3);
                        if (questTemplate.RewChoiceItemId4 != 0)
                        {
                            questTask.RewardItem4 = MangosDb.GetItemById(questTemplate.RewChoiceItemId4);
                            if (questTemplate.RewChoiceItemId5 != 0)
                            {
                                questTask.RewardItem5 = MangosDb.GetItemById(questTemplate.RewChoiceItemId5);
                                if (questTemplate.RewChoiceItemId6 != 0)
                                {
                                    questTask.RewardItem6 = MangosDb.GetItemById(questTemplate.RewChoiceItemId6);
                                }
                            }
                        }
                    }
                }
            }

            Logging.WriteDebug(questTask.ToString());

            return questTask;
        }

        private static QuestObjective GetQuestObjective(int objectiveIndex, QuestTemplate questTemplate)
        {
            QuestObjective questObjective = new QuestObjective()
            {
                QuestId = questTemplate.Entry,
                Index = objectiveIndex,
            };

            switch (objectiveIndex)
            {
                case 1:
                    if (questTemplate.ReqCreatureOrGOId1 > 0)
                    {
                        questObjective.CreatureId = questTemplate.ReqCreatureOrGOId1;
                    }
                    else if (questTemplate.ReqCreatureOrGOId1 < 0)
                    {
                        questObjective.GameObjectId = System.Math.Abs(questTemplate.ReqCreatureOrGOId1);
                    }
                    break;
                case 2:
                    if (questTemplate.ReqCreatureOrGOId2 > 0)
                    {
                        questObjective.CreatureId = questTemplate.ReqCreatureOrGOId2;
                    }
                    else if (questTemplate.ReqCreatureOrGOId2 < 0)
                    {
                        questObjective.GameObjectId = System.Math.Abs(questTemplate.ReqCreatureOrGOId2);
                    }
                    break;
                case 3:
                    if (questTemplate.ReqCreatureOrGOId3 > 0)
                    {
                        questObjective.CreatureId = questTemplate.ReqCreatureOrGOId3;
                    }
                    else if (questTemplate.ReqCreatureOrGOId3 < 0)
                    {
                        questObjective.GameObjectId = System.Math.Abs(questTemplate.ReqCreatureOrGOId3);
                    }
                    break;
                case 4:

                    if (questTemplate.ReqCreatureOrGOId4 > 0)
                    {
                        questObjective.CreatureId = questTemplate.ReqCreatureOrGOId4;
                    }
                    else if (questTemplate.ReqCreatureOrGOId4 < 0)
                    {
                        questObjective.GameObjectId = System.Math.Abs(questTemplate.ReqCreatureOrGOId4);
                    }
                    break;
            }


            if (questTemplate.SrcItemId != 0)
            {
                // Item is usable on the mob
                if (questObjective.CreatureId > 0)
                {
                    questObjective.UsableItemId = questTemplate.SrcItemId;
                }
                // Item should be consumed
                else
                {
                    questObjective.ConsumableItemId = questTemplate.SrcItemId;
                }
            }

            //Find items that need to be looted from either mobs or game objects
            List<Creature> creatures = new List<Creature>();
            List<GameObject> gameObjects = new List<GameObject>();

            switch (objectiveIndex)
            {
                case 1:
                    if (questTemplate.ReqItemId1 != 0)
                    {
                        creatures = MangosDb.GetCreaturesByLootableItemId(questTemplate.ReqItemId1);
                        gameObjects = MangosDb.GetGameObjectByLootableItemId(questTemplate.ReqItemId1);
                    }
                    else if (questObjective.CreatureId != 0)
                    {
                        List<Creature> questCreatures = MangosDb.GetCreaturesById(questObjective.CreatureId);

                        foreach (Creature creature in questCreatures)
                        {
                            questObjective.HotSpots.Add(new Vector3(creature.PositionX, creature.PositionY, creature.PositionZ));
                        }
                    }
                    else if (questObjective.GameObjectId != 0)
                    {
                        gameObjects = MangosDb.GetGameObjectsById(questObjective.GameObjectId);
                    }
                    break;
                case 2:
                    if (questTemplate.ReqItemId2 != 0)
                    {
                        creatures = MangosDb.GetCreaturesByLootableItemId(questTemplate.ReqItemId2);
                        gameObjects =  MangosDb.GetGameObjectByLootableItemId(questTemplate.ReqItemId2);
                    }
                    else if (questObjective.CreatureId != 0)
                    {
                        List<Creature> questCreatures = MangosDb.GetCreaturesById(questObjective.CreatureId);

                        foreach (Creature creature in questCreatures)
                        {
                            questObjective.HotSpots.Add(new Vector3(creature.PositionX, creature.PositionY, creature.PositionZ));
                        }
                    }
                    else if (questObjective.GameObjectId != 0)
                    {
                        gameObjects = MangosDb.GetGameObjectsById(questObjective.GameObjectId);
                    }
                    break;
                case 3:
                    if (questTemplate.ReqItemId3 != 0)
                    {
                        creatures = MangosDb.GetCreaturesByLootableItemId(questTemplate.ReqItemId3);
                        gameObjects = MangosDb.GetGameObjectByLootableItemId(questTemplate.ReqItemId3);
                    }
                    else if (questObjective.CreatureId != 0)
                    {
                        List<Creature> questCreatures = MangosDb.GetCreaturesById(questObjective.CreatureId);

                        foreach (Creature creature in questCreatures)
                        {
                            questObjective.HotSpots.Add(new Vector3(creature.PositionX, creature.PositionY, creature.PositionZ));
                        }
                    }
                    else if (questObjective.GameObjectId != 0)
                    {
                        gameObjects = MangosDb.GetGameObjectsById(questObjective.GameObjectId);
                    }
                    break;
                case 4:
                    if (questTemplate.ReqItemId4 != 0)
                    {
                        creatures = MangosDb.GetCreaturesByLootableItemId(questTemplate.ReqItemId4);
                        gameObjects = MangosDb.GetGameObjectByLootableItemId(questTemplate.ReqItemId4);
                    }
                    else if (questObjective.CreatureId != 0)
                    {
                        List<Creature> questCreatures = MangosDb.GetCreaturesById(questObjective.CreatureId);

                        foreach (Creature creature in questCreatures)
                        {
                            questObjective.HotSpots.Add(new Vector3(creature.PositionX, creature.PositionY, creature.PositionZ));
                        }
                    }
                    else if (questObjective.GameObjectId != 0)
                    {
                        gameObjects = MangosDb.GetGameObjectsById(questObjective.GameObjectId);
                    }
                    break;
            }

            if (gameObjects.Count > 0 || creatures.Count > 0)
            {
                OrganizeObjectiveTargets(questObjective, gameObjects, creatures);
            }

            return questObjective;
        }

        private static void OrganizeObjectiveTargets(QuestObjective questObjective, List<GameObject> gameObjects, List<Creature> creatures)
        {
            // Drops from mob
            if (creatures.Count > 0)
            {
                questObjective.CreatureId = creatures[0].Id;

                foreach (Creature creature in creatures)
                {
                    questObjective.HotSpots.Add(new Vector3(creature.PositionX, creature.PositionY, creature.PositionZ));
                }
            }
            // Looted from game object
            else if (gameObjects.Count > 0)
            {
                questObjective.GameObjectId = gameObjects[0].Id;

                foreach (GameObject gameObject in gameObjects)
                {
                    questObjective.HotSpots.Add(new Vector3(gameObject.PositionX, gameObject.PositionY, gameObject.PositionZ));
                }
            }
        }
    }
}
