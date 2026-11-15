using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManager;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private ReactiveVariable<int> _field;
        private ReactiveVariable<int> _field2;

        private List<IDisposable> _disposables;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");

            _disposables = new();
            _field = new ReactiveVariable<int>(5);
            _field2 = new ReactiveVariable<int>(10); 
            IDisposable didposeble = _field.Subscribe(OnFieldChanged);
            IDisposable didposeble2 = _field2.Subscribe((arg1, arg2) => Debug.Log($"Поле изменилось. Старое значение - {arg1}, новое - {arg2}"));

            _disposables.Add(didposeble);
            _disposables.Add(didposeble2);
        }

        private void OnFieldChanged(int arg1, int arg2)
        {
            Debug.Log($"Поле изменилось. Старое значение - {arg1}, новое - {arg2}");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(2)));
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _field.Value++;
                _field2.Value++;

                foreach(IDisposable disposable in _disposables)
                    disposable.Dispose();

                _disposables.Clear();
            }
        }
    }
}
