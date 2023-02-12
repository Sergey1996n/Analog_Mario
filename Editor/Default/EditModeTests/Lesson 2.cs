using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class Lesson2
{
    [Test]
    public void ExistsOnScenePlayer()
    {

        Assert.IsNotNull(GameObject.Find("Player"),
            "There is no \"Player\" object on the scene");
    }

    [Test]
    public void PlayerHaveTagPlayer()
    {
        Assert.AreEqual("Player", GameObject.Find("Player").tag, 
            "The \"Player\" object does not have a \"Player\" tag");
    }

    [Test]
    public void PlayerHaveRigidbody2d()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var rb = player.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Assert.AreEqual(player.AddComponent<Rigidbody2D>(), rb,
                "The \"Player\" object does not have a Rigibody 2D component");
        }
        

        Assert.AreEqual(rb.constraints, RigidbodyConstraints2D.FreezeRotation,
            "The Rigidbody2D component of the \"Player\" object does not have a check mark for the \"Freeze Rotation\" property");
        
    }

    [Test]
    public void PlayerHaveBoxCollider2d()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var bc = player.GetComponent<BoxCollider2D>();

        if (bc == null)
        {
            Assert.AreEqual(player.AddComponent<BoxCollider2D>(), bc,
            "The \"Player\" object does not have a Box Collider 2D component");
        }
    }

    [Test]
    public void TilemapHaveTilemapCollider2d()
    {
        var tilemap = GameObject.FindGameObjectWithTag("Ground");
        var tc = tilemap.GetComponent<TilemapCollider2D>();
        if (tc == null)
        {
            Assert.AreEqual(tilemap.AddComponent<BoxCollider2D>(), tc,
                "The \"Tilemap\" object does not have a Tilemap Collider 2D component");
        }
    }

    [Test]
    public void PlayerHaveScriptPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var p = player.GetComponent<Player>();
        if (p == null)
        {
            Assert.AreEqual(player.AddComponent<Player>(), p,
                "The \"Player\" object does not have a Player script");
        }

        TestAssistant.TestingFields(p, typeof(Player), "speed", "Single", FieldAttributes.Private, .2f, true);
        TestAssistant.TestingFields(p, typeof(Player), "jumpForce", "Single", FieldAttributes.Private, 8f, true);
        TestAssistant.TestingFields(p, typeof(Player), "rigidbody2d", "Rigidbody2D", FieldAttributes.Private);
        TestAssistant.TestingMethods(typeof(Player), "Start", "Void", MethodAttributes.Private | MethodAttributes.HideBySig, new MyParameterInfo[] { });
        TestAssistant.TestingMethods(typeof(Player), "FixedUpdate", "Void", MethodAttributes.Private | MethodAttributes.HideBySig, new MyParameterInfo[] { });
        TestAssistant.TestingMethods(typeof(Player), "Update", "Void", MethodAttributes.Private | MethodAttributes.HideBySig, new MyParameterInfo[] { });
        TestAssistant.TestingMethods(typeof(Player), "Jump", "Void", MethodAttributes.Private | MethodAttributes.HideBySig, new MyParameterInfo[] { });
        
    //    TestAssistant.TestingMethods(typeof(Player), 
    //        "OnCollisionEnter2D", 
    //        "Void", 
    //        MethodAttributes.Private | MethodAttributes.HideBySig, 
    //        new MyParameterInfo[] { new MyParameterInfo(typeof(Collision2D), "collision") });
    }
}
