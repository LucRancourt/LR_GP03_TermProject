using _Project.Code.Core.ServiceLocator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace _Project.Code.Core.General
{
    public class CoroutineExecutor : MonoBehaviourService
    {
        private List<Coroutine> _activeCoroutines = new();


        private void Start()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            StopAllCoroutines();
            _activeCoroutines.Clear();
        }

        public Coroutine StartCoroutineExec(IEnumerator routine)
        {
            Coroutine coroutine = StartCoroutine(routine);
            _activeCoroutines.Add(coroutine);
            return coroutine;
        }

        public Coroutine CallbackOnConditionMet(Func<bool> condition, Action callback)
        {
            Coroutine coroutine = StartCoroutine(OnConditionMet(condition, callback));
            _activeCoroutines.Add(coroutine);
            return coroutine;
        }

        private IEnumerator OnConditionMet(Func<bool> condition, Action callback)
        {
            if (condition == null)
                throw new ArgumentNullException("Null Condition " + nameof(condition));

            while (!condition())
                yield return null;

            callback?.Invoke();
        }

        public void CancelCoroutine(Coroutine coroutine)
        {
            Coroutine coroutineToCancel = _activeCoroutines.Find(t => t == coroutine);

            if (coroutineToCancel != null)
            {
                StopCoroutine(coroutineToCancel);
                _activeCoroutines.Remove(coroutineToCancel);
            }
        }
    }
}