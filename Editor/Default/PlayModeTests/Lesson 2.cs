using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEditor.MPE;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Lesson2
{

    [UnitySetUp]
    public IEnumerator Setup()
    {
        //SceneManager.LoadScene("Assets/Scenes/SampleScene.unity", LoadSceneMode.Single);
        yield return EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/SampleScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

    [UnityTest]
    public IEnumerator ThereLinksToComponents()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var p = player.GetComponent<Player>();

        var myTypePlayer = typeof(Player);
        var myBindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;

        var field = myTypePlayer.GetField("rigidbody2d", myBindingFlags);

        yield return new WaitForEndOfFrame();

        Assert.AreEqual(player.GetComponent<Rigidbody2D>(), field.GetValue(p),
            $"There is no reference to the \"{player.GetComponent<Rigidbody2D>()}\" component in the \"{field.Name}\" field!");
    }

    [UnityTest]
    public IEnumerator JumpPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var p = player.GetComponent<Player>();

        var myTypePlayer = typeof(Player);
        var myBindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;

        var method = myTypePlayer.GetMethod("Jump", myBindingFlags);
        yield return new WaitForSeconds(.9f);

        var position = player.transform.position;
        method.Invoke(p, new object[] { });
        yield return new WaitForSeconds(0.2f);

        Assert.Greater(player.transform.position.y, position.y,
            $"The \"Jump\" method does not increase the position by Y!");
    }
}
