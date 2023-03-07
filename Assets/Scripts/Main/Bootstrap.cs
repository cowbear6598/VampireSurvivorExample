using System;
using SoapUtils.SceneSystem;
using UnityEngine;
using Zenject;

namespace Main
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject] private ISceneService sceneService;

        private void Start()
        {
            sceneService.DoLoadScene(0);
        }
    }
}