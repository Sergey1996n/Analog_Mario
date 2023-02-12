using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class Lesson1EditMode
{
    [Test]
    public void ExistsOnSceneTwoTilemaps()
    {
        var tilemaps = MonoBehaviour.FindObjectsOfType<Tilemap>();
        Assert.AreEqual(2, tilemaps.Length,
            $"On scene {tilemaps.Length} map of 2");

        var tilemapIsNameTilemap = tilemaps.Where(t => t.gameObject.name == "Tilemap");
        if (tilemapIsNameTilemap != null)
        {
            Assert.AreEqual("Ground", tilemapIsNameTilemap.First().tag,
                "The \"Tilemap\" object does not have the \"Ground\" tag");
        }
    }

    [Test]
    public void ExistsDirectoryPalettes()
    {
        Assert.IsTrue(Directory.Exists("Assets/Sprites/Palettes"),
            "Not directory \"Palettes\" in \"Sprites\"!");
    }

    [Test]
    public void ExistsFileLevel()
    {
        Assert.IsTrue(File.Exists("Assets/Sprites/Palettes/Level.prefab"),
            "Not palette \"Level\" in directory \"Palettes\"!");
    }

    [Test]
    public void ExistsDirectoryTiles()
    {
        Assert.IsTrue(Directory.Exists("Assets/Sprites/Tiles"),
            "Not directory \"Tiles\" in \"Sprites\"!");
    }

    [Test]
    public void ExistsTileBonusCell()
    {
        Assert.IsTrue(File.Exists("Assets/Sprites/Tiles/BonusCell.asset"),
            "Not asset \"BonusCell\" in directory \"Tiles\"!");
    }

    [Test]
    public void ExistsTileBricks()
    {
        Assert.IsTrue(File.Exists("Assets/Sprites/Tiles/Bricks.asset"),
            "Not asset \"Bricks\" in directory \"Tiles\"!");
    }

    [Test]
    public void ExistsTileGround()
    {
        Assert.IsTrue(File.Exists("Assets/Sprites/Tiles/Ground.asset"),
            "Not asset \"Ground\" in directory \"Tiles\"!");
    }

    [Test]
    public void ExistsTileLeftBush()
    {
        Assert.IsTrue(File.Exists("Assets/Sprites/Tiles/LeftBush.asset"),
            "Not asset \"LeftBush\" in directory \"Tiles\"!");
    }

    [Test]
    public void ExistsTileCenterBushl()
    {
        Assert.IsTrue(File.Exists("Assets/Sprites/Tiles/CenterBush.asset"),
            "Not asset \"CenterBush\" in directory \"Tiles\"!");
    }

    [Test]
    public void ExistsTileRightBush()
    {
        Assert.IsTrue(File.Exists("Assets/Sprites/Tiles/RightBush.asset"),
            "Not asset \"RightBush\" in directory \"Tiles\"!");
    }

    [Test]
    public void ExistsTilesGroundInTilemap()
    {
        var tilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        for (int x = tilemap.cellBounds.position.x; x < tilemap.cellBounds.position.x + tilemap.cellBounds.size.x; x++)
        {
            for (int y = tilemap.cellBounds.position.y; y < tilemap.cellBounds.position.y + 2; y++)
            {
                var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                Assert.IsNotNull(tile,
                    $"There is not enough tile \"Ground\" or there are none at all.");
                Assert.AreEqual(tile.name, "Ground",
                    $"The name \"Ground\" was expected, but the name \"{tile.name}\" was found!");
            }
        }
    }

    [Test]
    public void ExistsTileBricksInTilemap()
    {
        var tilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        bool isExist = false;
        for (int x = tilemap.cellBounds.position.x; x < tilemap.cellBounds.position.x + tilemap.cellBounds.size.x && !isExist; x++)
        {
            for (int y = tilemap.cellBounds.position.y; y < tilemap.cellBounds.position.y + tilemap.cellBounds.size.y; y++)
            {
                if (tilemap.GetTile(new Vector3Int(x, y, 0))?.name == "Bricks")
                {
                    isExist = true;
                    break;
                }
            }
        }
        Assert.IsTrue(isExist,
            $"Tile named \"Bricks\" not found in Tilemap!");
    }

    [Test]
    public void ExistsTileBonusCellInTilemap()
    {
        var tilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        bool isExist = false;
        for (int x = tilemap.cellBounds.position.x; x < tilemap.cellBounds.position.x + tilemap.cellBounds.size.x && !isExist; x++)
        {
            for (int y = tilemap.cellBounds.position.y; y < tilemap.cellBounds.position.y + tilemap.cellBounds.size.y; y++)
            {
                if (tilemap.GetTile(new Vector3Int(x, y, 0))?.name == "BonusCell")
                {
                    isExist = true;
                    break;
                }
            }
        }
        Assert.IsTrue(isExist,
            $"Tile named \"BonusCell\" not found in Tilemap!");
    }

    [Test]
    public void NotExistsTileBushInTilemap()
    {
        var tilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        for (int x = tilemap.cellBounds.position.x; x < tilemap.cellBounds.position.x + tilemap.cellBounds.size.x; x++)
        {
            for (int y = tilemap.cellBounds.position.y; y < tilemap.cellBounds.position.y + tilemap.cellBounds.size.y; y++)
            {
                Assert.IsFalse(tilemap.GetTile(new Vector3Int(x, y, 0))?.name.IndexOf("Bush") > 0,
                    "Objects named \"Bash\" should be in Tilemap1");
            }
        }
    }

    [Test]
    public void ExistsTileBushInTilemap1()
    {
        var tilemaps = MonoBehaviour.FindObjectsOfType<Tilemap>();
        var tilemap = tilemaps.Where(t => !t.gameObject.CompareTag("Ground")).First().GetComponent<Tilemap>();
        bool isExist = false;
        for (int x = tilemap.cellBounds.position.x; x < tilemap.cellBounds.position.x + tilemap.cellBounds.size.x && !isExist; x++)
        {
            for (int y = tilemap.cellBounds.position.y; y < tilemap.cellBounds.position.y + tilemap.cellBounds.size.y; y++)
            {
                switch (tilemap.GetTile(new Vector3Int(x, y, 0))?.name)
                {
                    case "LeftBush":
                        var tile = tilemap.GetTile(new Vector3Int(x + 1, y, 0));
                        Assert.IsNotNull(tile,
                            "There is no tile \"CenterBush\" to the right of \"LeftBush\"");
                        Assert.AreEqual(tile.name, "CenterBush",
                            "To the right of \"LeftBush\" is a tile with a different name");
                        break;
                    case "CenterBush":
                        tile = tilemap.GetTile(new Vector3Int(x - 1, y, 0));
                        Assert.IsNotNull(tile,
                            "There is no tile \"LeftBush\" to the left of \"CenterBush\"");
                        Assert.AreEqual(tile.name, "LeftBush",
                            "To the left of \"CenterBush\" is a tile with a different name");

                        tile = tilemap.GetTile(new Vector3Int(x + 1, y, 0));
                        Assert.IsNotNull(tile,
                            "There is no tile \"RightBush\" to the right of \"CenterBush\"");
                        Assert.AreEqual(tile.name, "RightBush",
                            "To the right of \"CenterBush\" is a tile with a different name");
                        break;
                    case "RightBush":
                        tile = tilemap.GetTile(new Vector3Int(x - 1, y, 0));
                        Assert.IsNotNull(tile,
                            "There is no tile \"CenterBush\" to the left of \"RightBush\"");
                        Assert.AreEqual(tile.name, "CenterBush",
                            "To the left of \"RightBush\" is a tile with a different name");
                        break;
                    default:
                        break;
                }

                if (tilemap.GetTile(new Vector3Int(x, y, 0))?.name == "LeftBush" &&
                    tilemap.GetTile(new Vector3Int(x + 1, y, 0))?.name == "CenterBush" &&
                    tilemap.GetTile(new Vector3Int(x + 2, y, 0))?.name == "RightBush")
                {
                    isExist = true;
                    break;
                }
            }
        }
        Assert.IsTrue(isExist,
            $"Tile named \"Bush\" not found in Tilemap!");
    }

    [Test]
    public void ExistsObjectCloundInScene()
    {
        var clouds = MonoBehaviour.FindObjectsOfType<GameObject>()
            .Where(g => g.name == "Clouds")
            .FirstOrDefault();
        Assert.IsNotNull(clouds,
            "Object \"Clouds\" not found in scene");

        Assert.Greater(clouds.transform.childCount, 0,
            "Object \"Clouds\" not found child!");

        foreach (Transform item in clouds.transform)
        {
            Assert.AreEqual(item.name, "Cloud",
                "The \"Clouds\" object contains a child with an incorrect name!");
        }
        
    }

    [Test]
    public void CameraColor()
    {
        var camera = MonoBehaviour.FindObjectOfType<Camera>();
        Assert.IsNotNull(camera,
            "Object \"Camera\" not found in scene");

        //var gameObjectTest = new GameObject("Test Object");
        //var cameraTest = gameObjectTest.AddComponent<Camera>();
        //cameraTest.backgroundColor = new Color(99, 136, 251, 0);
        //cameraTest.allowMSAA = false;
        //cameraTest.orthographic = true;
        //cameraTest.depth = -1;
        //cameraTest.useOcclusionCulling = false;
        //Type myType = typeof(Camera);
        //Debug.Log(myType.GetProperties().Length);
        //List<string> list = new List<string>();
        //foreach (var item in myType.GetProperties())
        //{
        //    if (item.GetValue(cameraTest)?.ToString() != item.GetValue(camera)?.ToString())
        //    {
        //        Debug.Log(cameraTest.gameObject + ": " + item.Name + " - " + item.GetValue(cameraTest));
        //        Debug.Log(camera.gameObject + ": " + item.Name + " - " + item.GetValue(camera));
        //    }
        //}
        Assert.AreEqual(99, (int)(camera.backgroundColor.r * 255),
            "The \"MainCamera\" object in the \"Camera\" component has an incorrect value for the \"Background\" property in the red channel!");
        Assert.AreEqual(136, (int)(camera.backgroundColor.g * 255), 
            "The \"MainCamera\" object in the \"Camera\" component has an incorrect value for the \"Background\" property in the green channel!");
        Assert.AreEqual(251, (int)(camera.backgroundColor.b * 255), 
            "The \"MainCamera\" object in the \"Camera\" component has an incorrect value for the \"Background\" property in the blue channel!");
    }



    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator Lesson1EditModeWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}
}
