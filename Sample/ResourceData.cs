using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerToGantt
{
    public partial class ResourceData
    {
        public static List<ResourceAllocateData> GetResources = new List<ResourceAllocateData>()
        {
            new ResourceAllocateData() { ResourceId= 1, ResourceName= "Martin Tamer" ,Unit=70},
            new ResourceAllocateData() { ResourceId= 2, ResourceName= "Rose Fuller" },
            new ResourceAllocateData() { ResourceId= 3, ResourceName= "Margaret Buchanan" },
            new ResourceAllocateData() { ResourceId= 4, ResourceName= "Fuller King" },
            new ResourceAllocateData() { ResourceId= 5, ResourceName= "Davolio Fuller" },
            new ResourceAllocateData() { ResourceId= 6, ResourceName= "Van Jack" },
            new ResourceAllocateData() { ResourceId= 7, ResourceName= "Fuller Buchanan" },
            new ResourceAllocateData() { ResourceId= 8, ResourceName= "Jack Davolio" },
            new ResourceAllocateData() { ResourceId= 9, ResourceName= "Tamer Vinet" },
            new ResourceAllocateData() { ResourceId= 10, ResourceName= "Vinet Fuller" },
            new ResourceAllocateData() { ResourceId= 11, ResourceName= "Bergs Anton" },
            new ResourceAllocateData() { ResourceId= 12, ResourceName= "Construction Supervisor" }
                };
        public static List<TaskData> GetTaskCollection()
        {
            List<TaskData> Tasks = new List<TaskData>() {
        new TaskData() {
            TaskId = "1",
            TaskName = "Project initiation",
            StartDate = new DateTime(2021, 03, 28),
            EndDate = new DateTime(2021, 07, 28),
            TaskType ="FixedDuration",
            Work=128,
            Duration="4"
        },
        new TaskData() {
            TaskId = "2",
            TaskName = "Identify site location",
            StartDate = new DateTime(2021, 03, 29),
            Progress = 30,
            ParentId = 1,
            Duration="2",
            TaskType ="FixedDuration",
            Work=16,
            Resources = new List<ResourceAllocateData>(){ new ResourceAllocateData() { ResourceId=1,Unit=70} ,new ResourceAllocateData() { ResourceId=6} }
        },
        new TaskData() {
            TaskId = "3",
            TaskName = "Perform soil test",
            StartDate = new DateTime(2021, 03, 29),
            Resources = new List<ResourceAllocateData>(){ new ResourceAllocateData() { ResourceId=2} ,new ResourceAllocateData() { ResourceId=3} ,new ResourceAllocateData() { ResourceId=5} },
            ParentId = 1,
            Work=96,
            Duration="4",
            TaskType="FixedWork"
        },
        new TaskData() {
            TaskId = "4",
            TaskName = "Soil test approval",
            StartDate = new DateTime(2021, 03, 29),
            Duration = "1",
            Progress = 30,
            ParentId = 1,
            Resources = new List<ResourceAllocateData>(){ new ResourceAllocateData() { ResourceId=8} ,new ResourceAllocateData() { ResourceId=9} },
            Work=16,
            TaskType="FixedWork"
        },
        new TaskData() {
            TaskId = "5",
            TaskName = "Project estimation",
            StartDate = new DateTime(2021, 03, 29),
            EndDate = new DateTime(2021, 04, 2),
            TaskType="FixedDuration",
            Duration="4"
        },
        new TaskData() {
            TaskId = "6",
            TaskName = "Develop floor plan for estimation",
            StartDate = new DateTime(2021, 03, 29),
            Duration = "3",
            Progress = 30,
            ParentId = 5,
            Resources = new List<ResourceAllocateData>(){ new ResourceAllocateData() { ResourceId=4} },
            Work=30,
            TaskType="FixedWork"
        },
        new TaskData() {
            TaskId = "7",
            TaskName = "List materials",
            StartDate = new DateTime(2021, 04, 01),
            Duration = "3",
            Progress = 30,
            ParentId = 5,
            TaskType="FixedWork",
            Work=48,
            Resources = new List<ResourceAllocateData>(){ new ResourceAllocateData() { ResourceId=4},new ResourceAllocateData() { ResourceId=8} }
        },
        new TaskData() {
            TaskId = "8",
            TaskName = "Estimation approval",
            StartDate = new DateTime(2021, 04, 01),
            Duration = "2",
            ParentId = 5,
            Work=60,
            TaskType="FixedWork",
            Resources = new List<ResourceAllocateData>(){ new ResourceAllocateData() { ResourceId= 12},new ResourceAllocateData() { ResourceId= 5} }
        },
        new TaskData() {
            TaskId = "9",
            TaskName = "Sign contract",
            StartDate = new DateTime(2021, 03, 31),
            EndDate = new DateTime(2021, 04, 01),
            Duration="1",
            TaskType="FixedWork",
            Work=24,
            Resources = new List<ResourceAllocateData>(){ new ResourceAllocateData() { ResourceId= 12},new ResourceAllocateData() { ResourceId= 5} }
        },
    };
            return Tasks;
        }
    }
}