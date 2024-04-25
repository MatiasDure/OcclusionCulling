## Introduction

In game development, optimizing rendering performance is extremely important to ensure smooth and efficient operations. With the complexity of modern game environments and the increasing demand for immersive experiences, a big challenge for developers is maximizing visual quality while maintaining acceptable performance levels across various hardware platforms.

Unity comes with a range of built-in tools and techniques to deal with these optimization challenges. Among these, frustum culling, occlusion culling, and Level of Detail (LOD) are widely-used methods for managing rendering workload and improving performance. 

The techniques mentioned above will be explained and their performance impact, gains and losses, will be measured in different test scenarios to understand their strengths and weaknesses, and when to best apply them.

## Frustum Culling

Frustum culling is an optimization technique used in real-time rendering engines like Unity to improve performance by rendering only the objects that are within the camera's view frustum. The [view frustum](https://docs.unity3d.com/Manual/UnderstandingFrustum.html) is the portion of the scene that is visible to the camera, which is defined by a pyramid-shaped volume extending from the camera's position. By ignoring objects outside this volume, frustum culling reduces unnecessary rendering overhead and ensures that only the current visible portions of the scene are processed by the graphics pipeline. This technique is extremely handy in scenes with large environments, where rendering everything would greatly impact performance. 

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/d976c615-ef3c-4a5e-8efa-721a7a32cc46)

Fortunately, frustum culling is enabled by default in Unity, although it does not check whether a Renderer is blocked by other GameObjects. This however, can be tackled by the next technique mentioned, occlusion culling.

## Occlusion Culling

[Occlusion culling](https://docs.unity3d.com/Manual/OcclusionCulling.html#:~:text=Occlusion%20culling%20works%20best%20in,GameObjects%20cannot%20occlude%20other%20GameObjects.) aims to reduce rendering workload by avoiding rendering objects that are not visible due to obstructions in the scene. Unlike frustum culling, which considers the camera's view frustum, occlusion culling takes into account objects that block the view of other objects. By identifying occluded objects and preventing them from being rendered, occlusion culling reduces the number of objects processed by the graphics pipeline, which in some cases leads to improved performance. This technique is especially useful in scenes where small, well-defined areas are clearly separated from one another by solid GameObjects. A good example would be rooms connected by corridors. Unity utilizes the [Umbra](https://blog.unity.com/engine-platform/occlusion-culling-basics-unity-4-3) library to perform occlusion culling.

There are few parameters available that can be tweaked in the occlusion culling window, specifically in the _Bake_ tab, to improve Umbra's occlusion culling accuracy, processing time, and data size. [Occlusion Culling: Best Practices](https://blog.unity.com/engine-platform/occlusion-culling-best-practices-unity-4-3) can come in handy when starting out.

Another essential component to get started with occlusion culling is the [Occlusion Area](https://docs.unity3d.com/Manual/class-OcclusionArea.html), which is used to define _View Volumes_. These volumes are used to enhance data precision when baking, and is recommended to be placed in areas where the camera is most likely to be during runtime.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/7d97ceca-159d-4d10-957d-e86b961e6808)

## Level Of Detail (LOD)

[Level of Detail (LOD)](https://docs.unity3d.com/Manual/LevelOfDetail.html) is a rendering optimization technique that adjusts the level of detail of objects based on their distance from the camera. The concept behind LOD is to render objects with lower polygon counts or simpler representations of the object when they are far away from the camera, gradually increasing the level of detail as they move closer. By prioritizing visual quality where it matters most and preserve resources where details are less distinguishable, LOD helps maintain a balance between visual quality and performance. This technique is commonly used in environments with large open spaces or numerous objects, like open-world games, to ensure smooth performance without sacrificing visual quality.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/98a09ce9-35f2-4b3c-a211-9d35583a7017)

## Testing

### Occlusion Culling

To test occlusion culling, a small room environment was created with corridors and corners, following recommendations for an ideal occlusion culling setup. This room was duplicated into three different scenes to test occlusion culling performance under different conditions:

**Empty Scene:** The first scene contained no game objects to be used as a starting point.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/878addd0-a523-4eb0-ba2e-66937ea2a297)

**Low Poly Scene:** The second scene contained game objects with low polygon counts, with each object containing 3936 triangles. The Blender monkey, Suzanne, was used.

![Screenshot 2024-04-23 220303](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/7dfc916b-da0d-4add-a887-4c151d58ebdc)

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/365fef8f-5320-49c4-be13-f0191f2d8c8e)

**High Poly Scene:** The third scene consisted of high-poly objects, where each model contained 251904 triangles. This extreme scenario was created to investigate the impact of occlusion culling on rendering optimization under demanding conditions. The Blender monkey, Suzanne, was used along with a _Subdivision Surface_ modifier to increase the amount of triangles for the test.

![Screenshot 2024-04-23 220239](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/cec9f969-886c-497d-83c2-e9f26fd99a64)

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/3693e88e-feaa-4fd7-be73-1257f9116ce9)
 
