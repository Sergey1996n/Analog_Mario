using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class Lesson1
{
    GameObject gameGameObject;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Assets/Scenes/SampleScene.unity", LoadSceneMode.Single);
        //gameGameObject =
        //    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Root"));
        //game = gameGameObject.GetComponent<Game>();
    }


    [TearDown]
    public void Teardown()
    {
        //Object.Destroy(gameGameObject);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void Lesson1SimplePasses123()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    [Test]
    public void ExistsOnSceneTilemap()
    {
        

        Assert.IsNotNull(MonoBehaviour.FindObjectOfType<Tilemap>());


        //bool checkComponent = false;
        //foreach (Transform child in gameGameObject.transform)
        //{
        //    if (child.GetComponentInChildren<Tilemap>() != null)
        //    {
        //        checkComponent = true;
        //        break;
        //    }
        //}
        //Assert.IsTrue(checkComponent, 
        //    "Not Tilemap on Scene!");
        //yield return null;
    }

    [UnityTest]
    public IEnumerator ExistsDirectoryTiles()
    {
        Assert.IsTrue(Directory.Exists("Assets/Sprites/Tiles"), 
            "Not directory \"Tiles\" in \"Sprites\"!");
        yield return null;
    }
}
