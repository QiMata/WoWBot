using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using AdvancedQuester.Quest;
using robotManager.Helpful;

namespace AdvancedQuester.TestQuests
{
    public class CuttingTeeth : QuestTask
    {
        public CuttingTeeth()
        {
            QuestName = "Cutting Teeth";
            QuestId = 788;
            PickUpNpc = new QuestGiver
            {
                NpcName = "Gornek",
                NpcId = 3134,
                Position = new Vector3(-600.132f, -4186.19f, 41.08915f)
            };
            TurnInNpc = PickUpNpc;
            ToArea.AddRange(new List<Vector3>
            {
                new Vector3(-600.6609f, -4190.028f, 41.09747f, "None"),
                new Vector3(-601.3873f, -4192.863f, 41.09767f, "None"),
                new Vector3(-602.2546f, -4196.246f, 41.09767f, "None"),
                new Vector3(-603.0514f, -4199.652f, 40.96827f, "None"),
                new Vector3(-603.4778f, -4203.125f, 40.25501f, "None"),
                new Vector3(-603.5517f, -4206.62f, 38.50479f, "None"),
                new Vector3(-603.3094f, -4210.11f, 38.29113f, "None"),
                new Vector3(-602.38f, -4213.458f, 38.47211f, "None"),
                new Vector3(-600.6697f, -4216.512f, 38.23663f, "None"),
                new Vector3(-599.0916f, -4219.636f, 38.31187f, "None"),
                new Vector3(-597.5381f, -4222.772f, 38.17006f, "None"),
                new Vector3(-595.9847f, -4225.908f, 38.1692f, "None"),
                new Vector3(-594.4312f, -4229.045f, 38.24033f, "None"),
                new Vector3(-592.8778f, -4232.181f, 38.18168f, "None"),
                new Vector3(-591.3146f, -4235.313f, 38.17253f, "None"),
                new Vector3(-589.6031f, -4238.366f, 38.13491f, "None"),
                new Vector3(-587.8496f, -4241.395f, 38.14624f, "None"),
                new Vector3(-585.8932f, -4244.297f, 38.32281f, "None"),
                new Vector3(-583.9361f, -4247.199f, 37.99532f, "None"),
                new Vector3(-581.979f, -4250.101f, 37.85158f, "None"),
                new Vector3(-580.0884f, -4252.903f, 37.94198f, "None"),
                new Vector3(-578.038f, -4255.885f, 37.96259f, "None"),
                new Vector3(-576.0355f, -4258.756f, 37.921f, "None"),
                new Vector3(-573.9998f, -4261.602f, 37.99207f, "None"),
                new Vector3(-571.9053f, -4264.254f, 38.06863f, "None"),
                new Vector3(-569.3955f, -4266.861f, 38.21403f, "None"),
                new Vector3(-566.7034f, -4269.089f, 38.27538f, "None"),
                new Vector3(-563.8197f, -4271.072f, 38.41393f, "None"),
                new Vector3(-560.8667f, -4272.951f, 38.53642f, "None"),
                new Vector3(-557.8962f, -4274.802f, 38.70748f, "None"),
                new Vector3(-554.8947f, -4276.602f, 39.01046f, "None"),
                new Vector3(-551.9838f, -4278.321f, 38.75349f, "None"),
                new Vector3(-548.8681f, -4280.162f, 38.09847f, "None"),
                new Vector3(-545.8548f, -4281.943f, 37.92727f, "None"),
                new Vector3(-542.8414f, -4283.723f, 37.94929f, "None"),
                new Vector3(-539.8281f, -4285.503f, 37.86578f, "None"),
                new Vector3(-536.8148f, -4287.284f, 37.8061f, "None")
            });
            HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-513.5054f, -4290.405f, 39.87229f),
                new Vector3(-492.8509f, -4350.517f, 39.30183f),
                new Vector3(-454.5764f, -4246.796f, 49.82771f)
            });
            Target.Add(3098);
            Objective.Add(10);
        }
    }
}