To examine occlusion culling's performance in these scenes, tools were developed to track performance metrics, like triangle count, frames per second (fps), and elapsed time. These tools collected data every second and wrote the results to a file until the camera reached the final waypoint. Additionally, a script was implemented to move the camera to specified waypoints to simulate player movement and capture performance fluctuations throughout gameplay.

The tests were held both with occlusion culling enabled and disabled to determine its impact on rendering performance. By comparing the results of these tests, I aimed to evaluate the effectiveness of occlusion culling in optimizing rendering performance in Unity.

### Level of Detail

To evaluate the performance of LOD in Unity, I conducted a series of tests using different scenarios designed to stress-test LOD functionality under different levels of scene complexity. Each scenario consisted of overlapping objects to maximize scene complexity without spreading objects all over the scene. Some tools were created to automate these test, and iterate efficiently through each test scenario. For each scenario, there are an _**n**_ amount of GameObjects with 4 level of details spawned.

The model used for the LOD GameObject is the Suzanne Blender monkey.

**LOD0:** 3936 triangles

![Screenshot 2024-04-23 235810](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/a25f3e83-796c-4725-b497-eb488ba0aa31)

**LOD1:** 1968 triangles

![Screenshot 2024-04-23 235824](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/83c10430-0b8f-4646-9e42-842168cd5fad)

**LOD2:** 627 triangles

![Screenshot 2024-04-23 235838](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/b2caf832-332f-47dc-825c-d3a4fc94ae1c)

**LOD3:** 78 triangles

![Screenshot 2024-04-23 235853](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/cf0ec786-2c13-4b6c-9c23-80c13d663413)

Using these 4 different versions of the model, the _LOD Group_ was created for the _Monkey_ GameObject.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/39d63577-30ee-48e1-8ea0-754f2a25c0e5)

The following seven scenarios were created:

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/8d5475b2-9d24-4513-86ec-44511526f64e)

**Scenario 1:** Spawns 10 Monkeys.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/446bc2e6-4491-495f-893a-3d2bb9845516)
![Screenshot 2024-04-24 001315](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/900762ed-c0b0-41db-bbb0-85e99c4e2046)
![Screenshot 2024-04-24 001325](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/07bae8e7-65d7-46ac-bdfa-c5b5dfc68ac6)
![Screenshot 2024-04-24 001333](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/780f98e6-ad66-4c5f-9d85-85d1f2c8eba4)

**Scenario 2:** Spawns 50 Monkeys.

**Scenario 3:** Spawns 100 Monkeys.

**Scenario 4:** Spawns 250 Monkeys.

**Scenario 5:** Spawns 500 Monkeys.

**Scenario 6:** Spawns 1000 Monkeys.

**Scenario 7:** Spawns 2000 Monkeys.

For each scenario, there are four types of tests, each measuring fps and triangle count at a specific moment:

**Type LOD0 Monkey:** Camera up close, rendering LOD0.

**Type LOD1 Monkey:** Rendering LOD1.

**Type LOD2 Monkey:** Rendering LOD2.

**Type LOD3 Monkey:** Rendering LOD3.

A waiting period of 5 seconds was introduced between tests to ensure stability and consistency in measurements. This testing approach allowed for a detailed analysis of how LOD performance varied across different scene complexities and LOD levels. By comparing the performance metrics between LOD-enabled and LOD-disabled scenarios, I was able to gain insights into the effectiveness of LOD in optimizing rendering performance in Unity.

## Results

### Understanding the graphs

All graphs have horizontal (x) and vertical (y) axes. The x axis stays constant throughout all graphs representing the time elapsed between each data retrieval. The y axis has two possible representations, the frames per second (fps) or the triangle count. Both axes have a title describing what they represent in terms of the data being displayed.

When reading graphs that contain the triangle count on the vertical axis, the values displayed along with it are meant to be multiplied by a thousand.

### Occlusion Culling

**Scenario 1: Empty Room** This scenario is used as a baseline, as well as to check how occlusion culling performs in the absence of objects in the scene. 

_FPS Trends:_
The scenario with occlusion culling disabled maintains higher FPS values throughout most of the elapsed time compared to the scenario with occlusion culling enabled. Both scenarios display fluctuations in FPS values over time, but occlusion culling disabled consistently maintains a performance advantage.

_Triangle Count Trends:_
With no objects present, triangle counts remain consistently low and stable for both occlusion culling enabled and disabled scenarios. The stability and similar triangle count suggests that the performance differences observed in FPS are not due to varying rendering complexities but are likely influenced by other factors like occlusion culling calculations on the CPU.

_Conclusion:_
Using the data as guide, it can be seen that there is no performance advantage by using occlusion culling in an scene with little amount of objects. On the contrary, there is a performance impact which could be associated to the culling operations performed against the baked data.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/a805e7f6-5729-4619-8250-d96c6866e6a4)

**Scenario 2: Room with low-poly objects** This scenario is used to assess how occlusion culling performs with multiple low-poly objects in a scene. 

_FPS Trends:_
Both occlusion culling enabled and disabled scenarios maintain similar FPS values throughout most of the elapsed time. The similarity suggests that occlusion culling may not be significantly impacting performance in this scenario.

_Triangle Count Trends:_
In the occlusion culling disabled scenario, the triangle count is clearly higher compared to the occlusion culling enabled scenario.
Despite the higher triangle count, FPS values remain similar between the two scenarios, which hint that other factors may be influencing performance more than the rendering complexity alone.

_Conclusion:_
The similar FPS values between occlusion culling enabled and disabled scenarios imply that occlusion culling may not be providing a significant performance benefit in the low poly object scenario. The higher triangle count in the occlusion culling disabled scenario hint that the overhead of occlusion culling may not be necessary in situations where rendering complexity is low. Investigating the factors that influence performance in low poly object scenarios, like the CPU or GPU bottlenecks, might provide a better image into optimizing performance more effectively.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/1f2fe6ec-b6d4-44da-8a5f-34052600621b)

**Scenario 3: Room with high-poly objects** This scenario is used to determine how occlusion culling performs with high-poly objects in a scene. 

_FPS Trends:_
With high poly objects in the room, occlusion culling enabled scenarios maintain more stable FPS values compared to occlusion culling disabled scenarios throughout most of the test duration. Towards the end of the test, the occlusion culling disabled scenario experiences a sharp decline in FPS, which hints the performance implications of not utilizing occlusion culling, mainly in scenarios with high poly object complexity.

_Triangle Count Trends:_
Occlusion culling disabled scenarios display more dynamic changes in triangle count, which is caused by frustum culling. Having occlusion culling enabled on the other hand, avoids these drastic changes because of it's nature of disregarding objects that are placed behind other objects, which often occurs in this room-like environment.  

_Conclusion:_
The occlusion culling enabled scenario demonstrated better FPS stability and performance, when used in scenarios with high poly object complexity, where the impact of not using occlusion culling clearly becomes more apparent.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/3d8a85d9-48a2-4859-9c15-f8efee1d3d17)

### Level of Detail

**Scenarios 1 - 6**:

_FPS Trends:_
Initially and as time progresses, both LOD enabled and disabled scenarios have similar fps values.

_Triangle Count Trends:_
As expected, here is where we can see notable differences. The triangle counts for both, LOD enabled and disabled scenario, start with similar count values, but have very different fluctuations over time. 

The LOD enabled scenarios exhibit more dynamic movement. This is because the farther away a GameObject is from the camera, the less detailed (less triangles) the GameObject is. This explains the downward trends for LOD enabled scenarios. 

For LOD disabled scenarios, Unity by default uses the same amount of triangles for both close up and far away distances, which explains the horizontal trend lines between scenarios.

**Scenario 7**:

_FPS Trends:_
The significant FPS drop in the last LOD disabled scenario towards the end of the test demonstrates the potential performance impact of not utilizing LOD, especially in scenarios where scenes are quite complex.

![image](https://github.com/MatiasDure/OcclusionCulling_LOD/assets/91970565/52f914cd-5984-4641-938f-4b72f94e02f5)

_Conclusion:_
The data gathered indicates that making use of LOD to optimize rendering performance does not offer big performance gains when used in non-complex scenes. On the other hand, it does highlight the potential performance impact of not utilizing LOD in scenarios with high scene complexity. 

## Conclusion

Optimizing performance using techniques like Level of Detail (LOD) and Occlusion Culling can be really effective when used wisely. However, their impact on performance can vary depending on the context and scene. When used appropriately, they can significantly reduce the burden on the GPU, which can improve frame rates.

While LOD and Occlusion Culling can provide performance gains in certain scenarios, they may not always provide noticeable improvements. In some cases, the overhead of implementing these techniques may be greater than their performance benefits, especially in scenes with low rendering complexity or minimal occlusion opportunities.

Based on the results, these tools are excellent for maintaining consistent performance across different scene complexities. Rather than focusing on performance gains, they can also be used to help guarantee smooth and consistent frame rates.

Developers should investigate the trade-offs and performance consequences of implementing LOD and Occlusion Culling in their Unity projects. Profiling when utilizing these tools is a good strategy to determine whether it would benefitial.

## Laptop Specifications

_OS_: Windows 11 Home

_CPU:_ Ryzen 7 4800H

_GPU:_ NVIDIA GeForce RTX 2060

_RAM:_ 16GB DDR4

_Storage:_ 512GB PCIe SSD
